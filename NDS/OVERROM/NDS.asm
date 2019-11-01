; --------------------------------------------------------------------------
; NDS - Next Dev System v0.1
;
; To use include this file
; init NDS by doing

;	nextreg 0x50,NDS_MMUBankStore
;	call NDS_INIT

; This put NDS code over the rom routines at 0000 and init out code. You can still use 0000
; but if you single step and the rom routines are paged back in NDS will lose connection
; There are ways around this and im currently working on it (DIVMMC automapping)

; to update in your main loop call
;	nextreg 0x50,NDS_MMUBank
;	call NDS_POLL
;
; Current NDS will turn off interupts. but later we should be able to use NDS on a interupt.
; we need two banks until i have the devmmc stuff working!

; --------------------------------------------------------------------------
NDS_MMUBankStore 	equ 221			;bank where NDS code will be stored
NDS_MMUBank 		equ 222			;bank where NDS code will be copied too
NDS_BaudRate		equ 1958400		;baud rate for communication, set the same value for debugger


; --------------------------------------------------------------------------
;Protocol
; --------------------------------------------------------------------------
; --------------------------------------------------------------------------
; SetBank  CMD<180> - in=XY
; X = MMU Slot X (0-7)
; Y = Bank number  (0-222)
; --------------------------------------------------------------------------
; --------------------------------------------------------------------------
; PutData CMD<181> - in=XYYZZ  out=ABB
; X = Bank number  (0-222)
; YY = address to send to (0-8191)
; XX = Number of bytes (0-8192)
;
; A = Status 0=ok, !0 failed
; BB = bytes not sent, should be 0 if ok
; --------------------------------------------------------------------------
; --------------------------------------------------------------------------
; Execute  CMD<182> - XXYY
; XX = Stack address
; YY = PC address
; --------------------------------------------------------------------------
; --------------------------------------------------------------------------
; SendRegs CMD<183>	- out=A+
; A+ returns the 93 bytes register dump
; includes all z80 registers all readable Next io ports, stack data and mode
; --------------------------------------------------------------------------
; --------------------------------------------------------------------------
; SetRegs  CMD<184>	- X+
; X = 22 bytes z80 register data in order
; order f',a',r,i,f,a,iy,ix,bc',de',hl',bc,de,hl
; --------------------------------------------------------------------------
; --------------------------------------------------------------------------
; SendMem   CMD<185> - in=XYYZZ out=<MEM>A+
; X = Bank number  (0-222)
; YY = Address to of data (0-8191)
; ZZ = number of bytes (0-8192)
;
; A+ = data is sent to pc 
; --------------------------------------------------------------------------
; --------------------------------------------------------------------------
; Break   CMD<186>
; Stops execution and NDS wait for commands
; --------------------------------------------------------------------------
; --------------------------------------------------------------------------
; Continue CMD<187>
; Resumes execution after break
; --------------------------------------------------------------------------
; --------------------------------------------------------------------------
; Set Breakpoint   CMD<188> - in=XXY
; XX = addr (0-8191)
; Y = Bank number
; --------------------------------------------------------------------------
; --------------------------------------------------------------------------
; SendWatche CMD<189> - in=XYY  out=<WAT>AA B=Bank  AA=Address
; X = Bank number  (0-222)
; YY = Address to of data (0-8191);
;
; AA = bytes from address requested
; --------------------------------------------------------------------------
; --------------------------------------------------------------------------
; GetBreakpoints CMD<190> out=A+
; A = 53 byte dump of breakpoint data
; --------------------------------------------------------------------------
; --------------------------------------------------------------------------


NDS_Cmd_SetBank				equ	180		;used for running next code
NDS_Cmd_PutData				equ	181
NDS_Cmd_Execute				equ	182
NDS_Cmd_SendRegs			equ	183		;send registers to pc
NDS_Cmd_SetRegs				equ	184		;set the registers
NDS_Cmd_SendMem				equ	185		;send memeory to pc
NDS_Cmd_Break				equ	186
NDS_Cmd_Continue			equ	187
NDS_Cmd_SetBreakpoint		equ	188
NDS_Cmd_SendWatch			equ	189
NDS_Cmd_GetBreakpoints		equ	190
NDS_Cmd_DeleteBreakpoints	equ	191

NDS_RX						equ	143Bh
NDS_TX						equ	133Bh

