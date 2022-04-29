//Rosita Raišuotytė
//Studento pažymėjimo nr. 2016026

/*
o	Sukurti tuščią eilę --- new_gueue()
o	Patikrinti, ar eilė --- tuščia  check_if_empty( Queue_work q)
o	Patikrinti, ar eilė --- pilna  check_if_full( Queue_work q)
o	Įdėti (angl. enqueue) naują elementą į eilę --- enqueue(Queue_work q , int num)
o	Išimti (angl. dequeue) elementą iš eilės --- dequeue(Queue_work q)
o	Gauti pirmo eilės elemento duomenis, neišimant jo iš eilės --- first_queue_node(Queue_work q)
o	Gauti eilės elementų skaičių --- count_queue_nodes( Queue_work q)
o	Sunaikinti eilę --- delete_queue(Queue_work q)
*/

/*
    Errno klaidų kodai:

    o Kad bandėm susikurt eilę arba pridėti elementą, bet mums neišskyrė atminties/išskyrė
    #define MEMORY_NOT_ALLOCATED -1111111
    #define MEMORY_ALLOCATED      1111111

    o Kad bandėm išimti elementą, bet mes turėjom tuščią eilę/viskas pavyko
    #define QUEUE_IS_EMPTY -2222222
    #define QUEUE_IS_OK     2222222

*/

#include <stdbool.h>

#ifndef QUEUE_H_INCLUDED
#define QUEUE_H_INCLUDED

typedef int my_type;

typedef struct node
{
    my_type value;
    struct node *next;
} QueueNode;

typedef QueueNode * Node_work;

typedef struct
{
    QueueNode *front;
    QueueNode *rear;
    int count;
} Queue;

typedef Queue * Queue_work;


Queue_work new_queue();
void enqueue( Queue_work , my_type );
my_type dequeue( Queue_work );
my_type first_queue_node( Queue_work );
void delete_queue( Queue_work );
int count_queue_nodes( Queue_work );
bool check_if_full( Queue_work q);
bool check_if_empty( Queue_work );


#endif
