.data
;----------------------------------------------------------------------------------------------
;asmBlackAndWhite .data section
zero		qword 0;

maskRRev	byte 80h, 80h, 00h, 80h, 80h, 04h, 80h, 80h, 08h, 80h, 80h, 0Ch, 80h, 80h, 80h, 80h
maskGRev	byte 80h, 00h, 80h, 80h, 04h, 80h, 80h, 08h, 80h, 80h, 0Ch, 80h, 80h, 80h, 80h, 80h
maskBRev	byte 00h, 80h, 80h, 04h, 80h, 80h, 08h, 80h, 80h, 0Ch, 80h, 80h, 80h, 80h, 80h, 80h

vectRRRR	real4 0.2126, 0.2126, 0.2126, 0.2126
vectGGGG	real4 0.7152, 0.7152, 0.7152, 0.7152
vectBBBB	real4 0.0722, 0.0722, 0.0722, 0.0722

maskRRRR	byte 02h, 80h, 80h, 80h, 05h, 80h, 80h, 80h, 08h, 80h, 80h, 80h, 0Bh, 80h, 80h, 80h
maskGGGG	byte 01h, 80h, 80h, 80h, 04h, 80h, 80h, 80h, 07h, 80h, 80h, 80h, 0Ah, 80h, 80h, 80h
maskBBBB	byte 00h, 80h, 80h, 80h, 03h, 80h, 80h, 80h, 06h, 80h, 80h, 80h, 09h, 80h, 80h, 80h
maskGREY	byte 00h, 00h, 00h, 04h, 04h, 04h, 08h, 08h, 08h, 0Ch, 0Ch, 0Ch, 80h, 80h, 80h, 80h


divRed   	real4 0.2126
divGreen 	real4 0.7152
divBlue     real4 0.0722
NEXT_PIX    EQU 36
IMG_WIDTH   EQU 22176

; kernel matrix start (box bluring - each pixel divided by 9)
;A			real4 0.11, 0.11, 0.11, 0.11
;B			real4 0.11, 0.11, 0.11, 0.11
;C			real4 0.11, 0.11, 0.11, 0.11
;D			real4 0.11, 0.11, 0.11, 0.11
;E			real4 0.11, 0.11, 0.11, 0.11
;F			real4 0.11, 0.11, 0.11, 0.11
;G			real4 0.11, 0.11, 0.11, 0.11
;H			real4 0.11, 0.11, 0.11, 0.11
;I			real4 0.11, 0.11, 0.11, 0.11
; kernel matrix end

; kernel matrix start (sharpening)
;A			real4  0.0,  0.0,  0.0,  0.0
;B			real4 -1.0, -1.0, -1.0, -1.0
;C			real4  0.0,  0.0,  0.0,  0.0
;D			real4 -1.0, -1.0, -1.0, -1.0
;E			real4  5.0,  5.0,  5.0,  5.0
;F			real4 -1.0, -1.0, -1.0, -1.0
;G			real4  0.0,  0.0,  0.0,  0.0
;H			real4 -1.0, -1.0, -1.0, -1.0
;I			real4  0.0,  0.0,  0.0,  0.0
; kernel matrix end

; kernel matrix start (edge detection)
A			real4 -1.0, -1.0, -1.0, -1.0
B			real4 -1.0, -1.0, -1.0, -1.0
C			real4 -1.0, -1.0, -1.0, -1.0
D			real4 -1.0, -1.0, -1.0, -1.0
E			real4  8.0,  8.0,  8.0,  8.0
F			real4 -1.0, -1.0, -1.0, -1.0
G			real4 -1.0, -1.0, -1.0, -1.0
H			real4 -1.0, -1.0, -1.0, -1.0
I			real4 -1.0, -1.0, -1.0, -1.0
; kernel matrix end

counter         	qword 0
tmp      	word 0
;end of asmBlackAndWhite .data section
;----------------------------------------------------------------------------------------------

.code

asmBlackAndWhite proc
	mov			rbx, rcx
	mov			rax, zero					;zero RAX
	mov			eax, dword ptr[r8+8]		;store image length in bytes in EAX
	add			rax, rbx					;RAX now holds max address in array

	vmovdqu		xmm3, xmmword ptr[maskRRRR]	;store constants in ragisters for faster access
	vmovdqu		xmm4, xmmword ptr[maskGGGG]
	vmovdqu		xmm5, xmmword ptr[maskBBBB]

	vmovdqu		xmm6, xmmword ptr[vectRRRR]
	vmovdqu		xmm7, xmmword ptr[vectGGGG]
	vmovdqu		xmm8, xmmword ptr[vectBBBB]

	vmovdqu		xmm9, xmmword ptr[maskGREY]

