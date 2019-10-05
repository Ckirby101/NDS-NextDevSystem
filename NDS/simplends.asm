
; --------------------------------------------------------------------------


	DEVICE ZXSPECTRUMNEXT
	CSPECTMAP "nds.map"


	  include "NDS.asm"


	  org     $4000

Screen:
	incbin "screen0.scr"


StackEnd:
   ds      4
StackStart:
   db      0


	  include "includes.asm"


count:	db 0

; --------------------------------------------------------------------------
; --------------------------------------------------------------------------
StartAddress:
	di

	; we want 14mhz z80's only!
	nextreg 0x7,2

	nextreg 0x50,NDS_MMUBankStore
	call NDS_INIT




; --------------------------------------------------------------------------
; --------------------------------------------------------------------------
MainLoop

	ld a,191
	call WaitForLine

	ld a,(count)
	inc a
	ld (count),a
	and %11111000
	nextreg 0x32,a


	;poll the debugger system
	nextreg 0x50,NDS_MMUBank
	call NDS_POLL



	jp MainLoop



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









	SAVENEX OPEN "NDS.nex", StartAddress, StackStart
	SAVENEX CFG 7,0,0,0
	SAVENEX AUTO

