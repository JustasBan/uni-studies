package dictionary_package;

/*
javac dictionary_package\Main.java
java dictionary_package.Main

PROJEKTAS:
Protingas Redaktorius 

REIKALAVIMAI:
sudaryti klasių hierarchiją bent iš trijų paveldėjimo ryšiais susijusių klasių. 
Išvestinės klasės privalo:
 -Pasinaudoti bazinės klasės konstruktoriumi super() bei super-metodu.
 -Turėti papildomų metodų ir laukų
 -Užkloti Object metodą toString() ir dar bent vieną metodą
 -Kitos klasės privalo pasinaudoti sukurtų klasių polimorfiniu elgesiu
  (kviesti užklotus metodus bazinio tipo nuorodai)
 -Bazinė klasė privalo turėti metodų, kuriuos draudžiama užkloti
 -Visos projekto klasės privalo priklausyti bent 2 skirtingiems paketams

PLANAS/PASIULYMAI/PAKEITIMAI:
    ar toks zodis yra - Base
    panasiausi zodziai "basic" metodu - derived
    panasiausi zodziai "Peter Norvig" metodu- derived

*/

public class Main{
    public static void main(String[] args) {

        //zodynas is 3000 zodziu
        String dictionary[ ] = {
            "a",
            "abandon",
            "ability",
            "able",
            "abortion",
            "about",
            "above",
            "abroad",
            "absence",
            "absolute",
            "absolutely",
            "absorb",
            "abuse",
            "academic",
            "youth",
            "zone"
        };
        

        String WordToCheck = "whe";
        //polymorphism
        Suggestions A = new Suggestions(dictionary, WordToCheck);
        System.out.println(A);

        System.out.println("--------------------------------");


        Suggestions B = new BasicSuggestions(dictionary, WordToCheck);
        System.out.println(B);

        System.out.println("--------------------------------");

        Suggestions C = new NorvigSuggestions(dictionary, WordToCheck);
        System.out.println(C);

    }
}