NDS_Mode_Running			equ	0
NDS_Mode_Paused				equ	1
NDS_Mode_Error				equ	2

	; tells sjasm where to put our code
	; we want this code to be in bank UARTBank and orgd to 0000
	MMU 0 e,NDS_MMUBankStore
	org $0000
RST00:
	org $0008
RST08:
	org $0010
RST10:
	org $0018
RST18:
	org $0020
RST20:
	org $0028
RST28:
	org $0030
RST30:
	org $0038
RST38:
	JP NDS_BreakpointHit




; timing calculation table
NDS_BaudRateTable			dw 28000000/NDS_BaudRate,28571429/NDS_BaudRate,29464286/NDS_BaudRate,30000000/NDS_BaudRate,31000000/NDS_BaudRate,32000000/NDS_BaudRate,33000000/NDS_BaudRate,27000000/NDS_BaudRate


NDS_STACK					DS	20		;Stack space for downloader
NDS_REGISTERS				DS	20		;Other registers stored here
NDS_ZHL						DW	0		;HL stored here
NDS_ZPC						DW	0		;PC stored here
NDS_ZSP						DW	0		;SP stored here
NDS_HWREGS					ds	NDS_NumRegs
NDS_mode					db	NDS_Mode_Running
NDS_REGISTERS_LEN 			equ	$-NDS_REGISTERS


//max 10 breakpoints B=used BB=Addr B=Bank B=opcode
NDS_BreakpointData			ds 5*10
NDS_BreakpointDataLength	equ	$ - NDS_BreakpointData

NDS_temp					ds 4


; ************************************************************************************************
; NDS_INIT
; ************************************************************************************************
NDS_INIT:
	ld bc,$243b	;select reg
	ld a,0x11
	out (c),a
	inc b
	in a,(c)
	and 3			;video timing is in bottom 4 bits!

	ld hl,NDS_BaudRateTable
	add hl,a
	ld e,(hl)
	inc hl
	ld d,(hl)

	ld	bc,NDS_RX
	ld a,e
	and %1111111
	out	(c),a		//set lower 7 bits

	ld a,e
	sla a
	ld a,d
	rla
	or %10000000
	out	(c),a		;set to upper bits


	//clear breakpoints
	ld hl,NDS_BreakpointData
	ld bc,NDS_BreakpointDataLength-1
	ld de,NDS_BreakpointData+1
	xor a
	ld (hl),a
	ldir


	nextreg 0x51,NDS_MMUBank

	//thi is the bodge to copy code for now
	ld hl,0
	ld de,$2000
	ld bc,8192
	ldir



	ret

; ************************************************************************************************
; FindFreeBreakpoint
; hl with address of free breakpoint
; carry flag set if error
; ************************************************************************************************
NDS_FindFreeBreakpoint:
	push bc

	ld hl,NDS_BreakpointData
	ld b,10
.loop
	ld a,(hl) ;get used byte
	cp 0
	jr z,.foundfree
	add hl,5
	djnz.loop


	pop bc
	scf		//set the carry flag  error no space left
	ret
.foundfree
	pop bc
	xor a	//clear the carry flag  found one
	ret


; ************************************************************************************************
; FindBreakpoint
; de breakpoint addr to find
; c = bank
; return hl with breakpoint data found
; carry flag set if error
; ************************************************************************************************
NDS_FindBreakpoint:
	push bc

	ld ix,NDS_BreakpointData
	ld b,10
.loop
	ld a,(ix+0) ;get used byte
	cp 0
	jr z,.notused

	ld a,(ix+1)	;check address
	cp e
	jr nz,.notused

	ld a,(ix+2)
	cp d
	jr nz,.notused

	ld a,(ix+3)	;check bank
	cp c
	jr z,.found


.notused
	.5 inc ix
	djnz.loop

	pop bc
	scf		//set the carry flag  error did not find
	ret
.found
	push ix
	pop hl
	pop bc
	xor a	//clear the carry flag = found it
	ret



