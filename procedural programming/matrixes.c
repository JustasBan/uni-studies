/*
Įvesti sveiką skaičių N
Įvesti N*N sveikų skaičių, kurie sudarys matricą.
Raskite visų matricos balno taškų koordinates:
     Matricos elementas vadinamas balno tašku, 
     jei jis yra mažiausias savo eilutėje ir didžiausias savo stulpelyje
     arba, atvirkščiai, didžiausias savo eilutėje ir mažiausias savo stulpelyje
*/

/*
Find "saddle" point of NxN matrix
*/

#include <stdio.h>
#include <stdlib.h>

void skaitymas(int *n, int ***matrica)
{
    
    char enter='\0';

    while (scanf("%d%c", n, &enter) == 0 || enter!='\n')
    {
        printf("Iveskite skaiciu is naujo: \n");
        scanf("%*[^\n]");
    }

    *matrica = malloc(*n * sizeof(int*));   //rezervuojame vietas matricai (eiluciu kiekis)

    for(int i=0; i<*n; i++)
        *(*matrica+i) = malloc(*n * sizeof(int)); //rezervuojame eiluteje vietas matricos stulpeliams
    
    for(int j=0; j<*n; j++)
    {
        for(int x=0; x<*n; x++)
        {
            scanf("%d", (*(*matrica+j)+x));
        }
    }

}

int main()
{
    int n=0;          //N skaicius
    int **matrica;    //deklaruojame adresa i adresus matricai

    skaitymas(&n, &matrica);    //perduodu adresus

    for(int j=0; j<n; j++)
    {
        for(int x=0; x<n; x++)
        {
            printf("%d ", *(*(matrica+j)+x));
        }

        printf("\n");
    }

    return 0;
}