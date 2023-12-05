#include "main.h"
#include <stdbool.h>

BOOL DLL_EXPORT checkSudokuSolution(char sudoku[3][3]) {
    // Check for 'x' in the grid
    for (int row = 0; row < 3; ++row) {
        for (int col = 0; col < 3; ++col) {
            if (sudoku[row][col] == 'x') {
                return FALSE; // 'x' means the puzzle is incomplete
            }
        }
    }

    // Check rows and columns for duplicates
    for (int i = 0; i < 3; ++i) {
        bool rowCheck[3] = {0};
        bool colCheck[3] = {0};

        for (int j = 0; j < 3; ++j) {
            // Subtract '1' to convert char '1', '2', '3' to int 0, 1, 2
            int rowIndex = sudoku[i][j] - '1';
            int colIndex = sudoku[j][i] - '1';

            if (rowIndex < 0 || colIndex < 0 || rowIndex > 2 || colIndex > 2) {
                return FALSE; // Value out of bounds (not 1, 2, or 3)
            }

            if (rowCheck[rowIndex] || colCheck[colIndex]) {
                return FALSE; // Duplicate found
            }

            rowCheck[rowIndex] = true;
            colCheck[colIndex] = true;
        }
    }

    // For a 3x3 Sudoku, there is no need to check subgrids as they are effectively single cells.
    // If you were to scale this up to a standard 9x9 Sudoku, you would need to implement subgrid checks.

    return TRUE; // Sudoku is valid
}

extern "C" DLL_EXPORT BOOL APIENTRY DllMain(HINSTANCE hinstDLL, DWORD fdwReason, LPVOID lpvReserved)
{
    switch (fdwReason)
    {
    case DLL_PROCESS_ATTACH:
        // attach to process
        // return FALSE to fail DLL load
        break;

    case DLL_PROCESS_DETACH:
        // detach from process
        break;

    case DLL_THREAD_ATTACH:
        // attach to thread
        break;

    case DLL_THREAD_DETACH:
        // detach from thread
        break;
    }
    return TRUE; // succesful
}
