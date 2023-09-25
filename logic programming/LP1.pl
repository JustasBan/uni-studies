/* Justas Baniulis;  4 kursas;  1 grupė; 4, 16, 37 ir 42 variantai */

/* asmuo(Vardas, Lytis, Amzius, Pomegis); */

asmuo(mariukas, vyras, 2, zaislai).
asmuo(nagliukas, vyras, 1, grojimas-smuiku).

asmuo(jonulis, vyras, 16, masinos).
asmuo(inga, moteris, 16, dainavimas).

asmuo(jonuke, moteris, 18, dainavimas).
asmuo(antanas, vyras, 20, dainavimas).

asmuo(petre, moteris, 36, mezgimas).
asmuo(petras, vyras, 40, masinos).

asmuo(kazlas, vyras, 70, ukis).
asmuo(kazle, moteris, 68, ukis).

asmuo(kazys, vyras, 70, masinos).
asmuo(kaziene, moteris, 68, mezgimas).

/* mama(Mama, Vaikas); */

mama(petre, jonulis).
mama(petre, jonuke).
mama(kazle, petras).
mama(jonuke, nagliukas).
mama(kaziene, petre).
mama(inga, mariukas).

/* pora(Vyras, Zmona); */

pora(petras, petre).
pora(kazlas, kazle).
pora(antanas, jonuke).
pora(kazys, kaziene).
pora(jonulis, inga).

/* UZDUOTYS */



/* 4. sunus(Sunus, TevasMama) - Pirmasis asmuo (Sunus) yra antrojo (TevasMama) sūnus; */

sunus(Sunus, Mama) :- mama(Mama, Sunus), asmuo(Sunus, vyras, _, _).
sunus(Sunus, Tevas) :- pora(Tevas, Mama), sunus(Sunus, Mama).

/* 4. Pavyzdžiai: */
/*    
    sunus(jonuke, petre). = false (jonuke yra dukra)
    sunus(jonulis, petre). = true (jonulis yra petres sunus)
    sunus(jonulis, kazle). = false (jonulis yra kazles anukas)
*/



/* 16. anukas(Anukas, SenelisSenele) - Pirmasis asmuo (Anukas) yra antrojo (SenelisSenele) anūkas (bet ne anūkė!); */

vienas_is_tevu(TevasMama, Vaikas) :- mama(TevasMama, Vaikas).
vienas_is_tevu(TevasMama, Vaikas) :- pora(TevasMama, Mama), mama(Mama, Vaikas).

anukas(Anukas, SenelisSenele) :- 
    asmuo(Anukas, vyras, _, _),
    asmuo(SenelisSenele, _, _, _),
    vienas_is_tevu(SenelisSenele, TevasMama),
    vienas_is_tevu(TevasMama, Anukas).

/* 16. Pavyzdžiai: */
/*    
    anukas(jonuke, kazle). = false (jonuke yra anuke)
    anukas(nagliukas, petras). = true (nagliukas yra petro anukas)
    anukas(nagliukas, kazle). = false (nagliukas yra kazles pro-anukas)
*/



/* 37.  vunderkindas(Kudikis) - Asmuo Kudikis „dar kūdikis, o jau groja (mėgsta groti) smuiku“; */

vunderkindas(Kudikis) :- asmuo(Kudikis, _, Amzius, grojimas-smuiku), Amzius < 5.

/* 37. Pavyzdžiai: */
/*    
    vunderkindas(jonuke). = false (jonuke per sena)
    vunderkindas(nagliukas). = true (nagliukas 1 metu ir groja smuiku)
    vunderkindas(mariukas). = true (nagliukas megsta zaislus)
*/



/* 42.  gera_pora(Asmuo1, Asmuo2) - Asmenys Asmuo1 ir Asmuo2 yra panašaus amžiaus ir turi tą patį pomėgį; */

gera_pora(Asmuo1, Asmuo2) :- 
    asmuo(Asmuo1, _, Amzius1, Pomegis1),
    asmuo(Asmuo2, _, Amzius2, Pomegis2),
    abs(Amzius1 - Amzius2) =< 5,
    Pomegis1 = Pomegis2,
    Asmuo1 \= Asmuo2.

/* 42. Pavyzdžiai: */
/*    
    gera_pora(jonuke, antanas). = true (jonuke ir antanas panasaus amziaus ir megsta dainuoti)
    gera_pora(jonulis, nagliukas). = false (per didelis amziaus skirtumas)
    gera_pora(jonulis, inga). = false (jonulis ir inga turi skirtingus pomegius)
*/
    