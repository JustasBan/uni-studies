package interfaces;

public class FragmentWord extends Word implements FoundableAsAFragment{

    //tikrina ar toks zodis yra zodyne
    public boolean wordAsAFragmentExists(String[] dictionary){
        boolean result = false;

        for (String string : dictionary) {
            if(string.equals(inputWord))
               result = true;
         }

        return result;
    }

    //randa 10 panasiausiu zodziu
    public String[] similarWords(String[] dictionary) {
        
        String strings[];
    
        //tikrinu ar reikia ieskoti
        if(wordAsAFragmentExists(dictionary))
        {
            strings = new String[1];
            strings[0] = inputWord;
        }
        else
        {
            strings = new String[dictionary.length];
            int[] distances = new int[dictionary.length];

            //calculating
            for (int i = 0; i < dictionary.length; i++) {
                strings[i] = dictionary[i];
                distances[i] = distance(dictionary[i]);
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

        if(wordAsAFragmentExists(dictionary))
        {
            String[] result = strings;
            return result;
        }
        else
        {
            String[] result = new String[10];

            int i = 0;
            for (String string : strings) {
                if(string.contains(inputWord))
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
    public int distance(String compareWord) {

        String a = compareWord, b = inputWord;

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