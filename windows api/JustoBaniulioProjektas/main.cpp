#if defined(UNICODE) && !defined(_UNICODE)
#define _UNICODE
#elif defined(_UNICODE) && !defined(UNICODE)
#define UNICODE
#endif

#include <tchar.h>
#include <windows.h>
#include "resource.h"

#define IDC_SUDOKU_CELL1 101
#define IDC_SUDOKU_CELL2 102
#define IDC_SUDOKU_CELL3 103
#define IDC_SUDOKU_CELL4 104
#define IDC_SUDOKU_CELL5 105
#define IDC_SUDOKU_CELL6 106
#define IDC_SUDOKU_CELL7 107
#define IDC_SUDOKU_CELL8 108
#define IDC_SUDOKU_CELL9 109

#define IDC_IMPORT_BUTTON 110
#define IDC_FILEPATH_INPUT 111

#define IDC_CHECK_SOLUTION_BUTTON 112

typedef BOOL (*LPFN_CHECKSOLUTION)(TCHAR[3][3]);

HINSTANCE hInst;
LPFN_CHECKSOLUTION checkSudokuSolution;

/*  Declare Windows procedure  */
LRESULT CALLBACK WindowProcedure (HWND, UINT, WPARAM, LPARAM);

/*  Make the class name into a global variable  */
TCHAR szClassName[] = _T("CodeBlocksWindowsApp");

