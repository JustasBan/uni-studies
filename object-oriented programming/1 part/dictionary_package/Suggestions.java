package dictionary_package;

public class Suggestions {

   protected String[] dictionary;
   protected String input;
   
   //constructor
   public Suggestions(String[] dictionary, String input)
   {
      this.dictionary = dictionary;
      this.input = input;
   }

   //tikrina ar zodis jau yra zodyne, isvestinese klasese ieskos panasiu

   //PAKEISTI:
   //mazoji raide
   //grazinti jei nesutapo 0, jei sutapo ta zodi 
   public String[] similarWords(String[] dictionary, String input)
   {
      input = cleanWord(input);

      String[] arr = new String[1];
      arr[0] = "0";

      for (String string : dictionary) {
         if(string.equals(input))
            arr[0] = input;
      }

      return arr;
   } 

   //isvalo zodi nuo nereikalingu simboliu
   protected final String cleanWord(String word)
   {
      word = word.replace(",", "");
      word = word.replace(".", "");
      word = word.replace("?", "");
      word = word.replace("/", "");
      word = word.replace("\\", "");
      word = word.replace("*", "");
      word = word.replace("-", "");
      word = word.replace("+", "");
      word = word.replace("\t", "");
      word = word.replace("\n", "");
      word = word.replace("!", "");
      word = word.replace("@", "");
      word = word.replace("#", "");
      word = word.replace("$", "");
      word = word.replace("%", "");
      word = word.replace("^", "");
      word = word.replace("&", "");
      word = word.replace("(", "");
      word = word.replace(")", "");
      return word;
   }

   @Override
   public String toString()
   {
      return similarWords(this.dictionary, this.input)[0];
   }
   
}