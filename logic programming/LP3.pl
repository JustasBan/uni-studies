/* Justas Baniulis;  4 kursas;  1 grupė;  1.11, 2.3, 3.7, 4.3 variantai */

/*
1.11
iterpti(S,K,R) - sąrašas R gautas į duotąjį skaičių sąrašą S, įterpus duotąjį skaičių K, kad K kaimynas iš kairės sąraše R būtų mažesnis, o iš dešinės - didesnis negu K. 

Pavyzdžiai:
iterpti([10,2,14,8,1], 13, R).
R = [10,2,13,14,8,1].

iterpti([10,2,14,8,1], 5, R).
R = [5,10,2,14,8,1].

iterpti([10,2,14,8,1], 15, R).
R = [10,2,14,8,1,15].
*/

/* tusciam sarasui idedame K */
%iterpti([], K, [K]) :- !.

/* sarasui, kurio pirmas elementas didesnis uz K, idedame pradzioje */
iterpti([Priekis_S|Galas_S], K, [K,Priekis_S|Galas_S]) :- 
    K < Priekis_S.

/* sarasui, kurio pirmas elementas mazesnis ir antras elementas didenis uz K, idedame tarp tu elementu, tuos elementus idedame i R*/
iterpti([Priekis1_s,Priekis2_S|Galas_S], K, [Priekis1_s,K,Priekis2_S|Galas_S]) :- 
    K > Priekis1_s, K < Priekis2_S.

/* tesiame darba */
iterpti([Priekis_S|Galas_S], K, [Priekis_S|Galas_R]) :-	
    iterpti(Galas_S, K, Galas_R).







/*
2.3
dubl_trigub(S,R) - sąrašas R gaunamas iš S, pastarojo teigiamus elementus pakartojant du kartus, o neteigiamus - tris kartus. 

Pavyzdžiai:
dubl_trigub([-3,2,0],R).
R = [-3,-3,-3,2,2,0,0,0].

dubl_trigub([1, 2, 3],R).
R = [1, 1, 2, 2, 3, 3]

dubl_trigub([-1, -2, -3],R).
R = [-1, -1, -1, -2, -2, -2, -3, -3, -3].
*/

/* tusciam sarasui nieko nedarome */
dubl_trigub([], []) :- !.

/* teigiamam elementui pridedame du kartus */
dubl_trigub([Priekis_S|Galas_S], [Priekis_S,Priekis_S|Galas_R]) :-
    Priekis_S > 0,
    dubl_trigub(Galas_S, Galas_R), !.

/* neigiamam elementui pridedame tris kartus */
dubl_trigub([Priekis_S|Galas_S], [Priekis_S,Priekis_S,Priekis_S|Galas_R]) :-
    Priekis_S =< 0,
    dubl_trigub(Galas_S, Galas_R), !.







/*
3.7
keisti(S,K,R) - duotas sąrašas S. Duotas sąrašas K, nusakantis keitinį ir susidedantis iš elementų pavidalo k(KeiciamasSimbolis, PakeistasSimbolis). R - rezultatas, gautas pritaikius sąrašui S keitinį K. 

Pavyzdžiai:
keisti([a,c,b],[k(a,x),k(b,y)],R).
R = [x,c,y].

keisti([a,c,b],[k(a,x),k(b,y),k(c,z)],R).
R = [x, z, y].

keisti([a,a,c],[k(a,x),k(b,y)],R).
R = [x, x, c].
*/

/* tusti sarasai */
keisti([], _, []).

/* jeigu S priekis turi ketini, tai R priekis bus keitiniu*/
keisti([Priekis_S|Galas_S], K, [Priekis_R|Galas_R]) :-
    keitimas(Priekis_S, K, Priekis_R), !,   %atkirta, jeigu randame keitini
    keisti(Galas_S, K, Galas_R).

/* jeigu S priekis NEturi ketinio, tai R priekis bus S priekis */
keisti([Priekis_S|Galas_S], K, [Priekis_S|Galas_R]) :-
    not_keitimas(Priekis_S, K),
    keisti(Galas_S, K, Galas_R).


/* tikriname ar elementas turi keitini: */
keitimas(Elementas, [k(Elementas, Keitinys)|_], Keitinys). % jeigu turi, tai grazinamas keitinys
keitimas(Elementas, [k(_, _)|Likutis], Keitinys) :- % jeigu neturi, ieskom toliau keitiniu sarase
    keitimas(Elementas, Likutis, Keitinys).


/* tikriname ar elementas NEturi keitinio */
not_keitimas(Elementas, K) :-
    keitimas(Elementas, K, _), !, fail. % randus keitini: atkirta ir false.
not_keitimas(_, _). % jeigu neradom, true











/*
4.3 suma(S1,S2,Sum) - S1 ir S2 yra skaičiai vaizduojami skaitmenų sąrašais. Sum - tų skaičių suma vaizduojama skaitmenų sąrašu. Pavyzdžiui:

suma([9,4,6,1,3,4],[2,8],Sum).
Sum = [9,4,6,1,6,2].
*/


/* apverciame sarasus, sudedame, apverciame rezultata */
suma(S1, S2, Sum) :-
    saraso_apvertimas(S1, Apverstas_S1),
    saraso_apvertimas(S2, Apverstas_S2),
    sudetis(Apverstas_S1, Apverstas_S2, 0, Rezultatas),
    saraso_apvertimas(Rezultatas, Sum).

/* saraso apvertimas */
saraso_apvertimas(Sarasas, ApverstasSarasas) :-
    saraso_apvertimas_helper(Sarasas, [], ApverstasSarasas).

/* paimam pirma ir dedam i acc prieki */
saraso_apvertimas_helper([], Apverstas, Apverstas).
saraso_apvertimas_helper([Pradzia|Galas], Acc, Apverstas) :-
    saraso_apvertimas_helper(Galas, [Pradzia|Acc], Apverstas).

/* tusciu sarasu suma yra tuscia */
sudetis([], [], 0, []) :- !.

/* kai abu sarasai tusti, bet turim pernesima */
sudetis([], [], Pernesimas, [Pernesimas]) :-
    Pernesimas > 0.

/* Jeigu S2 nebelieka skaiciu, naudojam jame 0  */ 
sudetis(Sarasas, [], Pernesimas, Suma) :-
    sudetis(Sarasas, [0], Pernesimas, Suma), !.

/* Jeigu S1 nebelieka skaiciu, naudojam jame 0  */ 
sudetis([], Sarasas, Pernesimas, Suma) :-
    sudetis([0], Sarasas, Pernesimas, Suma), !.

/* sudedame pradzias ir pernesima, perduodam nauja pernesima (sveikaja dali), pridedam liekana i prieki   */
sudetis([Pradzia_1|Galas_1], [Pradzia_2|Galas_2], Pernesimas, [Pradzia_rezultato|Galas_rezultato]) :-
    SkaitmenuSuma is Pradzia_1 + Pradzia_2 + Pernesimas,
    Pradzia_rezultato is SkaitmenuSuma mod 10,
    PernesimasNaujas is SkaitmenuSuma // 10,
    sudetis(Galas_1, Galas_2, PernesimasNaujas, Galas_rezultato).

