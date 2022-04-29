package interfaces_package;

public class Suggestions extends ExistenceCheck implements SearchWords{
    
    private String[] dictionary;
    private String input;

    public Suggestions(String[] dictionary, String input){
        this.input = input;
        this.dictionary = dictionary;
    }

    @Override
    public String toString(){
        if(wordExists(this.dictionary, this.input))
            return "egzistuoja";
        else
            return "ne";
    }

}
