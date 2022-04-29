package exceptions;

//bazinė klasė
public class DictionaryException extends Exception{
    
    DictionaryException(String message){
        
        //parent constructor
        super(message);
    }
    
    DictionaryException(){
        super("Dictionary is empty or has empty words");
    }
}
