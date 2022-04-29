#include <stdio.h>
#include <stdlib.h>
#include <errno.h> 
#include <stack.h>

/*
FUNCTIONS:
    int isEmpty (struct StackNode* which);+
    int isFull();+
    void push (struct StackNode** which, int var);+
    int pop (struct StackNode** which);+
    int peek (struct StackNode* which);+
    void deleteAll (struct StackNode** which);+
    void display (struct StackNode** which);+

DECLARATION:
    struct StackNode* <name> = NULL;
*/

int main(){

	struct StackNode* stack1 = NULL;
	struct StackNode* stack2 = NULL;

    //Are they empty?

    printf("1st empty status: %d\n", isEmpty(stack1));
    printf("2nd empty status: %d\n\n", isEmpty(stack2));

    push(&stack1, 1);

    printf("\n1st empty status: %d\n", isEmpty(stack1));
    printf("2nd empty status: %d", isEmpty(stack2));

    printf("\n----------------------\n");
    
    //push elements
    push(&stack1, 2);
    push(&stack1, 3);
    push(&stack1, 4);
    push(&stack1, 5);

    printf("\n");

    display(&stack1);

    printf("\n----------------------\n");

    //view top element
    int a = peek(stack1);
    printf("received: %d\n", a);
    printf("there are %d in stack\n", count(&stack1));
    printf("----------------------\n");

    //pop one element
    a = pop(&stack1);
    printf("received: %d\n\n", a);

    display(&stack1);
    printf("there are %d in stack\n", count(&stack1));
    printf("----------------------\n");

    //delete all elements
    deleteAll(&stack1);
    printf("1st empty status: %d\n", isEmpty(stack1));

    display(&stack1);

    //stack underflow
    pop(&stack2); 
    pop(&stack1);

    printf("----------------------\n");


    //other tests
    push(&stack2,55);
    push(&stack2,56);
    push(&stack2,58);

    display(&stack2);

    pop(&stack2);

    display(&stack2);

    push(&stack2,59);

    display(&stack2);

    printf("empty status: %d\nthere are %d elements in stack\n\n", isEmpty(stack2), count(&stack2));
    deleteAll(&stack2);
    printf("after deleteAll empty status: %d\n", isEmpty(stack2));


    return 0;
}
