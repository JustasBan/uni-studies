/** 
TEMA:
Protingas redaktorius

TIKSLAI:
-Sugebėti sudaryti klasių hierarchiją.
-Suprasti, kaip išvestinės klasės pasinaudoja bazinės klasės sąsaja. 
-Mokėti išplėsti išvestinės klasės funkcionalumą, lyginant su bazine klase. 
-Sugebėti, panaudojant metodų užklotį, "modifikuoti" bazinės klasės veikimą išvestinėje klasėje.3

UŽDUOTIS:
sudaryti klasių hierarchiją bent iš trijų paveldėjimo ryšiais susijusių klasių. 
Išvestinės klasės privalo:

-Pasinaudoti bazinės klasės konstruktoriumi super() bei super-metodu. -
-Turėti papildomų metodų ir laukų -
-Užkloti Object metodą toString() ir dar bent vieną metodą -
-Kitos klasės privalo pasinaudoti sukurtų klasių polimorfiniu elgesiu  -
(kviesti užklotus metodus bazinio tipo nuorodai) -



-Bazinė klasė privalo turėti metodų, kuriuos draudžiama užkloti (final) +
-visos projekto klasės privalo priklausyti bent 2 skirtingiems paketams ++
**/

import words_package.*;

import java.io.IOException;

import dictionary_package.*;

public class program {  
    public static void main(String[] args) throws IOException 
    { 
        Word A1 = new Word("bat");
        A1.println();

        Word A2 = new Word("cowssss");        
        A2.println();

    } 
} 