; ************************************************************************************************
; BreakpointHit
; ************************************************************************************************
NDS_BreakpointHit:
	di

	LD (NDS_ZHL),HL	;Save HL
	LD (NDS_ZSP),SP	;Save SP

	POP HL		;Get 'return' address

	dec hl			; we need to execute same instruction address
	PUSH hl
	LD (NDS_ZPC),HL	;Where I was called from

	LD SP,NDS_ZHL	;Top of register block

	PUSH DE
	PUSH BC
	EXX
	PUSH HL
	PUSH DE
	PUSH BC			;All registers are saved for NDS
	PUSH IX
	PUSH IY
	PUSH AF
	LD A,I
	LD H,A
	LD A,R
	LD L,A
	PUSH HL
	EX AF,AF'
	PUSH AF


	;store the bank no on stack
	ld bc,$243b	;select reg
	ld a,0x57
	out (c),a
	inc b
	in a,(c)
	push af

	ld de,(NDS_ZPC)	;ok
	call NDS_GetBankFromAddr	;a has bank number
	ld c,a					;bank in c
	nextreg 0x57,a

	ld a,d		;set top 3 bits aoff address
	or $e0
	ld d,a




	call NDS_FindBreakpoint
	jr c,.notfound

	//clear used flag
	xor a
	ld (hl),a	
	add hl,4
	ld a,(hl)		;get opcode

	ld (de),a		;put it back





.notfound
	ld a,NDS_Mode_Paused
	ld (NDS_mode),a	;ok

	;put the bank bank
	pop af
	nextreg 0x57,a



	jp NDS_checkCommandLoop






NDS_COLOR db 0
; ************************************************************************************************
; NDS_POLL
; ************************************************************************************************
NDS_POLL:
	di

	LD (NDS_ZHL),HL	;Save HL
	LD (NDS_ZSP),SP	;Save SP

	POP HL	;Get 'return' address
	LD (NDS_ZPC),HL	;Where I was called from

	LD SP,NDS_ZHL	;Top of register block

	PUSH DE
	PUSH BC
	EXX
	PUSH HL
	PUSH DE
	PUSH BC	;All registers are saved for NDS
	PUSH IX
	PUSH IY
	PUSH AF
	LD A,I
	LD H,A
	LD A,R
	LD L,A
	PUSH HL
	EX AF,AF'
	PUSH AF



NDS_checkCommandLoop
	call NDS_checkCommand


	ld a,(NDS_COLOR)
	inc a
	ld (NDS_COLOR),a
	out ($fe),a

	ld a,(NDS_mode)	;ok
	cp NDS_Mode_Paused
	jr z,NDS_checkCommandLoop


	xor a
	out ($fe),a


	POP AF
	EX AF,AF'
	POP HL
	LD A,H
	LD I,A
	LD A,L
	LD R,A
	POP AF
	POP IY
	POP IX	;Get all registers off stack
	POP BC
	POP DE
	POP HL
	EXX
	POP BC
	POP DE
	POP HL
	LD SP,(NDS_ZSP)		;ok
	ret


; ************************************************************************************************
; checkCommand
; ************************************************************************************************
NDS_checkCommand:

	;break
	;jp Command_SendRegs

	call NDS_dataready
	ret z		;nothing to do so exit

	call NDS_GetByte
	cp 'C'
	jr nz,.flush

	call NDS_GetByte
	cp 'M'
	jr nz,.flush

	call NDS_GetByte
	cp 'D'
	jr nz,.flush


	call NDS_GetByte
	cp NDS_Cmd_SetBank
	jp z,NDS_Command_SetBank
	cp NDS_Cmd_PutData
	jp z,NDS_Command_PutData
	cp NDS_Cmd_Execute
	jp z,NDS_Command_Execute
	cp NDS_Cmd_SendRegs
	jp z,NDS_Command_SendRegs
	cp NDS_Cmd_SetRegs
	jp z,NDS_Command_SetRegs
	cp NDS_Cmd_SendMem
	jp z,NDS_Command_SendMem
	cp NDS_Cmd_Break
	jp z,NDS_Command_Break
	cp NDS_Cmd_Continue
	jp z,NDS_Command_Continue
	cp NDS_Cmd_SetBreakpoint
	jp z,NDS_Command_SetBreakpoint
	cp NDS_Cmd_SendWatch
	jp z,NDS_Command_SendWatch

	cp NDS_Cmd_GetBreakpoints
	jp z,NDS_Command_GetBreakpoints

	cp NDS_Cmd_DeleteBreakpoints
	jp z,NDS_Command_DeleteBreakpoints


.flush
	;bad command so flush.
	call NDS_flush

	ret

; ************************************************************************************************
; dataready
; ************************************************************************************************
NDS_dataready:
	ld bc,NDS_TX
	in	a,(c)
	and	1       ; RX busy?
	ret   


; ************************************************************************************************
; flush
; ************************************************************************************************
NDS_flush:
	ld	bc,NDS_RX     ; FIFO (Read 512 BYTES)
	xor	a,a

