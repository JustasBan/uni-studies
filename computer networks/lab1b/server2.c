#include <stdio.h>
#include <ctype.h>
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
#include <time.h>
#include "logger.h"

#define PORT "20003"
#define HOST "::1"
#define CLIENT_COUNT 2

// server port host

int main()
{

    printf("Serveris2 pradeda veikti...\n");

    /* ------------------------------------------------------------ */
    // getaddrinfo() parametrai

    struct addrinfo hints; // definuoja ka getaddrinfo() darys
    memset(&hints, 0, sizeof hints);

    hints.ai_family = AF_UNSPEC;     // naudosim arba IPv6 arba IPv4
    hints.ai_socktype = SOCK_STREAM; // TCP soketas
    hints.ai_flags = AI_PASSIVE;     // ideda hosto IP adresa

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
    // tada soketa ir deskriptoriu paruosiam naudojimui
    // + validacija kad bind() ir socket() be klaidu, jei yra klaida terminale ziurim

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

        // atiduodam deskr. ir soketa bind() f-jai
        if (bind(deskr, i->ai_addr, i->ai_addrlen) == -1)
        {
            close(deskr);
            perror("bind");
            continue;
        }
        else
        {
            printf("soketas prijungtas...\n");
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
    // ziurime ieskanciu prisijungimo
    // + validacija

    if (listen(deskr, CLIENT_COUNT) == -1)
    {
        printf("listen() klaida, uzdaroma programa...\n");
        exit(3);
    }
    else
    {
        printf("klausome...\n");
    }

    /* ------------------------------------------------------------ */
    // priimame nauja klienta ir gaunam nauja deskriptoriu

    int newDeskr;
    struct sockaddr_storage clientAddr;
    socklen_t clientAddrSize;

    clientAddrSize = sizeof clientAddr;

    if ((newDeskr = accept(deskr, (struct sockaddr *)&clientAddr, &clientAddrSize)) == -1)
    {
        printf("accept() klaida, uzdaroma programa...\n");
        exit(4);
    }
    else
    {
        printf("priimame klienta...\n");
    }

    /* ------------------------------------------------------------ */
    // gaunam zinutes

    logger_initFileLogger("log1.txt", 1024 * 1024, 5);
    LOG_INFO("Logging started.......");

    char buffer[100];

    for (int i = 0; i < 1; i = 0)
    {
        if (recv(newDeskr, buffer, sizeof buffer, 0) < 1)
        {
            LOG_FATAL("del recv(), uzdaroma programa...");
            exit(5);
        }
        else
        {
            LOG_INFO(buffer);

            if (buffer[0] == '0')
            {
                break;
            }

            strcpy(buffer, "");
        }
    }

    /* ------------------------------------------------------------ */
    // valymaisi

    close(newDeskr);
    close(deskr);
    freeaddrinfo(results);

    return 0;
}