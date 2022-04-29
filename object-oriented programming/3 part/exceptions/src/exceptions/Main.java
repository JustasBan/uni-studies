package exceptions;
/*
PROJEKTAS:
Protingas redaktorius

TIKSLAI:
Suprasti isimciu panaudojima: moketi isimtis
 -deklaruoti, 
 -issaukti
 -"gaudyti"  

UŽDUOTIS:
 -Apibrezti bazine isimties klase savo projektui, isvesta is Exception. 
 -Apibrezti isvestine isimties klase su patikslinanciaja informacija. 
 -Kitos klases metodams deklaruoti (throws) metamas isimtis ir esant neteisingam kreipiniui jas issaukti (throw). 
 -Testineje (main) programos klaseje gaudyti metamas isimtis, parūpinant vartotoja diagnostine informacija. 

*/

public class Main {
    public static void main(String[] args) throws Exception {
 
        //fields
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
            "zone"
        };
        Suggestion A = new Suggestion();

        //testing exception
        try {
            
            String input = "when";
            String[] words;
            String[] temp = {""};

            words = A.similarWords(dictionary, input);

            for (String string : words) {
                System.out.println(string);
            }

        } catch (Exception e) {
            System.out.println("Exception occured: ");
            e.printStackTrace();
            

        }

    }
    
}
