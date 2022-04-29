#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include "queue.h"
#include <stdbool.h>

/*
Bank (ADT: queue). 

Process:    1)clients come to the bank and go to free bank teller
            2)if all tellers are busy, clients have to wait in line(queue). 
            
objective:  explore, how should bank manage its workers:
            1)put more experienced tellers (they work faster)
            2)put more inexperienced tellers (they work slower). 
            
data: 
            1)number of tellers, 
            2)client's service time, 
            3)probability, that client will arive
                (galima realizacija: generuojamas atsitiktinis skaičius ir tikrinama, ar jis tenkina tam tikrą sąlygą). 
                
Results:    statistika kažkuriam (vartotojo nurodytam) laiko momentui:
            1)VIDUTINIS ir MAKSIMALUS kliento laukimo laikas, 
            2)vidutinis kasininko užimtumas (procentais), 
            3)maksimalus eilės dydis ir pan.

DATA FILE:
1)time stamp 2)number of tellers 3)service time 4)possibility in percent

Example:
1 2 3 40

*/

void readData(int *teller_number, int *client_service_time, int *client_probability, char file_name[], int *given_time){
    
    FILE* file = fopen (file_name, "r");

    fscanf (file, "%d", given_time);
    fscanf (file, "%d", teller_number);
    fscanf (file, "%d", client_service_time);
    fscanf (file, "%d", client_probability);

    fclose (file);   
}

int random_number(int min_num, int max_num)
{
    int result = 0;
    result = (rand() % (max_num - min_num)) + min_num;
    return result;
}

int areFreeTellers(int tellers_state[], int tellers_number)
{
    for (int i = 0; i < tellers_number; i++)
    {
        if(tellers_state[i]==0)
            return i;
    }

    return -1;
    
}

void assignTeller(int tellers_state[], int id, int service_time)
{
    tellers_state[id]= service_time;
}

void printTellersStatus(int tellers_state[], int tellers_number)
{
    for (int i = 0; i < tellers_number; i++)
    {
        printf("%d state: %d\t",i, tellers_state[i]);
    }
    printf("\n");
    
}

int willCome(int client_probability){

    int check = random_number(1, 100);

    if(check<=client_probability)
    {
        printf("%s\n", "atejo");
        return 1;
    }
    else
    {
        printf("%s\n", "neatejo");
        return 0;
    }
}

int main()
{
    //client variables
    int client_service_time=0, client_probability=0, n_clients=0, clients_done_waiting=0;

    Queue_work line = new_queue();
    int queue_max=-1;

    float clients_sum=0;
    int clients_max=-1;


    //teller variables
    int teller_number=0;
    int *tellers; //busyness
    int *tellers_state; //countdown

    //shared variables
    int time_given = 0;
    srand(time(NULL));

    readData(&teller_number, &client_service_time, &client_probability, ".\\duom.txt", &time_given);
    tellers = (int*) malloc(teller_number *  sizeof(int));
    tellers_state = (int*) malloc(teller_number * sizeof(int));

    for(int b=0; b<teller_number; b++)
    {
        tellers[b] = 0;
        tellers_state[b] = 0;
    }

    //going thru each time unit
    for (int i = 0; i < time_given; i++)
    {
        printf("\n%d: ", i);
        
        //NEW CLIENT CHECK
        if (willCome(client_probability) == 1)
        {
            //declare new client
            n_clients++;

            //put in line
            enqueue(line, i);

            //try to assign teller
            if(areFreeTellers(tellers_state, teller_number)>-1)
            {
                assignTeller(tellers_state, areFreeTellers(tellers_state, teller_number), client_service_time);
                
                int temp = dequeue(line); 
                int duration = 0;
                clients_sum += duration;

                if(duration>clients_max)
                    clients_max=duration;

                clients_done_waiting++;
            }

            //when no free tellers, stay in line
        }
        
        //TELLERS WORK
        for (int j = 0; j < teller_number; j++)
        {
            //tellers work
            if (tellers_state[j]>0)
            {
                tellers_state[j]--;
                tellers[j]++;
            }
        }

        //QUEUE AND TELLERS CHECK
        //if are free tellers and line has clients
        if(areFreeTellers(tellers_state, teller_number)>-1 && check_if_empty(line) == false)
        {
            assignTeller(tellers_state, areFreeTellers(tellers_state, teller_number), client_service_time);
            
            int temp = dequeue(line); 
            int duration = i-temp+1;
            clients_sum += duration;

            if(duration>clients_max)
                clients_max=duration;

            clients_done_waiting++;            
        }

        //check queue size
        if(queue_max<count_queue_nodes(line))
        {
            queue_max = count_queue_nodes(line);
        }
    }

    printf("\n\n");


    printf("Left in line: %d\n", count_queue_nodes(line));
    printf("Longest line of clients: %d\n\n", queue_max);
    float avg;
    avg = clients_sum/clients_done_waiting;
    printf("Avg. time in line: %.3f\n", avg);
    
    printf("Max. time in line: %d\n\n", clients_max);
    printf("Serviced clients: %d\n", clients_done_waiting);


    double sum=0;
    for (int j1 = 0; j1 < teller_number; j1++)
    {
        printf("%d teller worked: %.1f %% of the time\n", j1,(double) tellers[j1]/time_given*100);
    }

    free(tellers);
    free(tellers_state);
    delete_queue(line);

    return 0;
}


