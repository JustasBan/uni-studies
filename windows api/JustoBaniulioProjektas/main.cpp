#if defined(UNICODE) && !defined(_UNICODE)
#define _UNICODE
#elif defined(_UNICODE) && !defined(UNICODE)
#define UNICODE
#endif

#include <tchar.h>
#include <windows.h>
#include "resource.h"

#define ID_SUDOKU_CELL1 101
#define ID_SUDOKU_CELL2 102
#define ID_SUDOKU_CELL3 103
#define ID_SUDOKU_CELL4 104
#define ID_SUDOKU_CELL5 105
#define ID_SUDOKU_CELL6 106
#define ID_SUDOKU_CELL7 107
#define ID_SUDOKU_CELL8 108
#define ID_SUDOKU_CELL9 109

#define ID_IMPORT_BUTTON 110
#define ID_FILEPATH_INPUT 111

#define ID_CHECK_SOLUTION_BUTTON 112

typedef BOOL (*LPFN_CHECKSOLUTION)(TCHAR[3][3]);

HBITMAP hBitmapCorrect = NULL;
HBITMAP hBitmapIncorrect = NULL;
HWND hwndImageControl = NULL;

HINSTANCE hInst;
LPFN_CHECKSOLUTION checkSudokuSolution;

LRESULT CALLBACK WindowProcedure (HWND, UINT, WPARAM, LPARAM);

TCHAR szClassName[] = _T("CodeBlocksWindowsApp");