.fifo:
	in	d,(c)
	in	d,(c)
	dec	a
	jr	nz,.fifo
	ret	 




; ************************************************************************************************
; NDS_GetByte
; get byte from computer
; a = byte from pc
; ************************************************************************************************
NDS_GetByte:
	push bc
	push de

	ld e,0
.loop
	dec e
	jr z,.exit		;timeout?

	ld	bc,NDS_TX
	in	a,(c)
	and	a,1       ; RX busy?
	jr	z,.loop


	ld	bc,NDS_RX
	in	a,(c)
	//out	(254),a   ; Set border color

.exit
	pop de
	pop bc
	ret



; ************************************************************************************************
; NDS_SendByte
; send  byte to computer
; a = byte to send
; ************************************************************************************************
NDS_SendByte:
	push bc
	push de

	ld d,a

	ld e,0
	ld	bc,NDS_TX
.loop
	dec e
	jr z,.exit		;timeout?

	in	a,(c)
	and	a,2       ; TX busy?
	jr	nz,.loop


	ld a,d
	out	(c),a
	and $1
	out	(254),a   ; Set border color

.exit

	pop de
	pop bc

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





NDS_regstoread:
	db 0,1,2,3,4,5,6,7,8,9,14,16,17,18,19,20,21,22,23,24,25,26,27,28,30,31,34,35,47,48,49,50,51,52,64,65,66,67,68,74,75,76
	db 80,81,82,83,84,85,86,87,104,107,108,110,111

NDS_NumRegs equ $-NDS_regstoread

; ************************************************************************************************
; Command_SendRegs
; ************************************************************************************************
NDS_Command_SendRegs:



	ld hl,NDS_HWREGS
	ld de,NDS_regstoread
	ld b,NDS_NumRegs
	ld c,0x3b

.regloop
	push bc


	//send register to read to HW
	ld b,0x24
	ld a,(de)
	inc de
	out(c),a
	//read register back
	ld b,0x25
	in a,(c)
	ld (hl),a
	inc hl

	pop bc
	djnz .regloop

	;get the command number
	;call NDS_GetByte();
	;ld l,a			;store command byte

	ld a,"R"
	call NDS_SendByte
	ld a,"E"
	call NDS_SendByte
	ld a,"G"
	call NDS_SendByte

	;ld a,l	;send the command byte
	;call NDS_SendByte


	ld hl,NDS_REGISTERS
	ld b,NDS_REGISTERS_LEN

.loop
	push bc

	ld a,(hl)
	inc hl
	call NDS_SendByte

	pop bc
	djnz .loop


	;send 4 layers of stack
	ld hl,(NDS_ZSP)		;ok
	inc hl			;junmp over return address
	inc hl

	ld b,4
.loop2
	ld a,(hl)
	call NDS_SendByte
	inc hl

	ld a,(hl)
	call NDS_SendByte
	inc hl


	djnz .loop2



	display "NDS SendRegs Len:",/D,(NDS_REGISTERS_LEN+3+8)



	jp NDS_checkCommand

; ************************************************************************************************
; Command_SetRegs
; ************************************************************************************************
NDS_Command_SetRegs:

	ld hl,NDS_REGISTERS
	ld b,22

.loop
	push bc

	call NDS_GetByte
	ld (hl),a
	inc hl

	pop bc
	djnz .loop


	jp NDS_checkCommand

; ************************************************************************************************
; Command_SetBreakpoint // realy set breakpoint
; ************************************************************************************************
NDS_Command_SetBreakpoint:



	;store the bank no onto stack
	ld bc,$243b	;select reg
	ld a,0x57
	out (c),a
	inc b
	in a,(c)
	push af




	;get  bank
	call NDS_GetByte
	ld c,a
	nextreg 0x57,a  ;$e000 - $ffff is paged in 




	;;get address of breakpoint
	call NDS_GetByte
	ld e,a
	call NDS_GetByte
	or $e0			;set top 3 bits or address
	ld d,a




	//allready there
	call NDS_FindBreakpoint
	jr nc,.exit			;alredy added?


	call NDS_FindFreeBreakpoint
	jp c,.exit			;cannot find breakpoint to use then ignore




	;mark bp as used
	ld a,1	
	ld (hl),a
	inc hl

	;store address
	ld (hl),e
	inc hl
	ld (hl),d
	inc hl

	ld (hl),c
	inc hl

	;store current opcode
	ld a,(de)		;get opcode
	ld (hl),a		;store opcode


	;overright opcode
	ld a,$ff  //opcode for RST 38H
	ld (de),a


