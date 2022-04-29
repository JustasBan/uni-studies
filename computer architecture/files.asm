%include 'yasmmac.inc'          ; Pagalbiniai makrosai

;Programa išveda tik tas eilutes, kuriose:
;    antras laukas neturi raidžių ‘Z’              +
;    trečias laukas yra neneigiamas skaičius       +
;    ketvirto ir penkto laukų suma dalijasi iš 10.

; program reads .csv file and writes answers to result file
; read file is read from command line arg.
; print lines, which:
;     2nd field doesn't have 'Z'
;     3rd field number is non-negative
;     4th and 5th fields sum is divisable by 10

;------------------------------------------------------------------------
org 100h                        ; visos COM programos prasideda nuo 100h
                                ; Be to, DS=CS=ES=SS !

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
section .text                   ; kodas prasideda cia 

   startas:                     ; nuo cia vykdomas kodas

   macPutString 'Justas Baniulis 1 kursas 1 grupe', crlf, '$' 

   call filtravimas

   ; -------------------------------- Baigiame -----------------------------------
   exit 

; procedūra: 

filtravimas:

   push bp
   mov bp, sp
   push bx
   push ax
   push cx
   push dx
   push si
   push di

   mov si, 0

   macPutString 'Irasykite rezultatu failo vardą', crlf, '$'     ;iveda rez faila
   mov al, 128                  ; ilgiausia eilutė
   mov dx, rasymoFailas      
   call procGetStr              
   macNewLine 
   
   mov dx, rasymoFailas             ;atidaro rez faila
   call procFCreateOrTruncate
   mov [rasymo_deskr], bx
   jc .error_rez                    ;jei neatsidaro - klaida

   mov bl, [0x82]                   ;tikrina ar ivestas argc[]
   cmp bl, 0
   je .error_duom                   ;jei ne - klaida

   mov si, 0

   .ciklas_iki_0D:
      mov bl, [0x82+si]                 ;argc[] -> duom failas
      cmp bl, 0x0D
      je .pasiekta_0D
      mov [skaitymoFailas + si], bl
      inc si
      jmp .ciklas_iki_0D

   .pasiekta_0D:
      mov bl, 0
      mov [skaitymoFailas + si], bl
      mov si, 0
   

   mov dx, skaitymoFailas        ;atidaro duom faila
   call procFOpenForReading      
   jnc .tesiam1
   jc .error_duom                ;jei ne - klaida


   .error_duom:
      macPutString 'Klaida atidarant faila skaitymui', crlf, '$' ;duomenu failo klaida
      exit

   .error_rez:
      macPutString 'Klaida atidarant faila rasymui', crlf, '$'   ;rezultatu failo klaida
      exit

   .tesiam1:
      mov [skaitymo_deskr], bx

   mov si, 0
   mov di, 0
   mov dl, 0

   ;pirmos eilutes nuskaitymas ir atspausdinimas
   .iki_0D_pirm:
      mov bx, [skaitymo_deskr]

      call procFGetChar
      jnc .filtras_pirm

      macPutString 'Klaida skaitant faila', crlf, '$' 
      exit

      .filtras_pirm:
         cmp ax, 0   ;ar ne failo pab
         je .pab     ;jei taip -> baigiame

         cmp cl, 0x0D
         je .spaudinimas

         mov al, cl
         mov [buferis+si], al
         inc si  

         jmp .iki_0D_pirm

         .spaudinimas_pirm:
            cmp di, si
            je .atspausdinta_pirm


            mov bx, [rasymo_deskr]
            mov al, [buferis+di]

            call procFPutChar
            jnc .tesiam2_pirm

            macPutString 'Klaida rasant faila', crlf, '$' 
            jmp .pab

            .tesiam2_pirm:
            inc di
            cmp ax, 0
            jne .spaudinimas_pirm
            macPutString 'isnaudota atmintis', crlf, '$' 
            jmp .pab

            .atspausdinta_pirm:

               mov bx, [rasymo_deskr]
               mov al, 0x0A
               call procFPutChar
               jnc .tesiam4_pirm

               macPutString 'Klaida rasant faila', crlf, '$' 
               jmp .pab

               .tesiam4_pirm:
               cmp ax, 0
               jne .tesiam5_pirm

               macPutString 'isnaudota atmintis', crlf, '$' 
               jmp .pab

               .tesiam5_pirm:

               mov si, 0
               mov di, 0
               mov dl, 0
               jmp .iki_0D_pirm



   .tesiam6:
   mov si, 0
   mov di, 0
   mov dl, 0

   ;antros eilutes nuskaitymas, filtravimas ir atspausdinimas


   .iki_EOF:
      mov bx, [skaitymo_deskr]

      call procFGetChar
      jnc .filtras

      macPutString 'Klaida skaitant faila', crlf, '$' 
      exit

      .filtras:
         cmp ax, 0   ;ar ne failo pab
         je .pab     ;jei taip -> baigiame

         cmp cl, 0x0D
         je .kita_eil

         mov al, cl
         mov [buferis+si], al
         inc si  

         ;call procPutInt16
         ;   macNewLine

         cmp al, 0x3B
         jne .tesiam3
         inc dl

         .tesiam3:

         cmp dl, 0
         je .nr1_laukas

         cmp dl, 1
         je .nr2_laukas

         cmp dl, 2
         je .nr3_laukas

         cmp dl, 3
         je .nr4_laukas

         cmp dl, 4
         je .tesiamA

         .tesiamA:
         cmp dh, 0x01
         jne .nr5_laukas
         mov cx, [sk1]
         neg cx
         mov [sk1], cx
         jmp .nr5_laukas

         .nr1_laukas:
            jmp .iki_EOF

         .nr2_laukas:
            cmp al, 0x5A
            jne .iki_EOF

            mov al, [ar_spausd]
            inc al
            mov [ar_spausd], al

            jmp .iki_EOF

         .nr3_laukas:
         
            cmp al, 0x2D
            jne .iki_EOF

            mov al, [ar_spausd]
            inc al
            mov [ar_spausd], al

            jmp .iki_EOF

         .nr4_laukas:
            
            cmp al, 0x3B
            je .iki_EOF

            cmp al, 0x2D
            jne .tesiam8
            mov dh, 0x01
            jmp .iki_EOF

            .tesiam8:
            mov bx, 0x0A
            mov cl, al
            sub cl, 0x30
            
            mov ax, [sk1]
            push dx
            mul bx
            add al, cl
            mov [sk1], ax
            pop dx
         
            
            jmp .iki_EOF

         .nr5_laukas:
            
            cmp al, 0x3B
            je .iki_EOF

            cmp al, 0x2D
            jne .tesiamB
            mov dh, 0x02
            jmp .iki_EOF

            .tesiamB:
            mov bx, 0x0A
            mov cl, al
            sub cl, 0x30
            
            mov ax, [sk2]
            push dx
            mul bx
            add al, cl
            mov [sk2], ax
            pop dx

            jmp .iki_EOF

      .kita_eil:
         

         mov ax, [sk1]     ;pirmo lauko sk
         mov bx, [sk2]     ;antro lauko sk

         cmp dh, 0x02
         jne .tesiamC
         neg bx

         .tesiamC:

         add ax, bx

         mov bl, 0x0A
         div bl

         cmp ah, 0
         je .tesiam9

         mov al, [ar_spausd]
         inc al
         mov [ar_spausd], al

         .tesiam9:
         mov al, [ar_spausd]
         cmp al, 0
         je .spaudinimas

         mov di, si

         .spaudinimas:
            cmp di, si
            je .atspausdinta


            mov bx, [rasymo_deskr]
            mov al, [buferis+di]

            cmp al, 0x0A
            je .tesiam2

            call procFPutChar
            jnc .tesiam2

            macPutString 'Klaida rasant faila', crlf, '$' 
            jmp .pab

            .tesiam2:
            inc di
            cmp ax, 0
            jne .spaudinimas
            macPutString 'isnaudota atmintis', crlf, '$' 
            jmp .pab

            .atspausdinta:
               mov al, [ar_spausd]
               cmp al, 0
               ja .tesiam5

               mov bx, [rasymo_deskr]
               mov al, 0x0A
               call procFPutChar
               jnc .tesiam4

               macPutString 'Klaida rasant faila', crlf, '$' 
               jmp .pab

               .tesiam4:
               cmp ax, 0
               jne .tesiam5

               macPutString 'isnaudota atmintis', crlf, '$' 
               jmp .pab

               .tesiam5:
               mov al, 0
               mov [ar_spausd], al

               mov si, 0
               mov di, 0
               mov dl, 0
               mov dh, 0
               mov [sk1], al
               mov [sk2], al
               jmp .iki_EOF


   .pab:
      mov bx, [skaitymo_deskr]
      call procFClose
      
      mov bx, [rasymo_deskr]
      call procFClose

   pop di
   pop si
   pop dx
   pop cx
   pop ax
   pop bx
   pop bp

ret

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
%include 'yasmlib.asm'
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;



section .data                   ; duomenys

 skaitymoFailas:
    times 255 db 00
   
 rasymoFailas:
    times 255 db 00

 rasymo_deskr:
    times 255 db 00

 skaitymo_deskr:
    times 255 db 00

 buferis:
    times 255 db 00

 ar_spausd:
      dw 00  
 sk1:
      dw 00   
 sk2:
      dw 00  
 saug:
      dw 00 

    
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
section .bss                    ; neinicializuoti duomenys  


