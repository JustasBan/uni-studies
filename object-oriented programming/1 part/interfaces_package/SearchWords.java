package interfaces_package;

public interface SearchWords extends Existence{
    
    String[] similarWords(String[] dictionary, String input);
}
