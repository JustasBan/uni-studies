package sudoku_solver;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.Scanner;

public class Sudoku {

    private int[][] sudoku = new int[9][9];
    private int calls=0;

    public Sudoku(String fileName){

        this.sudoku = scanedSudoku(fileName);
    }

    private int[][] scanedSudoku(String fileName) {
        int[][] result = new int[9][9];
        int i=0;

        try {
            File myObj = new File(fileName);
            Scanner myReader = new Scanner(myObj);
            while (myReader.hasNextLine()) {
              String data = myReader.nextLine();
              
              for (int j = 0; j < 9; j++) {
                  result[i][j] = Character.getNumericValue(data.charAt(j));
              }
              i++;
            }
            myReader.close();
        } catch (FileNotFoundException e) {
            System.out.println("An error occurred.");
            e.printStackTrace();
          }

        return result;
    }

    public void display() {
        for (int i = 0; i < 9; i++) {
            for (int j = 0; j < 9; j++) {
                System.out.print(this.sudoku[i][j]);

                if((j+1)%3==0)
                    System.out.print(" ");
            }
            System.out.print("\n");

            if((i+1)%3==0)
                    System.out.print("\n");
        }

        System.out.println("recursive calls: " + calls);
    }

    private boolean isValid(int row, int col, int num)
    {
        boolean result = true;

        //checking row:
        for (int i = 0; i < 9; i++) {
            if(sudoku[row][i]==num)
                result = false;
        }

        //checking collumn:
        for (int i = 0; i < 9; i++) {
            if(sudoku[i][col]==num)
                result = false;
        }

        //box parameters:
        int boxRow = row - row%3, boxCol = col - col%3;

        //checking box:
        for (int i = boxRow; i < boxRow+3; i++) {
            for (int j = boxCol; j < boxCol+3; j++) {
                if(sudoku[i][j] == num)
                    result = false;
            }
        }

        return result;
    }

    public boolean solveSudoku() {

        calls++;

        for (int row = 0; row < 9; row++) {
            for (int col = 0; col < 9; col++) {

                if(sudoku[row][col]==0){
                    for (int i = 1; i < 10; i++) {
                        
                        if(isValid(row, col, i)){
                            sudoku[row][col] = i;

                            if(solveSudoku()) //recursion starts here
                                return true;
                            else
                                sudoku[row][col] = 0;
                        }
                    }
                    return false;
                }
            }
        }

        return true;
        
    }
}
