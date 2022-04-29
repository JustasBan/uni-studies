#include <stdio.h>
#include <stdlib.h>
#include <errno.h>
#include <stdbool.h>
#include "queue.h"

#define MEMORY_NOT_ALLOCATED -1111111
#define MEMORY_ALLOCATED      1111111

#define QUEUE_IS_EMPTY -2222222
#define QUEUE_IS_OK     2222222

Queue_work new_queue()
{
    Queue_work q;
    q=(Queue_work)malloc(sizeof(Queue));

    if (q == NULL)
    {
        errno=MEMORY_NOT_ALLOCATED;
        return NULL;
    }

    q->front = NULL;
    q->rear = NULL;
    q->count=0;
    errno=MEMORY_ALLOCATED;

    return q;
}

Node_work new_node( my_type num)
{
    Node_work n;
    n=(Node_work)malloc(sizeof(QueueNode));

    if (n == NULL)
    {
        errno=MEMORY_NOT_ALLOCATED;
        return NULL;
    }

    n->value = num;
    n->next = NULL;
    errno=MEMORY_ALLOCATED;

    return n;
}

void enqueue( Queue_work q , my_type num)
{
    Node_work n = new_node(num);

    if ( errno == MEMORY_NOT_ALLOCATED) exit(1);

    q->count++;

    if (q->front == NULL)
    {
        q->front = n;
        q->rear = n;
    }
    else
    {
        q->rear->next = n;
        q->rear = q->rear->next;
    }

}

my_type dequeue( Queue_work q)
{
    Node_work temp;
    QueueNode n;
    my_type number;

    if (q->front != NULL)
    {
        temp = q->front;
        q->front = q->front->next;
        n = *temp;
        number = n.value;
        free(temp);

        q->count--;

        errno=QUEUE_IS_OK;
        return number;
    }
    else
    {
        errno=QUEUE_IS_EMPTY;
        return 0;
    }
}

my_type first_queue_node( Queue_work q)
{
    Node_work temp;
    QueueNode n;
    my_type number;

    if (q->front != NULL)
    {
        temp = q->front;
        q->front = q->front->next;
        n = *temp;
        number = n.value;

        errno=QUEUE_IS_OK;
        return number;
    }
    else
    {
        errno=QUEUE_IS_EMPTY;
        return 0;
    }
}

void delete_queue( Queue_work q)
{
    Node_work temp;
    while(q->front != NULL)
    {
        temp = q->front;
        q->front = q->front->next;
        free(temp);
    }
    free(q);
}

bool check_if_empty( Queue_work q)
{
    if (q->front == NULL)
    {
        return true;
    }
    else
    {
        return false;
    }
}

bool check_if_full( Queue_work q)
{
    Node_work n = new_node(0);
    free(n);

    if ( errno == MEMORY_NOT_ALLOCATED)
    {
        return true;
    }
    else
    {
        return false;
    }
}

int count_queue_nodes( Queue_work q)
{
    return q->count;
}
