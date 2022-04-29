package interfaces_package;


public class Main {
    public static void main(String[] args) {

        String input = "whe";
        String dictionary[ ] = {
            "a",
            "yield",
            "you",
            "young",
            "your",
            "yours",
            "yourself",
            "youth",
            "zone"
        };
        
        Suggestions A = new Suggestions(dictionary, input);
        System.out.println(A);
    }
}
