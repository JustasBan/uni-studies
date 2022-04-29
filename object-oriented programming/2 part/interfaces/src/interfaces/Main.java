/*
PROJEKTAS:
Protingas redaktorius

UŽDUOTIS:
- Sukurti interfeisa, isreiskianti kurios nors parašytos klases funkcionavimo aspekta. 
- Interfeisa isplesti (extends) kitu. 
- si realizuoti abstrakciaja klase, o pastaraja - konkreciaja. 

*Atkreipti demesi, kad konkreciosios klases funkcionalumo panaudojimas turi remti kiek imanoma abstraktesne klase (interfeisu).
*Galite panaudoti jau egzistuojancia klasių hierarchija. 
*/
package interfaces;

public class Main {
    public static void main(String[] args) throws Exception {

        String input = "when";
        String dictionary[ ] = {
            "a",
            "abandon",
            "ability",
            "able",
            "abortion",
            "about",
            "above",
            "youth",
            "zone"
        };
        
        FragmentWord A = new FragmentWord();
        A.inputWord = input;

        for (String string : A.similarWords(dictionary)) {
            System.out.println(string);
        }

        System.out.println("---------------------");

        A.inputWord = "whe";
        for (String string : A.similarWords(dictionary)) {
            System.out.println(string);
            
        }
    }
}
