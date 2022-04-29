#include <stdio.h>
#include <stdlib.h>
#include <errno.h> 
#include <stack.h>


struct StackNode* newNode(my_type data)
{
    struct StackNode* node = (struct StackNode*) malloc (sizeof(struct StackNode));
    
    node -> data = data;
    node -> link = NULL;
    node -> nr = 0;
    return node;
}

int isEmpty(struct StackNode* which) //returns 1 if empty, 0 if not
{
    if(which == NULL)
    {
        return 1;
    }
    else
    {
        return 0;
    }
}

int isFull() //returns 0 if enough memorry, 1 if not enough
{
    struct StackNode* temp = (struct StackNode*) malloc (sizeof(struct StackNode));

    if(temp == NULL)
        return 1;
    else
        return 0;
}

int push(struct StackNode** which, int var) //adds "var" to specific stack. returns 2 if ssuccessful, -2 if not
{

    if(isFull()==0){
    struct StackNode* node = newNode(var);

    node -> nr = count(which);
    node -> link = *which;
    *which = node;
    
    return 2;
    }
    else
    {
        return -2;
    }
}

int pop(struct StackNode** which) //returns and takes top element out of stack
{
    if(*which == NULL)
    {
        return -1;
    }
    else{
    struct StackNode* temp = *which; 
    *which = (*which)->link;

    int done;
    done = temp -> data;
    free(temp);
    return done;}
}

int peek(struct StackNode* which) //returns top element of stack
{
    if (isEmpty(which))
    {
        return -1;
    }
    else{
        return which -> data;
    }
}

void deleteAll(struct StackNode** which) //erases stack elements
{
    while (isEmpty(*which) == 0)
    {
        struct StackNode* temp = *which; 
        *which = (*which)->link;

        free(temp);
    }  
}

void display(struct StackNode** which) //displays all stack's elements
{
    if(isEmpty(*which) == 1)
    {
        printf("\n");
        
    }
    else
    {
        struct StackNode* temp = *which; 

        while (isEmpty(temp) != 1)
        {
            printf("%d - %d\n", temp->nr, temp -> data);

            struct StackNode* temp2 = temp; 
            temp = temp->link;
        }
    }
    
}

int count(struct StackNode** which)
{
    struct StackNode* temp = *which; 
    int count = 0;

    while (isEmpty(temp) != 1)
    {
        count++;

        struct StackNode* temp2 = temp; 
        temp = temp->link;
    }

    return count;
}