; Kompiliuojame  (dosbox terminale): yasm sava1.asm -fbin -o sava1.com
; Paleidimas (dosbox terminale): sava1.com

%include 'yasmmac.inc'          ; Pagalbiniai makrosai
;------------------------------------------------------------------------
org 100h                        ; visos COM programos prasideda nuo 100h
                                ; Be to, DS=CS=ES=SS !

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
section .text                   ; kodas prasideda cia 

   startas:                     ; nuo cia vykdomas kodas

   mov ah, 09                   ; vardas, pavarde, kursas, grupe
   mov dx, pranesimas1
   int 0x21

   mov ah, 09                   ; paprasoma irasyti teksta
   mov dx, pranesimas2
   int 0x21

   mov ah, 0x0A                 ; iraso teksto eilute
   mov dx, buferisIvedimui
   int 0x21

   mov ah, 9                    ; nauja eilute
   mov dx, naujaEilute
   int 0x21

   ; antro, trecio ir priespaskutinio bitu suma
   mov bx,0
   mov bl, [buferisIvedimui+2]            ; bx <- pirmas simbolis
   and bx, 0x0040                         ; nepasikeicia tik antras, t.y priespaskutinis
   mov cl, 6
   shr bx, cl
   add word [bituSuma], bx

   mov ax, [bituSuma]
   macPutString crlf, ' suma: ', crlf, '$'    
   call procPutInt16
   
   ;-----------------------------------------------------------------------------------


  ; mov al, [buferisIvedimui+4]           ; AL <- trecias simbolis
  ; mov ah, [buferisIvedimui+7]           ; AH <- sestas simbolis
  ; mov [buferisIvedimui+4], ah           ; trecias simbolis <- AH
  ; mov [buferisIvedimui+7], al           ; sestas simbolis <- AL
  ; mov byte [buferisIvedimui+3], '-'     ; antras simbolis <- '-'

  ; mov bx,0
  ; mov bl, [buferisIvedimui+1]           ; bx <- kiek įvedėme baitų
  ; mov byte [buferisIvedimui+bx+3], 0x0a ; pridedame gale LF (CR jau ten yra) 
  ; mov byte [buferisIvedimui+bx+4], '$'  ; pridedame gale '$' tam, kad 9-ą funkcija galėtų atspausdinti  
  ; mov ah, 9
  ; mov dx, buferisIvedimui+2
  ; int 0x21

   ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

   ;macPutString 'Iveskite pirma skaiciu', crlf, '$'  ;;praso ivesti pirma skaiciu
   ;call procGetUInt16
   ;mov [sk1], ax
   ;macNewLine 

   ;macPutString 'Ivesk antra skaiciu', crlf, '$'  ;;praso ivesti antra skaiciu
   ;call procGetUInt16
   ;mov [sk2], ax
   ;macNewLine 
   
   ;macPutString 'Ivesk trecia skaiciu', crlf, '$'  ;;praso ivesti trecia skaiciu
   ;call procGetUInt16
   ;mov [sk3], ax
   ;macNewLine 

   mov ah, 0x4c                  ; tiesiog bagiame
   int 0x21
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;


%include 'yasmlib.asm'
section .data                   ; duomenys

   pranesimas1:
      db 'Justas Baniulis 1 kursas 1 grupe', 0x0D, 0x0A, '$'

   pranesimas2:
      db 'Iveskite tekstine eilute (nuo 8 iki 80 simboliu) ', 0x0D, 0x0A, '$'

   naujaEilute:
      db 0x0D, 0x0A, '$'
      
   buferisIvedimui:      
      db 0x52, 0x00, '**********************'

   bituSuma:
      dw 00

    sk1:
      dw 00
    sk2:
      dw 00
    sk3:
      dw 00

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
section .bss                    ; neinicializuoti duomenys  


