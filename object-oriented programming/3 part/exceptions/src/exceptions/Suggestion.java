package exceptions;

public class Suggestion{

    //tikrina ar toks zodis yra zodyne
    public boolean wordExists(String[] dictionary, String inputWord){
        boolean result = false;

        for (String string : dictionary) {
            if(string.equals(inputWord))
               result = true;
         }

        return result;
    }

    private boolean isOrHaveEmpty(String[] dictionary){
        if(dictionary.length == 0)
            return true;
        
        for (String string : dictionary) {
            if(string.length() == 0)
                return true;
        }

        return false;
    }

    //randa 10 panasiausiu zodziu
    public String[] similarWords(String[] dictionary, String input) throws DictionaryException{
        
        String strings[];
    
        //tevines klases naudojimas:
        if(isOrHaveEmpty(dictionary)){

            DictionaryException e = new DictionaryException();
            
            throw e;

        }
        //vaikinės klasės naudojimas: 
        else{ if(wordExists(dictionary, input))
        {
            WordExistException e = new WordExistException();
            e.setWrongWord(input);
            System.out.println(e.wordNumber(dictionary) + "'as zodis");

            throw e;
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

    //zodziu atstumas
    public int distance(String wordOne, String WordTwo) {

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
}