pixBatch4:									;procedure loop
	vmovdqu		xmm0, xmmword ptr[rbx]		;load data to xmm (too much)
	vpshufb		xmm2, xmm0, xmm3			;shuffle R values to xmm2, just 4 pixels
	vcvtdq2ps	xmm2, xmm2					;convert int -> float
	vmulps		xmm2, xmm2, xmm6			;multiply 4 pixels R
	vpshufb		xmm1, xmm0, xmm4			;shuffle G values to xmm1, just 4 pixels
	vcvtdq2ps	xmm1, xmm1					;convert int -> float
	vmulps		xmm1, xmm1, xmm7			;multiply 4 pixels G
	vpshufb		xmm0, xmm0, xmm5			;shuffle B values to xmm0, just 4 pixels
	vcvtdq2ps	xmm0, xmm0					;convert int -> float
	vmulps		xmm0, xmm0, xmm8			;multiply 4 pixels B
	vaddps		xmm0, xmm0, xmm1			;add G component to B
	vaddps		xmm0, xmm0, xmm2			;add R component to G+B
	vcvtps2dq	xmm0, xmm0					;convert float -> int
	vpshufb		xmm0, xmm0, xmm9			;shuffle grey values
	vmovdqu		xmmword ptr[rdx], xmm0		;store 4 processed pixels

	add			rbx, 12						;advance pointer to input 12 bytes (4 pixels)
	add			rdx, 12						;advance pointer to output 12 bytes (4 pixels)
	cmp			rbx, rax					;check if max address == current address
	jng			pixBatch4					;pointer still in range -> repeat
	ret
asmBlackAndWhite endp


asmBlurAndSharpening proc
	mov			rbx, rcx
	mov			rax, zero					;zero RAX
	mov			eax, dword ptr[r8+8]		;store image length in bytes in EAX
	add			rax, rbx					;RAX now holds max address in array

	vmovdqu		xmm3, xmmword ptr[maskRRRR]	;store constants in ragisters for faster access
	vmovdqu		xmm4, xmmword ptr[maskGGGG]
	vmovdqu		xmm5, xmmword ptr[maskBBBB]

