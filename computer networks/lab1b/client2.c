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

#define PORT "20002"
#define HOST "::1"

// client port host

int main()
{

    printf("Klientas2 pradeda veikti...\n");

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
    // gaunam zinute

    char buffer[100];

    for (int i = 0; i < 1; i = 0)
    {
        if (recv(deskr, buffer, sizeof buffer, 0) < 1)
        {
            printf("del recv(), uzdaroma programa...\n");
            exit(5);
        }
        else
        {
            printf("gavome: %s\n", buffer);

            if (buffer[0] == '0')
            {
                break;
            }
        }

        /* ------------------------------------------------------------ */
        // siunciam zinute

        for (int i = 0; i < strlen(buffer); i++)
        {
            buffer[i] = tolower(buffer[i]);
        }

        if (send(deskr, buffer, strlen(buffer) + 1, 0) == -1)
        {
            close(deskr);
            perror("send");
        }
        else
        {
            printf("siunciame zinute soketui...\n");
        }
    }

    /* ------------------------------------------------------------ */
    // valymaisi
    close(deskr);
    freeaddrinfo(results);

    return 0;
}