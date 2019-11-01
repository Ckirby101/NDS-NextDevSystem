
; --------------------------------------------------------------------------


	DEVICE ZXSPECTRUMNEXT
	CSPECTMAP "nds.map"


	  include "NDSFull.asm"
;	  include "NDS.asm"


	  org     $4000


Screen:
	incbin "nds.scr"

	  org     $6000

StackEnd:
   ds      20
StackStart:
   db      0


;	  include "NDSBasic.asm"
	  include "includes.asm"


count:	db 0
sptemp dw 0
; --------------------------------------------------------------------------
; --------------------------------------------------------------------------
StartAddress:
	di

	break
	; we want 14mhz z80's only!
	nextreg 0x7,2


	ld a,NDS_SLOT
	call GetMmuBank
	push af

	nextreg 0x50+NDS_SLOT,NDS_MMUBank
	call NDS_INIT

	pop af
	nextreg 0x50+NDS_SLOT,a


; 	//nextreg 0x50,NDS_MMUBankStore
; 	call NDS_INIT

; 	nextreg 9,%00001000			;reset divmmc mapram
;  	nextreg 6,%00011000			;divmmc automatic paging, nmi on!

; 	call NDS_BREAK

; 	;map in memory
;  	ld a,%10000011
;  	ld bc,$e3
;  	out (c),a

; 	call NDS_BREAK

;  	ld hl,RST
;  	ld de,$2000
;  	ld bc ,RSTLength
;  	ldir

;  	ld a,$c9
;  	ld ($3fff),a


; 	call NDS_BREAK

;  	; put divmmc bank
;  	ld a,%01000000
;  	ld bc ,$e3
;  	out (c),a


; 	call NDS_BREAK


; 	rst 0


	;ld hl,$1234
	;ld (sptemp),sp

	;NDS_BREAK


	ei
; --------------------------------------------------------------------------
; --------------------------------------------------------------------------
MainLoop

	halt
	;ld a,191
	;call WaitForLine

	ld a,(count)
	inc a
	ld (count),a
	;and %11111000
	nextreg 0x26,a


	;poll the debugger system
	;nextreg 0x50,NDS_MMUBank
	;call NDS_POLL

;	NDS_UPDATE



	jp MainLoop

	;rst 08h

; --------------------------------------------------------------------------
; --------------------------------------------------------------------------
WaitForLine:
	ld l,a

.Loop:
	ld	bc,$243B
	ld	a,$1f
	out	(c),a
	ld	bc,$253B
	in	a,(c)
	cp	l
	jr	nz,.Loop
	ret




; --------------------------------------------------------------------------
; a = mmu slot (0-7)
; Returns a = bank number
; --------------------------------------------------------------------------
GetMmuBank:
	push bc
	add 0x50
	ld bc,$243b	;select reg
	out (c),a
	inc b
	in a,(c)
	pop bc
	ret




Count: db 0







	SAVENEX OPEN "NDS.nex", StartAddress, StackStart
	SAVENEX CFG 7,0,0,0
	SAVENEX AUTO

