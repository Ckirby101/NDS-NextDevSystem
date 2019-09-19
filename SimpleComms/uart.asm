
; --------------------------------------------------------------------------


	DEVICE ZXSPECTRUMNEXT
	CSPECTMAP "uart.map"


	  org     $8000



StackEnd:
   ds      4
StackStart:
   db      0

;opt	sna=StartAddress:StackStart                             ; save SNA,Set PC = run point in SNA file and TOP of stack
;opt	Z80                                                     ; Set z80 mode
;opt	ZXNEXT 
;opt Illegal
	  include "includes.asm"

; SPECTRUM NEXT SERIAL LINK ROUTINES.

; **INTERRUPTS SHOULD BE OFF WHEN ACCESSING THE UART**

; see https://www.specnext.com/tbblue-io-port-system for register information.


; here are some limits:
; Data format is fixed to 8N1: 8 data bits, no parity bit, one stop bit.
; No handshake signals, such as RTS/CTS, DTR/DSR.
; Not 5V tolerant. Putting more than 3.6V will cause the Spartan6 death


;Protocol
;
; SetBank CMD<180> - XY   Set Bank SLOT MMU (X) to Bank Y 
; PutrData CMD<181> - AAXX copy data starting at address AA with XX byte following
; Execute CMD<182> - XXAA run code at AA set stack to XX


	;include "uartlowlevel.asm"




bordercolor db GREEN


RX	equ	143Bh
TX	equ	133Bh


; --------------------------------------------------------------------------


; **ADDITIONAL CODE WILL BE NEEDED TO SET OTHER BAUD RATES**

; A 14 bit prescaler value must be programmed into the the hardware to set up the BAUD rate.
; The BAUD rate must be adjusted for each video timing mode (VGA 0..6, HDMI 7).
; There are eight possible values for each rate.
; Register 0x11 (17) can be read to determine the 3 bit timing mode.
; This example assumes 115200 BAUD and videe timing mode 0 (VGA 0).

; 115200 BAUD timing modes 0-7, 14 bit prescaler values: 243,248,256,260,269,278,286,234


; Set timing for VGA 0 and 115200 BAUD.  230400
; 460800
; 921600
; 1843200
;
;
;000 = Base VGA timing, clk28 = 28000000
;001 = VGA setting 1, clk28 = 28571429
;010 = VGA setting 2, clk28 = 29464286
;011 = VGA setting 3, clk28 = 30000000
;100 = VGA setting 4, clk28 = 31000000
;101 = VGA setting 5, clk28 = 32000000
;110 = VGA setting 6, clk28 = 33000000
;111 = HDMI, clk28 = 27000000

;
;
; 28 is 28000000 / 1000000  1mpbs
; value is clk28 / baud rate
rate: equ 30 ;61 ;15 ;121 ;243


StartAddress:
	di

	;want 14mhz
	;nextreg $7,%00
	;copy this code into very high bank (and hope it wont be needed)
	nextreg 0x57,110
	ld hl,$8000
	ld de,$e000
	ld bc,CodeSize
	ldir

	;we are now runnign code in high bank memory
	nextreg 0x54,110

; ************************************************************************************************
; init
; ************************************************************************************************
init:
	ld	bc,RX	; 115200 VGA mode 0 (243)  00000011110011
	ld	a,rate&%1111111        ;01110011b  ; Lower 7 bits of BAUD rate (LSB) 0LLLLLLL
	out	(c),a
	ld	a,%10000000 | (rate>>7)        ; 10000001b  ; Upper 7 bits of BAUD rate (MSB) 1MMMMMMM
	out	(c),a


; ************************************************************************************************
; checkCommand
; ************************************************************************************************
checkCommand:
	call dataready
	jr nz,.read	


	;green waiting pattern
	ld a,BLACK
	out	(254),a
	ld bc,400
	call delay

	ld a,(bordercolor)
	out	(254),a
	ld bc,GREEN
	call delay

	jr checkCommand

.read
	call GetByte
	cp 'C'
	jr nz,.flush

	call GetByte
	cp 'M'
	jr nz,.flush

	call GetByte
	cp 'D'
	jr nz,.flush


	call GetByte
	cp 180
	jp z,Command_SetBank
	cp 181
	jp z,Command_PutData
	cp 182
	jp z,Command_Execute



.flush
	;bad command so flush.
	call flush

	jp checkCommand

; ************************************************************************************************
; dataready
; ************************************************************************************************
dataready:
	ld bc,TX
	in	a,(c)
	and	1       ; RX busy?
	ret   


