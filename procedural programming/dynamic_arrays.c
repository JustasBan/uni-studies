//apibrezti dinamini masyva
//iskviesti funkcija, kuri tam masyvui priskiria visiem elementams 5
//atspausdinti masyva
//masyvo ilgi galima perduoti per funkcija

/* define dynamically allocated array
   call a function, which turns all array elements into 5
   print array */

#include <stdio.h>
#include <stdlib.h>

void penketai(int *masyvas, int n)
{
    for(int i=0; i<n; i++)      
    {                           
        *(masyvas+i)=5;
    }
}

int main()
{
    int n=10;           
    int *masyvas;       

    masyvas = calloc(n, sizeof(int));       

    for(int i=0; i<n; i++)                  
    {
        printf("%d. %d\n",i+1, *(masyvas+i));
    }

    putchar('\n');

    penketai(masyvas, n);   

    for(int i=0; i<n; i++)                  
    {
        printf("%d. %d\n",i+1, *(masyvas+i));
    }

    free(masyvas);          

    return 0;
}