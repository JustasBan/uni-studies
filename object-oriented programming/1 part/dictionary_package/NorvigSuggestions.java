package dictionary_package;

import java.util.*;

public class NorvigSuggestions extends Suggestions{

    private boolean wordExists = false;

    //constructor
    public NorvigSuggestions(String[] dictionary, String input) {
        super(dictionary, input);
        
        String temp = super.similarWords(dictionary, input)[0];
        
        if(temp.equals("0") == false)
            this.wordExists = true;
    }

    // sugeneruoja visus zodzius, kuriu atstumas lygu 1 
	protected ArrayList<String> generateWords(String word) {
		ArrayList<String> result = new ArrayList<String>();
		for(int i=0; i < word.length(); ++i) result.add(word.substring(0, i) + word.substring(i+1));
		for(int i=0; i < word.length()-1; ++i) result.add(word.substring(0, i) + word.substring(i+1, i+2) + word.substring(i, i+1) + word.substring(i+2));
		for(int i=0; i < word.length(); ++i) for(char c='a'; c <= 'z'; ++c) result.add(word.substring(0, i) + String.valueOf(c) + word.substring(i+1));
		for(int i=0; i <= word.length(); ++i) for(char c='a'; c <= 'z'; ++c) result.add(word.substring(0, i) + String.valueOf(c) + word.substring(i));
		return result;
	}

    // tikrina ar sugeneruoti zodziai egzistuoja zodyne,
    // "gerus" zodzius deda i masyva 
    @Override
    public String[] similarWords(String[] dictionary, String input)
    {
        input = super.cleanWord(input);

        if(this.wordExists)
        {
            String[] strings = new String[1];
            strings[0] = "toks zodis yra";

            return strings;
        }
        else
        {
            ArrayList<String> temp = generateWords(input);
            ArrayList<String> filteredWords = new ArrayList<String>();

            for (String string : temp) {
                if(Arrays.asList(dictionary).contains(string)){
                    filteredWords.add(string);
                }
            }
                    
            return filteredWords.toArray(new String[filteredWords.size()]);
        }

        
    }

    @Override
    public String toString()
    {
        return String.join(",", similarWords(dictionary, input));
    }

}