; ************************************************************************************************
; flush
; ************************************************************************************************
flush:
	ld	bc,RX     ; FIFO (Read 512 BYTES)
	xor	a,a

.fifo:
	in	d,(c)
	in	d,(c)
	dec	a
	jr	nz,.fifo
	ret	 


//***************************************************************************************
; > HL = Destination
; > de = Length
//***************************************************************************************
readfixed:
	push hl



.nextbyteloop


	;clear timeout flag
	exx
	ld bc,2000
	exx

.loop

	;update timeout only 256 times test then out!
	exx
	dec bc
	ld a,b
	or c
	jr z,.timeout
	exx


	;is there a byte ready?
	ld	bc,TX
	in	a,(c)
	and	a,1       ; RX busy?
	jr	z,.loop


	;read byte
	ld	bc,RX
	in	a,(c)
	ld	(hl),a    ; Store to memory
	inc	hl
	and %1
	out	(254),a   ; Set border color

	;decrease counter
	dec	de
	ld	a,e
	or	d
	jr	nz,.nextbyteloop

	ld a,GREEN
	ld (bordercolor),a

	pop hl
	ret

.timeout

	ld a,RED
	ld (bordercolor),a

	exx
	pop hl
	ret

; ************************************************************************************************
; GetByte
; get byte from computer
; destroys bc
; a = byte from pc
; ************************************************************************************************
GetByte:
	push de

	ld e,0
.loop
	dec e
	jr z,.exit		;timeout?

	ld	bc,TX
	in	a,(c)
	and	a,1       ; RX busy?
	jr	z,.loop


	ld	bc,RX
	in	a,(c)
	out	(254),a   ; Set border color

.exit
	pop de
	ret







; ************************************************************************************************
; delay
; ************************************************************************************************
delay:
.loop

	dec bc
	ld a,c
	or b
	jr nz,.loop
	ret







; ************************************************************************************************
; Command_SetBank
; ************************************************************************************************
Command_SetBank:

	call GetByte	;slot
	and $7
	add $50			;is Next register
	ld d,a

	call GetByte	;bank number
	ld bc,$243b	;select reg
	out (c),d
	ld b,$25	;select bank
	out (c),a

	jp checkCommand


; ************************************************************************************************
; Command_PutData
; ************************************************************************************************
Command_PutData:

	call GetByte	;dest
	ld l,a
	call GetByte
	ld h,a


	call GetByte	;length
	ld e,a
	call GetByte
	ld d,a


	call readfixed

	jp checkCommand	


; ************************************************************************************************
; Command_Execute
; ************************************************************************************************
Command_Execute:

	; puit screen memeory in correct place
	nextreg 0x52,10
	nextreg 0x53,11


	call GetByte	;stack
	ld l,a
	call GetByte
	ld h,a
	ld (stack_addr+1),hl

	call GetByte	;execute addr
	ld l,a
	call GetByte
	ld h,a
	ld (pc_addr+1),hl

	ld hl,BootStrap
	ld de,$4000
	ld bc,BootStrapSize
	ldir

	jp $4000



; ************************************************************************************************
; Copied to $4000 and executed there
; ************************************************************************************************
BootStrap:
	nextreg 0x54,4
	nextreg 0x55,5
	nextreg 0x56,0
	nextreg 0x57,1
stack_addr
	ld sp,0

pc_addr
	jp 0

BootStrapSize equ $ - BootStrap

	display "Bootstrap Size ",/D,BootStrapSize

;;
;; Set up the Nex output
;;


CodeSize equ $ - StackEnd


;	SAVESNA "uart.sna", StartAddress


	display "Nex Start:",/H,StartAddress
	display "Nex Stack:",/H,StackStart
	display "Total Bytes Used ",/D,CodeSize


        ; This sets the name of the project, the start address, 
        ; and the initial stack pointer.
        SAVENEX OPEN "uart.nex", StartAddress, StackStart

        ; This asserts the minimum core version.  Set it to the core version 
        ; you are developing on.
        SAVENEX CORE 2,0,0

        ; This sets the border colour while loading (in this case white),
        ; what to do with the file handle of the nex file when starting (0 = 
        ; close file handle as we're not going to access the project.nex 
        ; file after starting.  See sjasmplus documentation), whether
        ; we preserve the next registers (0 = no, we set to default), and 
        ; whether we require the full 2MB expansion (0 = no we don't).
        SAVENEX CFG 7,0,0,0

        ; Generate the Nex file automatically based on which pages you use.
        SAVENEX AUTO

        //SAVENEX BANK 5,2,0,1,30
