

	include "Constants.asm"






ORDER_SLU	equ %00000
ORDER_LSU	equ %00100
ORDER_SUL	equ %01000
ORDER_LUS	equ %01100
ORDER_USL	equ %10000
ORDER_ULS	equ %10100



NegDE: macro
	xor a
	sub e
	ld e,a
	sbc a,a
	sub d
	ld d,a
	endm



BREAK: 	macro
		db $DD,$01
		endm


BORDER macro color
	push af
	ld a,color
	and 7
	out (254),a
	pop af
	endm

nexregcol8	macro reg,r,g,b
			nextreg reg, ((r&$7)<<5) | ((g&$7)<<2) | ((b&$6)>>1)
			endm


;convert rgb888 to rgb333
col9bit macro r,g,b
	db  (r&$E0) | ((g&$E0)>>3) | ((b&$C0)>>6)
	db ((b&$20)>>5)
	endm

;convert rgb888 to rgb332
col8bit macro r,g,b
	db  (r&$E0) | ((g&$E0)>>3) | ((b&$C0)>>6)
	endm	





PAGE0000	equ 0
PAGE2000	equ 1
PAGE4000	equ 2
PAGE6000	equ 3
PAGEA000	equ 4
PAGEC000	equ 5
PAGEE000	equ 6


PAGEAREA0: equ 0
PAGEAREA1: equ $2000
PAGEAREA2: equ $4000
PAGEAREA3: equ $6000
PAGEAREA4: equ $8000
PAGEAREA5: equ $a000
PAGEAREA6: equ $c000
PAGEAREA7: equ $e000


MMU0_ADDR: equ $0000
MMU1_ADDR: equ $2000
MMU2_ADDR: equ $4000
MMU3_ADDR: equ $6000
MMU4_ADDR: equ $8000
MMU5_ADDR: equ $A000
MMU6_ADDR: equ $C000
MMU7_ADDR: equ $E000




MMU0	macro bank
		nextreg	$50,bank	;$0000 - $1fff
		endm

MMU2000	macro bank	
		nextreg	$51,bank	;$2000 - $3fff
		endm
MMU1	macro bank	
		nextreg	$51,bank	;$2000 - $3fff
		endm

MMU4000	macro bank		
		nextreg	$52,bank	;$4000 - $5fff
		endm
MMU2	macro bank		
		nextreg	$52,bank	;$4000 - $5fff
		endm

MMU6000	macro bank		
		nextreg	$53,bank	;$6000 - $7fff
		endm
MMU3	macro bank		
		nextreg	$53,bank	;$6000 - $7fff
		endm

MMU8000	macro bank		
		nextreg	$54,bank	;$8000 - $9fff
		endm
MMU4	macro bank		
		nextreg	$54,bank	;$8000 - $9fff
		endm

MMUA000	macro bank		
		nextreg	$55,bank	;$A000 - $Bfff
		endm
MMU5	macro bank		
		nextreg	$55,bank	;$A000 - $Bfff
		endm

MMUC000	macro bank		
		nextreg	$56,bank	;$C000 - $Dfff
		endm
MMU6	macro bank		
		nextreg	$56,bank	;$C000 - $Dfff
		endm

MMUE000	macro bank		
		nextreg	$57,bank	;$E000 - $Ffff
		endm
MMU7	macro bank		
		nextreg	$57,bank	;$E000 - $Ffff
		endm


MMU0A	macro
		nextreg	$50,a	;$0000 - $1fff
		endm

MMU1A	macro		
		nextreg	$51,a	;$2000 - $3fff
		endm

MMU2A	macro		
		nextreg	$52,a	;$4000 - $5fff
		endm

MMU3A	macro		
		nextreg	$53,a	;$6000 - $7fff
		endm

MMU6000_A	macro		
		nextreg	$53,a	;$6000 - $7fff
		endm


MMU4A	macro		
		nextreg	$54,a	;$8000 - $9fff
		endm

MMU5A	macro		
		nextreg	$55,a	;$A000 - $Bfff
		endm

MMU6A	macro		
		nextreg	$56,a	;$C000 - $Dfff
		endm

MMU7A	macro		
		nextreg	$57,a	;$E000 - $Ffff
		endm