BOOL readSudokuFromFile(LPCTSTR filePath, CHAR sudoku[3][3])
{
    HANDLE hFile = CreateFile(filePath, GENERIC_READ, FILE_SHARE_READ, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
    if (hFile == INVALID_HANDLE_VALUE)
    {
        MessageBox(NULL, _T("Failed to open the file"), _T("Error"), MB_ICONERROR);
        return FALSE;
    }

    CHAR buffer[20]; // Adjusted buffer size to account for newlines and null terminator
    DWORD bytesRead;
    BOOL readSuccess = ReadFile(hFile, buffer, sizeof(buffer) - 1, &bytesRead, NULL);
    buffer[bytesRead] = '\0'; // Null-terminate the buffer

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

    // Load the DLL
    HINSTANCE hDll = LoadLibrary(_T("DLLs.dll"));
    if (!hDll)
    {
        MessageBox(NULL, _T("Failed to load DLL"), _T("Error"), MB_ICONERROR);
        return 1;
    }

    // Get function address
    checkSudokuSolution = (LPFN_CHECKSOLUTION)GetProcAddress(hDll, "checkSudokuSolution");

    if (!checkSudokuSolution)
    {
        MessageBox(NULL, _T("Failed to find function in DLL"), _T("Error"), MB_ICONERROR);
        FreeLibrary(hDll);
        return 1;
    }

    HWND hwnd;               /* This is the handle for our window */
    MSG messages;            /* Here messages to the application are saved */
    WNDCLASSEX wincl;        /* Data structure for the windowclass */

    /* The Window structure */
    wincl.hInstance = hThisInstance;
    wincl.lpszClassName = szClassName;
    wincl.lpfnWndProc = WindowProcedure;      /* This function is called by windows */
    wincl.style = CS_DBLCLKS;                 /* Catch double-clicks */
    wincl.cbSize = sizeof (WNDCLASSEX);

    /* Use default icon and mouse-pointer */
    wincl.hIcon = LoadIcon (NULL, IDI_APPLICATION);
    wincl.hIconSm = LoadIcon (NULL, IDI_APPLICATION);
    wincl.hCursor = LoadCursor (NULL, IDC_ARROW);
    wincl.lpszMenuName = NULL;                 /* No menu */
    wincl.cbClsExtra = 0;                      /* No extra bytes after the window class */
    wincl.cbWndExtra = 0;                      /* structure or the window instance */
    /* Use Windows's default colour as the background of the window */
    wincl.hbrBackground = (HBRUSH) COLOR_BACKGROUND;
    wincl.hIcon = LoadIcon(GetModuleHandle(NULL), MAKEINTRESOURCE(ID_ICON));
    wincl.hIconSm = LoadIcon(GetModuleHandle(NULL), MAKEINTRESOURCE(ID_ICON));

    /* Register the window class, and if it fails quit the program */
    if (!RegisterClassEx (&wincl))
        return 0;

    /* The class is registered, let's create the program*/
    hwnd = CreateWindowEx (
               0,                   /* Extended possibilites for variation */
               szClassName,         /* Classname */
               _T("JustoBaniulioProjektas_3*3Sudoku"),       /* Title Text */
               WS_OVERLAPPEDWINDOW, /* default window */
               CW_USEDEFAULT,       /* Windows decides the position */
               CW_USEDEFAULT,       /* where the window ends up on the screen */
               544,                 /* The programs width */
               375,                 /* and height in pixels */
               HWND_DESKTOP,        /* The window is a child-window to desktop */
               hMenu,                /*Menu */
               hThisInstance,       /* Program Instance handler */
               NULL                 /* No Window Creation data */
           );

    /* Make the window visible on the screen */
    ShowWindow (hwnd, nCmdShow);

    /* Run the message loop. It will run until GetMessage() returns 0 */
    while (GetMessage (&messages, NULL, 0, 0))
    {
        /* Translate virtual-key messages into character messages */
        TranslateMessage(&messages);
        /* Send message to WindowProcedure */
        DispatchMessage(&messages);
    }

    FreeLibrary(hDll);
    /* The program return-value is 0 - The value that PostQuitMessage() gave */
    return messages.wParam;
}


/*  This function is called by the Windows function DispatchMessage()  */

LRESULT CALLBACK WindowProcedure (HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam)
{
    switch (message)                  /* handle the messages */
    {
    case WM_DESTROY:
        PostQuitMessage (0);       /* send a WM_QUIT to the message queue */
        break;
    case WM_CREATE:
    {
        for (int i = 0; i < 9; i++)
        {
            int x = 10 + (i % 3) * 35;
            int y = 10 + (i / 3) * 35;

            CreateWindowEx(
                0, "EDIT", "",
                WS_CHILD | WS_VISIBLE | WS_BORDER | ES_NUMBER | ES_CENTER,
                x, y, 30, 20,
                hwnd, (HMENU)(IDC_SUDOKU_CELL1 + i), hInst, NULL);
        }

        CreateWindowEx(
            0, "EDIT", "",
            WS_CHILD | WS_VISIBLE | WS_BORDER | ES_LEFT,
            10, 120, 200, 20,
            hwnd, (HMENU)IDC_FILEPATH_INPUT, hInst, NULL);

        CreateWindow(
            "BUTTON", "Import Sudoku",
            WS_VISIBLE | WS_CHILD | BS_DEFPUSHBUTTON,
            220, 120, 100, 20,
            hwnd, (HMENU)IDC_IMPORT_BUTTON, hInst, NULL);

        CreateWindow(
            "BUTTON", "Check Solution",
            WS_VISIBLE | WS_CHILD | BS_DEFPUSHBUTTON,
            10, 150, 120, 30,
            hwnd, (HMENU)IDC_CHECK_SOLUTION_BUTTON, hInst, NULL);

        break;
    }
    case WM_COMMAND:
    {
        switch (LOWORD(wParam))
        {
        case IDC_IMPORT_BUTTON:
        {
            TCHAR filePath[MAX_PATH];
            GetDlgItemText(hwnd, IDC_FILEPATH_INPUT, filePath, MAX_PATH);

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

                HWND hwndInput = GetDlgItem(hwnd, IDC_SUDOKU_CELL1 + i);
                if (sudoku[row][col] != 'x' && sudoku[row][col] != '\n' && sudoku[row][col] != '\r')
                {
                    CHAR buffer[2] = {sudoku[row][col], '\0'};
                    SetWindowText(hwndInput, buffer);
                    EnableWindow(hwndInput, FALSE); // make it read-only if it's pre-filled
                }
                else
                {
                    SetWindowText(hwndInput, "");
                    EnableWindow(hwndInput, TRUE); // make it editable if it's empty
                }
            }

            break;
        }
        case IDC_CHECK_SOLUTION_BUTTON:
        {
            CHAR sudoku[3][3];
            for (int i = 0; i < 9; i++)
            {
                HWND hwndCell = GetDlgItem(hwnd, IDC_SUDOKU_CELL1 + i);
                CHAR cellValue[2];
                GetWindowText(hwndCell, cellValue, 2);
                sudoku[i / 3][i % 3] = cellValue[0];
            }

            BOOL isSolutionCorrect = checkSudokuSolution(sudoku);
            MessageBox(hwnd, isSolutionCorrect ? _T("Correct Solution!") : _T("Incorrect Solution!"), _T("Solution Check"), MB_OK);
            break;
        }
        case ID_EXIT:
            PostQuitMessage(0);
            break;
        case ID_ABOUT:
            MessageBox(hwnd, _T("Sudoku is a logic-based, combinatorial number-placement puzzle..."), _T("About Sudoku"), MB_OK);
            break;
        }
    }
    default:                      /* for messages that we don't deal with */
        return DefWindowProc (hwnd, message, wParam, lParam);
    }

    return 0;
}
