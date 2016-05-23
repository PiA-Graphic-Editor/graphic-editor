.data
.code

asmAddTwoDoubles proc
	vaddpd ymm0, ymm0, ymm1
	ret
asmAddTwoDoubles endp

asmAddFourDoubles proc
	vaddpd ymm0, ymm0, ymm1
	vaddpd ymm2, ymm2, ymm3
	vaddpd ymm0, ymm0, ymm2
	ret
asmAddFourDoubles endp

asmStructOperation proc
	nop
asmStructOperation endp

end