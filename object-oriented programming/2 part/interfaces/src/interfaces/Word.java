package interfaces;

abstract public class Word implements Foundable{

    public String inputWord;

    public boolean wordExists(String[] dictionary){
        boolean result = false;

        for (String string : dictionary) {
            if(string.equals(inputWord))
               result = true;
         }

        return result;
    }
}
