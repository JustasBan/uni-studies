package words_package;

public class Word     //calculating Levenshtein distances between input word and given word
{    
    //fields

    private static int counter = 0;

    private String word_one;
    //----------------------------------------------------------------

    //methods
    public int L_distance(Word inputWord) {

        //algoritm from https://rosettacode.org/wiki/Levenshtein_distance#Java

        inputWord = new Word(word_one);
        String b = inputWord.word_one, a = "word";

        a = a.toLowerCase();
        b = b.toLowerCase();
        // i == 0
        int [] costs = new int [b.length() + 1];
        for (int j = 0; j < costs.length; j++)
            costs[j] = j;
        for (int i = 1; i <= a.length(); i++) {
            // j == 0; nw = lev(i - 1, j)
            costs[0] = i;
            int nw = i - 1;
            for (int j = 1; j <= b.length(); j++) {
                int cj = Math.min(1 + Math.min(costs[j], costs[j - 1]), a.charAt(i - 1) == b.charAt(j - 1) ? nw : nw + 1);
                nw = costs[j];
                costs[j] = cj;
            }
        }
        
        return costs[b.length()];
    }
    
    private static int increaseCounter(){
        counter++;
        return counter;
    }

    public void println(){

        System.out.println(counter+"." + word_one + ";");
    }
    //----------------------------------------------------------------

    //no-argument constructor
    public Word() {
        this("");
    }

    //string constructor
    public Word(String word_one) {
        this.counter = increaseCounter();

        this.word_one = word_one;
    }
    
    //----------------------------------------------------------------

    //result and count getters

    static public int getCount() {
        
        return counter;
    }

    //word_one setter

    public void setWord_one(String a)
    {
        this.word_one = a;
    }
}