/* Justas Baniulis;  4 kursas;  1 grupė; 1.4, 6.4 variantai */

/* Keliai: */

kelias(marijampole, prienai, 30).
kelias(marijampole, kaunas, 60).

kelias(prienai, birstonas, 5).
kelias(prienai, trakai, 60).

kelias(kaunas, raseiniai, 50).
kelias(kaunas, prienai, 35).
kelias(kaunas, vilnius, 120).

kelias(trakai, vilnius, 20).

kelias(raseiniai, siauliai, 70).

kelias(siauliai, vilnius, 200).
kelias(siauliai, rekyva, 2).
kelias(siauliai, pamusis, 30).

kelias(rekyva, pamusis, 40).


/* 1.4 galimaNuvaziuoti(MiestasX, MiestasY, AtstumasL) */
galimaNuvaziuoti(MiestasX, MiestasY, AtstumasL) :-
    kelias(MiestasX, MiestasY, Atstumas),
    AtstumasL >= Atstumas.

galimaNuvaziuoti(MiestasX, MiestasY, AtstumasL) :-
    kelias(MiestasX, TarpinisMiestas, Atstumas),
    AtstumasL >= Atstumas,
    galimaNuvaziuoti(TarpinisMiestas, MiestasY, AtstumasL).

/* 1.4 Pavyzdžiai: */
/* 
    galimaNuvaziuoti(kaunas, pamusis, 80). - true.
    galimaNuvaziuoti(marijampole, vilnius, 50). - false.
*/





/* 
    6.4 
    Natūralieji skaičiai yra modeliuojami termais nul, s(nul), s(s(nul)),… (žr. paskaitos medžiagą). 
    Apibrėžkite predikatą: pirmasis skaičius dalus antrajam. 
*/

/* skaiciai dalus jei jie lygus po atimciu
   arba pats su savimi is karto  */
dalus(X, X).

/* atiminejame */
dalus(X, Y) :-
    atimtis(X, Y, Rezultatas),
    X \= Rezultatas,
    dalus(Rezultatas, Y).
    
/* atimtis po viena */
atimtis(X, nul, X).
atimtis(s(X), s(Y), Z) :- atimtis(X, Y, Z).

/* 6.4 Pavyzdžiai: */
/* 
    dalus(s(s(s(nul))), s(s(nul))). - false.
    dalus(s(s(s(nul))), s(nul)). - true.
*/