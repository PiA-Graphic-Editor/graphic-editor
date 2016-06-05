.data
;----------------------------------------------------------------------------------------------
;asmBlackAndWhite .data section
counter		dword 0;

vectRRRR	real4 0.2126, 0.2126, 0.2126, 0.2126
vectGGGG	real4 0.7152, 0.7152, 0.7152, 0.7152
vectBBBB	real4 0.0722, 0.0722, 0.0722, 0.0722

maskRRRR	byte 02h, 80h, 80h, 80h, 05h, 80h, 80h, 80h, 08h, 80h, 80h, 80h, 0Bh, 80h, 80h, 80h
maskGGGG	byte 01h, 80h, 80h, 80h, 04h, 80h, 80h, 80h, 07h, 80h, 80h, 80h, 0Ah, 80h, 80h, 80h
maskBBBB	byte 00h, 80h, 80h, 80h, 03h, 80h, 80h, 80h, 06h, 80h, 80h, 80h, 09h, 80h, 80h, 80h

maskGREY	byte 00h, 00h, 00h, 04h, 04h, 04h, 08h, 08h, 08h, 0Ch, 0Ch, 0Ch, 80h, 80h, 80h, 80h
;end of asmBlackAndWhite .data section
;----------------------------------------------------------------------------------------------

.code

asmBlackAndWhite proc
	mov			eax,  dword ptr[r8+8]		;store image length in bytes in EAX
pixBatch4:
	vmovdqu		xmm0, xmmword ptr[rcx]		;load data to xmm (too much)
	vpshufb		xmm2, xmm0, maskRRRR		;shuffle R values to xmm2, just 4 pixels
	vcvtdq2ps	xmm2, xmm2					;convert int -> float
	vmulps		xmm2, xmm2, vectRRRR		;multiply 4 pixels R
	vpshufb		xmm1, xmm0, maskGGGG		;shuffle G values to xmm1, just 4 pixels
	vcvtdq2ps	xmm1, xmm1					;convert int -> float
	vmulps		xmm1, xmm1, vectGGGG		;multiply 4 pixels G
	vpshufb		xmm0, xmm0, maskBBBB		;shuffle B values to xmm0, just 4 pixels
	vcvtdq2ps	xmm0, xmm0					;convert int -> float
	vmulps		xmm0, xmm0, vectBBBB		;multiply 4 pixels B
	vaddps		xmm0, xmm0, xmm1			;add G component to B
	vaddps		xmm0, xmm0, xmm2			;add R component to G+B
	vcvtps2dq	xmm0, xmm0					;convert float -> int
	vpshufb		xmm0, xmm0, maskGREY		;shuffle grey values
	vmovdqu		xmmword ptr[rdx], xmm0		;store 4 processed pixels

	add			rcx, 12						;advance pointer to input 12 bytes (4 pixels)
	add			rdx, 12						;advance pointer to output 12 bytes (4 pixels)
	add			counter, 12					;increment counter
	cmp			counter, eax				;check if bytes counted == bytes in image
	jng			pixBatch4					;counter not greater than size -> repeat
	ret
asmBlackAndWhite endp


asmBlurAndSharpening proc
	nop ; insert magic here
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