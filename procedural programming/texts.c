#include <stdio.h>
#include <stdlib.h> 
#include <string.h>

/*
find the longest word of given text line. Read from file that is written as command line arg.
*/

void isvalyti_eilute(char eilute[])
{
    for(int i=0; i<255; i++)
    {
        eilute[i] = '\0';
    }
}

void ilgiausias_eilutes_zodis(char duom_eilute[], char rez_eilute[])
{
    char zodis[255]={'\0'};
    int ilgis=0, didziausias=-1, pozicija=0;
    

    ilgis = strlen(duom_eilute);

    if(duom_eilute[ilgis-1]=='\n')
    {
        for(int i=0; i<ilgis; i++)
        {
            if(duom_eilute[i]==' ' || duom_eilute[i]=='\n')
            {
                if(duom_eilute[i]=='\n')
                    zodis[pozicija]='\n';

                if(strlen(zodis)>strlen(rez_eilute))
                {
                    strcpy(rez_eilute, zodis);
                }

                pozicija=0;
                isvalyti_eilute(zodis);
        }
        else
        {
            zodis[pozicija]=duom_eilute[i];
            pozicija++;
        }

        }
    }
    else
    {
        strcpy(rez_eilute, "Neradome new line simbolio, todel tai nera eilute");
        
    }
    
    
}

int main(int argc, char *argv[] ) {   

    char duom_vardas[] = "duom.txt", rez_vardas[] = "rez.txt", duom_eilute[255]={'\0'}, rez_eilute[255]={'\0'}, laikinas='\0';
    int a=0;
    
    FILE *duom_failas;
    if((duom_failas=fopen(duom_vardas,"r"))==NULL)
    {
        printf("vartotojo nurodyto duomenų failo nėra\n");
        exit(1);
    }

    FILE *rez_failas;
    if((rez_failas=fopen(rez_vardas,"w"))==NULL)
    {
        printf("Rezultatų failo negalima sukurti\n");
        exit(1);
    }
    
    while (fgets(duom_eilute, 256, duom_failas)!=NULL)
    {
        if(strlen(duom_eilute)==255)
        {
            while (laikinas!='\n' && a!=EOF)
            {
                a=fscanf(duom_failas,"%c", &laikinas);
            }
            laikinas='\0';
            printf("%c",'\n');
        }
       
        ilgiausias_eilutes_zodis(duom_eilute, rez_eilute);
        
        fprintf(rez_failas, "%s\n-----------\n", rez_eilute);

        isvalyti_eilute(duom_eilute);
        isvalyti_eilute(rez_eilute);
        
    }
        
    

    fclose(duom_failas);
    fclose(rez_failas);

    return 0;
}