BOOL readSudokuFromFile(LPCTSTR filePath, CHAR sudoku[3][3])
{
    HANDLE hFile = CreateFile(filePath, GENERIC_READ, FILE_SHARE_READ, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
    if (hFile == INVALID_HANDLE_VALUE)
    {
        MessageBox(NULL, _T("Failed to open the file"), _T("Error"), MB_ICONERROR);
        return FALSE;
    }

    CHAR buffer[20];
    DWORD bytesRead;
    BOOL readSuccess = ReadFile(hFile, buffer, sizeof(buffer) - 1, &bytesRead, NULL);
    buffer[bytesRead] = '\0';

    if (!readSuccess || bytesRead == 0)
    {
        MessageBox(NULL, _T("Failed to read from the file"), _T("Error"), MB_ICONERROR);
        return FALSE;
    }
    else
    {
        int row = 0, col = 0, index = 0;
        while (row < 3 && buffer[index] != '\0')
        {
            if (buffer[index] == '\r')
            {
                index++;
                continue;
            }
            if (buffer[index] == '\n')
            {
                row++;
                col = 0;
            }
            else
            {
                if (col < 3)
                {
                    sudoku[row][col] = buffer[index];
                    col++;
                }
            }
            index++;
        }

    }

    CloseHandle(hFile);
    return TRUE;
}

int WINAPI WinMain (HINSTANCE hThisInstance,
                    HINSTANCE hPrevInstance,
                    LPSTR lpszArgument,
                    int nCmdShow)
{
    hInst = hThisInstance;

    HMENU hMenu = LoadMenu(hInst, MAKEINTRESOURCE(ID_MENU));

    HINSTANCE hDll = LoadLibrary(_T("DLLs.dll"));

    if (!hDll)
    {
        MessageBox(NULL, _T("Failed to load DLL"), _T("Error"), MB_ICONERROR);
        return 1;
    }

    checkSudokuSolution = (LPFN_CHECKSOLUTION)GetProcAddress(hDll, "checkSudokuSolution");

    if (!checkSudokuSolution)
    {
        MessageBox(NULL, _T("Failed to find function in DLL"), _T("Error"), MB_ICONERROR);
        FreeLibrary(hDll);
        return 1;
    }

    HWND hwnd;
    MSG messages;
    WNDCLASSEX wincl;

    wincl.hInstance = hThisInstance;
    wincl.lpszClassName = szClassName;
    wincl.lpfnWndProc = WindowProcedure;
    wincl.style = CS_DBLCLKS;
    wincl.cbSize = sizeof (WNDCLASSEX);

    wincl.hIcon = LoadIcon (NULL, IDI_APPLICATION);
    wincl.hIconSm = LoadIcon (NULL, IDI_APPLICATION);
    wincl.hCursor = LoadCursor (NULL, IDC_ARROW);
    wincl.lpszMenuName = NULL;
    wincl.cbClsExtra = 0;
    wincl.cbWndExtra = 0;
    wincl.hbrBackground = (HBRUSH) COLOR_BACKGROUND;
    wincl.hIcon = LoadIcon(GetModuleHandle(NULL), MAKEINTRESOURCE(ID_ICON));
    wincl.hIconSm = LoadIcon(GetModuleHandle(NULL), MAKEINTRESOURCE(ID_ICON));

    if (!RegisterClassEx (&wincl))
        return 0;

    hwnd = CreateWindowEx (
               0,
               szClassName,
               _T("JustoBaniulioProjektas_3*3Sudoku"),
               WS_OVERLAPPEDWINDOW,
               CW_USEDEFAULT,
               CW_USEDEFAULT,
               544,
               650,
               HWND_DESKTOP,
               hMenu,
               hThisInstance,
               NULL
           );

    ShowWindow (hwnd, nCmdShow);

    while (GetMessage (&messages, NULL, 0, 0))
    {
        TranslateMessage(&messages);
        DispatchMessage(&messages);
    }

    FreeLibrary(hDll);
    if (hBitmapCorrect) DeleteObject(hBitmapCorrect);
    if (hBitmapIncorrect) DeleteObject(hBitmapIncorrect);

    return messages.wParam;
}

LRESULT CALLBACK WindowProcedure (HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam)
{
    switch (message)
    {
    case WM_DESTROY:
        PostQuitMessage (0);
        break;
    case WM_CREATE:
    {
        hBitmapIncorrect = (HBITMAP)LoadImage(hInst, _T("blogas.bmp"), IMAGE_BITMAP, 0, 0, LR_LOADFROMFILE);
        hBitmapCorrect = (HBITMAP)LoadImage(hInst, _T("geras.bmp"), IMAGE_BITMAP, 0, 0, LR_LOADFROMFILE);

        hwndImageControl = CreateWindow("STATIC", NULL, WS_VISIBLE | WS_CHILD | SS_BITMAP,
                                        10, 200, 300, 300, hwnd, NULL, hInst, NULL);

        if (hBitmapCorrect == NULL)
        {
            DWORD dwError = GetLastError();
            TCHAR szErrMsg[1024];
            wsprintf(szErrMsg, _T("Error loading BMP file: %d"), dwError);
            MessageBox(NULL, szErrMsg, _T("Error"), MB_ICONERROR);
        }

        if (hBitmapCorrect == NULL || hBitmapIncorrect == NULL)
        {
            MessageBox(hwnd, _T("Failed to load 'geras.bmp'"), _T("Error"), MB_ICONERROR);
        }

        for (int i = 0; i < 9; i++)
        {
            int x = 10 + (i % 3) * 35;
            int y = 10 + (i / 3) * 35;

            CreateWindowEx(
                0, "EDIT", "",
                WS_CHILD | WS_VISIBLE | WS_BORDER | ES_NUMBER | ES_CENTER,
                x, y, 30, 20,
                hwnd, (HMENU)(ID_SUDOKU_CELL1 + i), hInst, NULL);
        }

        CreateWindowEx(
            0, "EDIT", "sudoku.txt",
            WS_CHILD | WS_VISIBLE | WS_BORDER | ES_LEFT,
            10, 120, 200, 20,
            hwnd, (HMENU)ID_FILEPATH_INPUT, hInst, NULL);

        CreateWindow(
            "BUTTON", "Import Sudoku",
            WS_VISIBLE | WS_CHILD | BS_DEFPUSHBUTTON,
            220, 120, 100, 20,
            hwnd, (HMENU)ID_IMPORT_BUTTON, hInst, NULL);

        CreateWindow(
            "BUTTON", "Check Solution",
            WS_VISIBLE | WS_CHILD | BS_DEFPUSHBUTTON,
            10, 150, 120, 30,
            hwnd, (HMENU)ID_CHECK_SOLUTION_BUTTON, hInst, NULL);

        break;
    }
    case WM_COMMAND:
    {
        switch (LOWORD(wParam))
        {
        case ID_IMPORT_BUTTON:
        {
            TCHAR filePath[MAX_PATH];
            GetDlgItemText(hwnd, ID_FILEPATH_INPUT, filePath, MAX_PATH);

            CHAR sudoku[3][3];
            BOOL isRead = readSudokuFromFile(filePath, sudoku);

            if(!isRead)
            {
                break;
            }

            for (int i = 0; i < 9; i++)
            {
                int row = i / 3;
                int col = i % 3;

                HWND hwndInput = GetDlgItem(hwnd, ID_SUDOKU_CELL1 + i);
                if (sudoku[row][col] != 'x' && sudoku[row][col] != '\n' && sudoku[row][col] != '\r')
                {
                    CHAR buffer[2] = {sudoku[row][col], '\0'};
                    SetWindowText(hwndInput, buffer);
                    EnableWindow(hwndInput, FALSE);
                }
                else
                {
                    SetWindowText(hwndInput, "");
                    EnableWindow(hwndInput, TRUE);
                }
            }

            break;
        }
        case ID_CHECK_SOLUTION_BUTTON:
        {
            CHAR sudoku[3][3];
            for (int i = 0; i < 9; i++)
            {
                HWND hwndCell = GetDlgItem(hwnd, ID_SUDOKU_CELL1 + i);
                CHAR cellValue[2];
                GetWindowText(hwndCell, cellValue, 2);
                sudoku[i / 3][i % 3] = cellValue[0];
            }

            BOOL isSolutionCorrect = checkSudokuSolution(sudoku);

            SendMessage(hwndImageControl, STM_SETIMAGE,
                        (WPARAM)IMAGE_BITMAP,
                        (LPARAM)(isSolutionCorrect ? hBitmapCorrect : hBitmapIncorrect));

            break;
        }
        case ID_EXIT:
            PostQuitMessage(0);
            break;
        case ID_ABOUT:
            MessageBox(hwnd, _T("Sudoku is a logic-based, combinatorial number-placement puzzle. Rows and columns must not have duplicates."), _T("About Sudoku"), MB_OK);
            break;
        }
    }
    default:
        return DefWindowProc (hwnd, message, wParam, lParam);
    }

    return 0;
}