.exit
	//put bank it back
	pop af		;get bank back
	nextreg 0x57,a

	jp NDS_checkCommand


; ************************************************************************************************
; Command_DeleteBreakpoints
; ************************************************************************************************
NDS_Command_DeleteBreakpoints:
	;store the bank no onto stack
	ld bc,$243b	;select reg
	ld a,0x57
	out (c),a
	inc b
	in a,(c)
	push af


	;get  bank
	call NDS_GetByte
	ld c,a
	nextreg 0x57,a  ;$e000 - $ffff is paged in 

	;;get address of breakpoint
	call NDS_GetByte
	ld e,a
	call NDS_GetByte
	or $e0			;set top 3 bits or address
	ld d,a


	//allready there
	call NDS_FindBreakpoint
	jr c,.exit			;its not there


	;clear used flag
	xor a
	ld (hl),a
	add hl,4

	;store current opcode
	ld a,(hl)		;get opcode
	ld (de),a		;put opcode back


.exit
	//put bank it back
	pop af		;get bank back
	nextreg 0x57,a

	jp NDS_checkCommand



; ************************************************************************************************
; Command_SendMem
; ************************************************************************************************
NDS_Command_SendMem:

	;store the bank no on stack
	ld bc,$243b	;select reg
	ld a,0x57
	out (c),a
	inc b
	in a,(c)
	push af


	//set the bank
	call NDS_GetByte
	nextreg 0x57,a


	call NDS_GetByte
	ld l,a
	call NDS_GetByte
	or $e0
	ld h,a
	call NDS_GetByte
	ld c,a
	call NDS_GetByte
	ld b,a


	ld a,"M"
	call NDS_SendByte
	ld a,"E"
	call NDS_SendByte
	ld a,"M"
	call NDS_SendByte

	ld a,c
	call NDS_SendByte
	ld a,b
	call NDS_SendByte


.loop
	;push bc

	ld a,(hl)
	inc hl
	call NDS_SendByte


	dec bc
	ld a,b
	or c
	jr nz,.loop
	;pop bc


	//put it back
	pop af		;get bank back
	nextreg 0x57,a

	jp NDS_checkCommand


; ************************************************************************************************
; Command_SendWatch
; ************************************************************************************************
NDS_Command_SendWatch:

	;store the bank no on stack
	ld bc,$243b	;select reg
	ld a,0x57
	out (c),a
	inc b
	in a,(c)
	push af




	call NDS_GetByte	;bank number
	nextreg 0x57,a



	call NDS_GetByte
	ld l,a

	call NDS_GetByte
	or $e0
	ld h,a

	ld e,(hl)
	inc hl
	ld d,(hl)


	ld a,"W"
	call NDS_SendByte
	ld a,"A"
	call NDS_SendByte
	ld a,"T"
	call NDS_SendByte

	ld a,e
	call NDS_SendByte
	ld a,d
	call NDS_SendByte


	//put it back
	pop af		;get bank back
	nextreg 0x57,a


	jp NDS_checkCommand

; ************************************************************************************************
; Command_GetBreakpoints
; ************************************************************************************************
NDS_Command_GetBreakpoints:

	ld a,"B"
	call NDS_SendByte
	ld a,"R"
	call NDS_SendByte
	ld a,"K"
	call NDS_SendByte

	;ld a,l	;send the command byte
	;call NDS_SendByte


	ld hl,NDS_BreakpointData
	ld b,NDS_BreakpointDataLength

.loop
	push bc

	ld a,(hl)
	inc hl
	call NDS_SendByte

	pop bc
	djnz .loop


	jp NDS_checkCommand

	display "NDS GetBreakpoints Len:",/D,(NDS_BreakpointDataLength+3)

; ************************************************************************************************
; Command_SetBank
; ************************************************************************************************
NDS_Command_SetBank:




	call NDS_GetByte	;slot
	and $7
	add $50			;is Next register
	ld d,a

	call NDS_GetByte	;bank number
	ld bc,$243b	;select reg
	out (c),d
	inc b
	out (c),a

	jp NDS_checkCommand


