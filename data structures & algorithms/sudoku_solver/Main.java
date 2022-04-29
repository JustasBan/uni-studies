package sudoku_solver;

public class Main{

    public static void main(String[] args) {
        
    String fileName = "file";
    Sudoku A = new Sudoku(fileName);    

    if(A.solveSudoku())
        A.display();
    else
        System.out.println("neissprendziau");
}
}