pixBatch4:									;procedure loop
	vmovdqu		xmm0, xmmword ptr[rbx]		;load data to xmm (too much)

	;RRRRRRRRRRRRRRRRRRRRRRRRR
	;middle
	vpshufb		xmm2, xmm0, xmm3			;shuffle R values to xmm2, just 4 pixels
	vmulps		xmm2, xmm2, E
	
	;right
	add rbx, NEXT_PIX
	vmovdqu		xmm0, xmmword ptr[rbx]
	vpshufb		xmm6, xmm0, xmm3
	vmulps		xmm6, xmm6, F
	vaddps		xmm2, xmm2, xmm6
	sub rbx, NEXT_PIX
	vmovdqu		xmm0, xmmword ptr[rbx]

	;left
	sub rbx, NEXT_PIX
	vmovdqu		xmm0, xmmword ptr[rbx]
	vpshufb		xmm6, xmm0, xmm3
	vmulps		xmm6, xmm6, D
	vaddps		xmm2, xmm2, xmm6
	add rbx, NEXT_PIX
	vmovdqu		xmm0, xmmword ptr[rbx]

	;down
	add rbx, IMG_WIDTH
	vmovdqu		xmm0, xmmword ptr[rbx]
	vpshufb		xmm6, xmm0, xmm3
	vmulps		xmm6, xmm6, H
	vaddps		xmm2, xmm2, xmm6
	sub rbx, IMG_WIDTH
	vmovdqu		xmm0, xmmword ptr[rbx]

	;down-left
	add rbx, IMG_WIDTH
	sub rbx, NEXT_PIX
	vmovdqu		xmm0, xmmword ptr[rbx]
	vpshufb		xmm6, xmm0, xmm3
	vmulps		xmm6, xmm6, G
	vaddps		xmm2, xmm2, xmm6
	add rbx, NEXT_PIX
	sub rbx, IMG_WIDTH
	vmovdqu		xmm0, xmmword ptr[rbx]

	;down-right
	add rbx, IMG_WIDTH
	add rbx, NEXT_PIX
	vmovdqu		xmm0, xmmword ptr[rbx]
	vpshufb		xmm6, xmm0, xmm3
	vmulps		xmm6, xmm6, I
	vaddps		xmm2, xmm2, xmm6
	sub rbx, NEXT_PIX
	sub rbx, IMG_WIDTH
	vmovdqu		xmm0, xmmword ptr[rbx]

	;up
	sub rbx, IMG_WIDTH
	vmovdqu		xmm0, xmmword ptr[rbx]
	vpshufb		xmm6, xmm0, xmm3
	vmulps		xmm6, xmm6, B
	vaddps		xmm2, xmm2, xmm6
	add rbx, IMG_WIDTH
	vmovdqu		xmm0, xmmword ptr[rbx]

	;up-left
	sub rbx, IMG_WIDTH
	sub rbx, NEXT_PIX
	vmovdqu		xmm0, xmmword ptr[rbx]
	vpshufb		xmm6, xmm0, xmm3
	vmulps		xmm6, xmm6, A
	vaddps		xmm2, xmm2, xmm6
	add rbx, NEXT_PIX
	add rbx, IMG_WIDTH
	vmovdqu		xmm0, xmmword ptr[rbx]

	;up-right
	sub rbx, IMG_WIDTH
	add rbx, NEXT_PIX
	vmovdqu		xmm0, xmmword ptr[rbx]
	vpshufb		xmm6, xmm0, xmm3
	vmulps		xmm6, xmm6, C
	vaddps		xmm2, xmm2, xmm6
	sub rbx, NEXT_PIX
	add rbx, IMG_WIDTH
	vmovdqu		xmm0, xmmword ptr[rbx]

	vmovdqu		xmm9, xmmword ptr[maskRRev] ;load revert Red mask
	vpshufb		xmm2, xmm2, xmm9


	;GGGGGGGGGGGGGGGGGGGGG
	vpshufb		xmm1, xmm0, xmm4			;shuffle G values to xmm1, just 4 pixels
	vmulps		xmm1, xmm1, E
	
	;right
	add rbx, NEXT_PIX
	vmovdqu		xmm0, xmmword ptr[rbx]
	vpshufb		xmm6, xmm0, xmm4
	vmulps		xmm6, xmm6, F
	vaddps		xmm1, xmm1, xmm6
	sub rbx, NEXT_PIX
	vmovdqu		xmm0, xmmword ptr[rbx]

	;left
	sub rbx, NEXT_PIX
	vmovdqu		xmm0, xmmword ptr[rbx]
	vpshufb		xmm6, xmm0, xmm4
	vmulps		xmm6, xmm6, D
	vaddps		xmm1, xmm1, xmm6
	add rbx, NEXT_PIX
	vmovdqu		xmm0, xmmword ptr[rbx]

	;down
	add rbx, IMG_WIDTH
	vmovdqu		xmm0, xmmword ptr[rbx]
	vpshufb		xmm6, xmm0, xmm4
	vmulps		xmm6, xmm6, H
	vaddps		xmm1, xmm1, xmm6
	sub rbx, IMG_WIDTH
	vmovdqu		xmm0, xmmword ptr[rbx]
	
	;down-left
	add rbx, IMG_WIDTH
	sub rbx, NEXT_PIX
	vmovdqu		xmm0, xmmword ptr[rbx]
	vpshufb		xmm6, xmm0, xmm4
	vmulps		xmm6, xmm6, G
	vaddps		xmm1, xmm1, xmm6
	add rbx, NEXT_PIX
	sub rbx, IMG_WIDTH
	vmovdqu		xmm0, xmmword ptr[rbx]

	;down-right
	add rbx, IMG_WIDTH
	add rbx, NEXT_PIX
	vmovdqu		xmm0, xmmword ptr[rbx]
	vpshufb		xmm6, xmm0, xmm4
	vmulps		xmm6, xmm6, I
	vaddps		xmm1, xmm1, xmm6
	sub rbx, NEXT_PIX
	sub rbx, IMG_WIDTH
	vmovdqu		xmm0, xmmword ptr[rbx]

	;up
	sub rbx, IMG_WIDTH
	vmovdqu		xmm0, xmmword ptr[rbx]
	vpshufb		xmm6, xmm0, xmm4
	vmulps		xmm6, xmm6, B
	vaddps		xmm1, xmm1, xmm6
	add rbx, IMG_WIDTH
	vmovdqu		xmm0, xmmword ptr[rbx]

	;up-left
	sub rbx, IMG_WIDTH
	sub rbx, NEXT_PIX
	vmovdqu		xmm0, xmmword ptr[rbx]
	vpshufb		xmm6, xmm0, xmm4
	vmulps		xmm6, xmm6, A
	vaddps		xmm1, xmm1, xmm6
	add rbx, NEXT_PIX
	add rbx, IMG_WIDTH
	vmovdqu		xmm0, xmmword ptr[rbx]

	;up-right
	sub rbx, IMG_WIDTH
	add rbx, NEXT_PIX
	vmovdqu		xmm0, xmmword ptr[rbx]
	vpshufb		xmm6, xmm0, xmm4
	vmulps		xmm6, xmm6, C
	vaddps		xmm1, xmm1, xmm6
	sub rbx, NEXT_PIX
	add rbx, IMG_WIDTH
	vmovdqu		xmm0, xmmword ptr[rbx]

	vmovdqu		xmm9, xmmword ptr[maskGRev] ;load revert Green mask
	vpshufb		xmm1, xmm1, xmm9
	
	add rbx, 4
	sub rbx, 4

	;BBBBBBBBBBBBBBBBBBBBB
	vpshufb		xmm7, xmm0, xmm5			;shuffle B values to xmm0, just 4 pixels
	vmulps		xmm7, xmm7, E

	;right
	add rbx, NEXT_PIX
	vmovdqu		xmm0, xmmword ptr[rbx]
	vpshufb		xmm6, xmm0, xmm5
	vmulps		xmm6, xmm6, F
	vaddps		xmm7, xmm7, xmm6
	sub rbx, NEXT_PIX
	vmovdqu		xmm0, xmmword ptr[rbx]

	;left
	sub rbx, NEXT_PIX
	vmovdqu		xmm0, xmmword ptr[rbx]
	vpshufb		xmm6, xmm0, xmm5
	vmulps		xmm6, xmm6, D
	vaddps		xmm7, xmm7, xmm6
	add rbx, NEXT_PIX
	vmovdqu		xmm0, xmmword ptr[rbx]

	;down
	add rbx, IMG_WIDTH
	vmovdqu		xmm0, xmmword ptr[rbx]
	vpshufb		xmm6, xmm0, xmm5
	vmulps		xmm6, xmm6, H
	vaddps		xmm7, xmm7, xmm6
	sub rbx, IMG_WIDTH
	vmovdqu		xmm0, xmmword ptr[rbx]

	;down-left
	add rbx, IMG_WIDTH
	sub rbx, NEXT_PIX
	vmovdqu		xmm0, xmmword ptr[rbx]
	vpshufb		xmm6, xmm0, xmm5
	vmulps		xmm6, xmm6, G
	vaddps		xmm7, xmm7, xmm6
	add rbx, NEXT_PIX
	sub rbx, IMG_WIDTH
	vmovdqu		xmm0, xmmword ptr[rbx]

	;down-right
	add rbx, IMG_WIDTH
	add rbx, NEXT_PIX
	vmovdqu		xmm0, xmmword ptr[rbx]
	vpshufb		xmm6, xmm0, xmm5
	vmulps		xmm6, xmm6, I
	vaddps		xmm7, xmm7, xmm6
	sub rbx, NEXT_PIX
	sub rbx, IMG_WIDTH
	vmovdqu		xmm0, xmmword ptr[rbx]

	;up
	sub rbx, IMG_WIDTH
	vmovdqu		xmm0, xmmword ptr[rbx]
	vpshufb		xmm6, xmm0, xmm5
	vmulps		xmm6, xmm6, B
	vaddps		xmm7, xmm7, xmm6
	add rbx, IMG_WIDTH
	vmovdqu		xmm0, xmmword ptr[rbx]

	;up-left
	sub rbx, IMG_WIDTH
	sub rbx, NEXT_PIX
	vmovdqu		xmm0, xmmword ptr[rbx]
	vpshufb		xmm6, xmm0, xmm5
	vmulps		xmm6, xmm6, A
	vaddps		xmm7, xmm7, xmm6
	add rbx, NEXT_PIX
	add rbx, IMG_WIDTH
	vmovdqu		xmm0, xmmword ptr[rbx]

	;up-right
	sub rbx, IMG_WIDTH
	add rbx, NEXT_PIX
	vmovdqu		xmm0, xmmword ptr[rbx]
	vpshufb		xmm6, xmm0, xmm5
	vmulps		xmm6, xmm6, C
	vaddps		xmm7, xmm7, xmm6
	sub rbx, NEXT_PIX
	add rbx, IMG_WIDTH
	vmovdqu		xmm0, xmmword ptr[rbx]

	vmovdqu		xmm9, xmmword ptr[maskBRev] ;load revert Green mask
	vpshufb		xmm7, xmm7, xmm9
	
	add rbx, 4
	sub rbx, 4

	PADDQ 		xmm7, xmm1					;add green to blue
	PADDQ		xmm7, xmm2
	vmovdqu		xmmword ptr[rdx], xmm7		;store 4 processed pixels

	add			rbx, 12						;advance pointer to input 12 bytes (4 pixels)
	add			rdx, 12						;advance pointer to output 12 bytes (4 pixels)
	cmp			rbx, rax					;check if max address == current address
	jng			pixBatch4					;pointer still in range -> repeat
	ret
asmBlurAndSharpening endp


asmContrastAndBrightness proc
	nop ; insert magic here
	ret
asmContrastAndBrightness endp


asmSepia proc
	nop ; insert magic here
	ret
asmSepia endp


end