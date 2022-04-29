package dictionary_package;

public class BasicSuggestions extends Suggestions{

    private boolean wordExists = false;
    
    //constructor
    public BasicSuggestions(String[] dictionary, String input) {
        super(dictionary, input);

        String temp = super.similarWords(dictionary, input)[0];
            
        if(temp.equals("0") == false)
            this.wordExists = true;
    }

    //metodas atstumui skaiciuoti
    private int distance(String wordOne, String WordTwo) {

        //algoritm from https://rosettacode.org/wiki/Levenshtein_distance#Java

       String a = wordOne, b = WordTwo;

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
    
    //suskaiciuoja atstumus tarp zodyno zodziu ir input zodz.
    //isrikiuoja didejimo tvarka pagal atstumus, atrenka 10 pirmu
    @Override
    public String[] similarWords(String[] dictionary, String input)
    {
        input = super.cleanWord(input);

        String strings[];
    
        if(this.wordExists)
        {
            strings = new String[1];
            strings[0] = "toks zodis yra";
        }
        else
        {
            strings = new String[dictionary.length];
            int[] distances = new int[dictionary.length];

            //calculating
            for (int i = 0; i < dictionary.length; i++) {
                strings[i] = dictionary[i];
                distances[i] = distance(dictionary[i], input);
            }

            //sorting
            for (int i = 0; i < dictionary.length-1; i++) {
                for (int j = 0; j < dictionary.length-i-1; j++) {
                    if(distances[j] > distances[j+1])
                    {
                        
                        String temp = strings[j];
                        strings[j] = strings[j+1];
                        strings[j+1] = temp;

                        int temp2 = distances[j];
                        distances[j] = distances[j+1];
                        distances[j+1] = temp2;
                    }
                }
            }
        }

        if(this.wordExists)
        {
            String[] result = strings;
            return result;
        }
        else
        {
            String[] result = new String[10];

            int i = 0;
            for (String string : strings) {
                if(string.contains(input))
                {
                    result[i] = string;
                    i++;
                }

                if(i>=10)
                    break;
            }
        
            return result;
        }
        
    }

    @Override
    public String toString()
    {
        return String.join(",", similarWords(dictionary, input));
    }

}