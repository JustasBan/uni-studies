#include <stdio.h>

/* find maximum of array and make all elements equal to the largest element of that array */

void uzpildyti_didziausiu(int masyvas[], int* rodykle, int n)
{
    int did=0;

    for(int i=0; i<n; i++)
    {
        rodykle = &masyvas[i];          
        if(did<*rodykle)                
        {
            did=*rodykle;               
        }
        
    }

    for(int j=0; j<n; j++)
    {
        rodykle= &masyvas[j];           
        *rodykle=did;                   
    }
}

int main() {   
    
    int n=21;
    int masyvas[21]={-1,-2,-3,-4,-5,-6,-7,-8,-9,-10,0,1,2,3,4,5,6,7,8,9,10};
    int* rodykle;

    uzpildyti_didziausiu(masyvas, rodykle, n);

    for(int i=0; i<n; i++)
    {
        rodykle = &masyvas[i];          

        printf("%d;", *rodykle);        

    }

    return 0;
}
