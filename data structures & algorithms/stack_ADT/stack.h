// 2015956
#ifndef JUSTO_B_STACK_H_INCLUDED
#define JUSTO_B_STACK_H_INCLUDED

typedef int my_type;

//structures for linked list stack ADT
typedef struct StackNode
{
    my_type data;
    struct node *link;
    int nr;
};

struct StackNode* newNode(my_type data); 


int isEmpty(struct StackNode* which); //returns 1 if empty, 0 if not
int isFull(); //returns 0 if enough memorry, 1 if not enough
int push(struct StackNode** which, int var);  //adds "var" to specific stack. returns 2 if ssuccessful, -2 if not
int pop(struct StackNode** which); //returns and takes top element out of stack. returns top element if ssuccessful, -1 if not
int peek(struct StackNode* which); //returns element out of stack. returns top element if ssuccessful, -1 if not
void deleteAll(struct StackNode** which); //erases stack elements
void display(struct StackNode** which); //displays all stack's elements. Prints '\n\' if empty
int count(struct StackNode** which);


#endif
