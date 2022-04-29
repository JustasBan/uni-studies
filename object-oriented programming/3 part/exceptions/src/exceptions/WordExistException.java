package exceptions;

//išvestinė klasė
public class WordExistException extends DictionaryException{
    
    private String wrongWord;
    
    WordExistException() {

        super("This word already exists in Dictionary");
    }

    

    //issiaugau zodi
    public void setWrongWord(String wrongWord) {
        this.wrongWord = wrongWord;
    }

    //grazina zodzio eiles numeri zodyne
    public int wordNumber(String[] dictionary)
    {
        int result = 0;

        for (int i = 0; i < dictionary.length; i++) {

            if(dictionary[i].equals(wrongWord))
                result = i;
        }

        return result;
    }

    
}