; ************************************************************************************************
; Command_PutData
; ************************************************************************************************
NDS_Command_PutData:

	;store the bank no on stack
	ld bc,$243b	;select reg
	ld a,0x57
	out (c),a
	inc b
	in a,(c)
	push af




	//call NDS_DebugFlash

	call NDS_GetByte	;bank number
	nextreg 0x57,a

	//display bank number im writing too
	ld de,0
	call NDS_PrintHex


	call NDS_GetByte	;dest
	ld l,a
	call NDS_GetByte
	or $E0
	ld h,a

	call NDS_GetByte	;length
	ld e,a
	call NDS_GetByte
	ld d,a

	call NDS_readfixed
	jr c,.SendError
	xor a		
	call NDS_SendByte	;send ok

	ld a,e
	call NDS_SendByte	;send end count
	ld a,d
	call NDS_SendByte





	//put it back
	pop af		;get bank back
	nextreg 0x57,a

	jp NDS_checkCommand

.SendError
	ld a,e
	call NDS_SendByte	;send end count
	ld a,d
	call NDS_SendByte


	call NDS_SendByte	;send fail
	ld a,0xff
	call NDS_SendByte

	//put it back
	pop af		;get bank back
	nextreg 0x57,a

	jp NDS_checkCommand


//***************************************************************************************
; > HL = Destination
; > de = Length
;
; return
; de byte left to read if error should be zero
//***************************************************************************************
NDS_readfixed:
	push hl

.nextbyteloop
	;clear timeout flag
 	exx
 	ld bc,30000
 	exx

.loop
	;is there a byte ready?
	ld	bc,NDS_TX
	in	a,(c)
	and	a,1       ; RX busy?
	jr	nz,.dataready


	;update timeout only
 	exx
 	dec bc
 	ld a,b
 	or c
 	jr z,.timeout
 	exx


	jr .loop

.dataready


	;read byte
	ld	bc,NDS_RX
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


	pop hl
	scf
	ccf			;clear carry flag
	ret

.timeout
	exx
	pop hl
	scf
	ret



; ************************************************************************************************
Sleep:
	push af
	push bc

	ld bc,50
.loop
	dec bc
	ld a,b
	or c
	jr nz,.loop

	pop bc
	pop af
	ret



; ************************************************************************************************
; Command_Break
; ************************************************************************************************
NDS_Command_Break:

	ld a,NDS_Mode_Paused
	ld (NDS_mode),a


	jp NDS_checkCommand	

; ************************************************************************************************
; Command_Continue
; ************************************************************************************************
NDS_Command_Continue:
	ld a,NDS_Mode_Running
	ld (NDS_mode),a


	jp NDS_checkCommand	

; ************************************************************************************************
; Command_Execute
; ************************************************************************************************
NDS_Command_Execute:


	call NDS_DebugFlash


	; puit screen memeory in correct place
	nextreg 0x52,10
	nextreg 0x53,11


	call NDS_GetByte	;stack
	ld l,a
	call NDS_GetByte
	ld h,a
	ld (NDS_stack_addr+1),hl

	call NDS_GetByte	;execute addr
	ld l,a
	call NDS_GetByte
	ld h,a
	ld (NDS_pc_addr+1),hl

	ld hl,NDS_BootStrap
	ld de,$4000
	ld bc,NDS_BootStrapSize
	ldir

	jp $4000



; ************************************************************************************************
; GetBankFromAddr
; de is and address paged into memory returns the bank this address is in
; de addr 
; returns A=bank number
; ************************************************************************************************
NDS_GetBankFromAddr:
	push bc
	ld a,d
	and $e0	; only want top 3 bits 
	swapnib
	sra a
	;now top 3 bits are in low 3 bits  0-7


	add 0x50
	; a id now the MMU register we want to read


	ld bc,$243b	;select reg
	out (c),a
	inc b
	in a,(c)

	//ld (RST00+3),a

	;a is now the bank number
	pop bc
	ret




NDS_DebugFlash:
	push bc
	push af
	ld bc,10000
.loop

	ld a,1
	out ($fe),a
	ld a,2
	out ($fe),a

	dec bc
	ld a,b
	or c
	jr nz,.loop


	pop af
	pop bc
	ret



