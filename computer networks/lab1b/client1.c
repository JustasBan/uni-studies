#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <errno.h>
#include <string.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <netdb.h>
#include <arpa/inet.h>
#include <sys/wait.h>
#include <signal.h>

#define PORT "20001"
#define HOST "::1"

// client port host

int main()
{

    printf("Klientas1 pradeda veikti...\n");

    /* ------------------------------------------------------------ */
    // getaddrinfo() parametrai

    struct addrinfo hints; // definuoja ka getaddrinfo() darys
    memset(&hints, 0, sizeof hints);

    hints.ai_family = AF_UNSPEC;     // naudosim arba IPv6 arba IPv4
    hints.ai_socktype = SOCK_STREAM; // TCP soketas
    // hints.ai_flags = AI_PASSIVE; //ideda hosto IP adresa

    /* ------------------------------------------------------------ */
    //  gaunam adresu info su getaddrinfo()
    // + validacija, kad gaunam be klaidu, jei yra klaida terminale ziurim

    int initialSocket;
    struct addrinfo *results;

    if ((initialSocket = getaddrinfo(HOST, PORT, &hints, &results)) != 0)
    {
        printf("getaddrinfo() klaida: %s\n", gai_strerror(initialSocket));
        exit(1);
    }

    /* ------------------------------------------------------------ */
    // iteruojame per gautus adresus (getaddrinfo() rezultatas)
    // ir padarom desktriptoriu ant pirmo veikiancio soketo tolesniam darbui,
    // tada su deskriptoriumi jungiames prie soketo
    // + validacija kad bind() ir connect() be klaidu, jei yra klaida terminale ziurim

    struct addrinfo *i;
    int deskr;

    for (i = results; i != NULL; i->ai_next)
    {

        // deskriptoriaus dalis
        if ((deskr = socket(
                 i->ai_family,
                 i->ai_socktype,
                 i->ai_protocol)) == -1)
        {
            perror("socket");
            continue;
        }
        else
        {
            printf("deskriptorius gautas...\n");
        }

        // atiduodam deskr. ir jungiames
        if (connect(deskr, i->ai_addr, i->ai_addrlen) == -1)
        {
            close(deskr);
            perror("connect");
            continue;
        }
        else
        {
            printf("prisijungeme prie soketo...\n");
        }

        // atlikus sekmingai darbus, baigiam iteruoti - jau prisijungeme
        break;
    }

    /* ------------------------------------------------------------ */
    // valiodacija ar radom laisva adresa

    if (i == NULL)
    {
        printf("soketas neprisijunge, uzdaroma programa...\n");
        exit(2);
    }

    /* ------------------------------------------------------------ */
    // siunciam zinute

    char message_buffer[100];

    // lauksim input, veikima nutraukiame su '0'
    while (scanf("%s", message_buffer) == 0 || message_buffer[0] != '0')
    {
        if (send(deskr, message_buffer, strlen(message_buffer) + 1, 0) == -1)
        {
            close(deskr);
            strcpy(message_buffer, ""); // valom buferi kitam naudojimui
            perror("send");
        }
        else
        {
            /* ------------------------------------------------------------ */
            // gaunam zinute, nes ja issiutem ir galim tiketis ats

            printf("siunciame zinute soketui...\n");
            strcpy(message_buffer, ""); // valom buferi kitam naudojimui

            if (recv(deskr, message_buffer, sizeof message_buffer, 0) < 1)
            {
                printf("recv() klaida, uzdaroma programa...\n");
                exit(5);
            }
            else
            {
                printf("gavome: %s\n", message_buffer);
                strcpy(message_buffer, ""); // valom buferi kitam naudojimui
            }
        }
    }

    if (send(deskr, "0", strlen("0") + 1, 0) == -1)
    {
        close(deskr);
        perror("send");
    }

    /* ------------------------------------------------------------ */
    // valymaisi
    close(deskr);
    freeaddrinfo(results);

    return 0;
}