; ******************************************************************************
;
;	A  = hex value tp print
;	DE= address to print to (normal specturm screen)
;
; ******************************************************************************
NDS_PrintHex:


	push	bc
	push	hl


	ld h,a

	ld a,d
	or $e0
	ld d,a

	;store the bank no on stack
	ld bc,$243b	;select reg
	ld a,0x57
	out (c),a
	inc b
	in a,(c)
	push af


	nextreg 0x57,10

	ld a,h
	push af

	ld	bc,NDS_HexCharset

	srl	a
	srl	a
	srl	a
	srl	a	
	call	NDS_DrawHexCharacter

	pop	af
	and	$f	
	call	NDS_DrawHexCharacter


	ld hl,$f800
	ld de,$f801
	ld bc,767
	ld a,0
	ld (hl),a
	ldir


	pop af		;get bank back
	nextreg 0x57,a



	pop	hl
	pop	bc
	ret


;
; A= hex value to print
;
NDS_DrawHexCharacter:	
	ld	h,0
	ld	l,a
	add	hl,hl	;*8
	add	hl,hl
	add	hl,hl
	add	hl,bc	; add on base of character wet

	push	de
	push	bc
	ld	b,8
.lp1:	
	ld	a,(hl)
	ld	(de),a
	inc	hl		; cab't be sure it's on a 256 byte boundary
	inc	d		; next line down
	djnz	.lp1
	pop	bc
	pop	de
	inc	e
	ret


NDS_HexCharset:
	db %00000000	;char30  '0'
	db %00111100
	db %01000110
	db %01001010
	db %01010010
	db %01100010
	db %00111100
	db %00000000
	db %00000000	;char31	'1'
	db %00011000
	db %00101000
	db %00001000
	db %00001000
	db %00001000
	db %00111110
	db %00000000
	db %00000000	;char32	'2'
	db %00111100
	db %01000010
	db %00000010
	db %00111100
	db %01000000
	db %01111110
	db %00000000
	db %00000000	;char33	'3'
	db %00111100
	db %01000010
	db %00001100
	db %00000010
	db %01000010
	db %00111100
	db %00000000
	db %00000000	;char34	'4'
	db %00001000
	db %00011000
	db %00101000
	db %01001000
	db %01111110
	db %00001000
	db %00000000
	db %00000000	;char35	'5'
	db %01111110
	db %01000000
	db %01111100
	db %00000010
	db %01000010
	db %00111100
	db %00000000
	db %00000000	;char36	'6'
	db %00111100
	db %01000000
	db %01111100
	db %01000010
	db %01000010
	db %00111100
	db %00000000
	db %00000000	;char37	'7'
	db %01111110
	db %00000010
	db %00000100
	db %00001000
	db %00010000
	db %00010000
	db %00000000
	db %00000000	;char38	'8'
	db %00111100
	db %01000010
	db %00111100
	db %01000010
	db %01000010
	db %00111100
	db %00000000
	db %00000000	;char39	'9'
	db %00111100
	db %01000010
	db %01000010
	db %00111110
	db %00000010
	db %00111100
	db %00000000
	db %00000000	;char41	'A'
	db %00111100
	db %01000010
	db %01000010
	db %01111110
	db %01000010
	db %01000010
	db %00000000
	db %00000000	;char42	'B'
	db %01111100
	db %01000010
	db %01111100
	db %01000010
	db %01000010
	db %01111100
	db %00000000
	db %00000000	;char43	'C'
	db %00111100
	db %01000010
	db %01000000
	db %01000000
	db %01000010
	db %00111100
	db %00000000
	db %00000000	;char44	'D'
	db %01111000
	db %01000100
	db %01000010
	db %01000010
	db %01000100
	db %01111000
	db %00000000
	db %00000000	;char45	'E'
	db %01111110
	db %01000000
	db %01111100
	db %01000000
	db %01000000
	db %01111110
	db %00000000
	db %00000000	;char46	'F'
	db %01111110
	db %01000000
	db %01111100
	db %01000000
	db %01000000
	db %01000000
	db %00000000



; ************************************************************************************************
; Copied to $4000 and executed there
; ************************************************************************************************
NDS_BootStrap:
	nextreg 0x50,$ff
	nextreg 0x51,$ff
	nextreg 0x54,4
	nextreg 0x55,5
	;nextreg 0x56,0
	;nextreg 0x57,1
NDS_stack_addr
	ld sp,0

NDS_pc_addr
	jp 0

NDS_BootStrapSize equ $ - NDS_BootStrap

	display "NDS Bootstrap Size ",/D,NDS_BootStrapSize

;;
;; Set up the Nex output
;;


NDS_CodeSize equ $ 

	display "NDS Code Size ",/D,NDS_CodeSize




