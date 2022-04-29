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

#define PORT3 "20003"
#define PORT2 "20002"
#define PORT1 "20001"

#define HOST "::1"
#define CLIENT_COUNT 2

// server port host

int main()
{

    printf("Serveris1 pradeda veikti...\n");

    /* ------------------------------------------------------------ */
    /* -----------------PIRMAS PORTAS------------------------------ */
    /* ------------------------------------------------------------ */

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

    if ((initialSocket = getaddrinfo(HOST, PORT1, &hints, &results)) != 0)
    {
        printf("1. getaddrinfo() klaida: %s\n", gai_strerror(initialSocket));
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
            printf("1. deskriptorius gautas...\n");
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
            printf("1. soketas prijungtas...\n");
        }

        // atlikus sekmingai darbus, baigiam iteruoti - jau prisijungeme
        break;
    }

    /* ------------------------------------------------------------ */
    // valiodacija ar radom laisva adresa

    if (i == NULL)
    {
        printf("1. soketas neprisijunge, uzdaroma programa...\n");
        exit(2);
    }

    /* ------------------------------------------------------------ */
    // ziurime ieskanciu prisijungimo
    // + validacija

    if (listen(deskr, CLIENT_COUNT) == -1)
    {
        printf("1. listen() klaida, uzdaroma programa...\n");
        exit(3);
    }
    else
    {
        printf("1. klausome...\n");
    }

    /* ------------------------------------------------------------ */
    // priimame nauja klienta ir gaunam nauja deskriptoriu

    int newDeskr;
    struct sockaddr_storage clientAddr;
    socklen_t clientAddrSize;

    clientAddrSize = sizeof clientAddr;

    if ((newDeskr = accept(deskr, (struct sockaddr *)&clientAddr, &clientAddrSize)) == -1)
    {
        printf("1. accept() klaida, uzdaroma programa...\n");
        exit(4);
    }
    else
    {
        printf("1. priimame klienta...\n");
    }

    /* ------------------------------------------------------------ */
    /* -----------------ANTRAS PORTAS------------------------------ */
    /* ------------------------------------------------------------ */

    /* ------------------------------------------------------------ */
    // getaddrinfo() parametrai

    struct addrinfo hints2; // definuoja ka getaddrinfo() darys
    memset(&hints2, 0, sizeof hints2);

    hints2.ai_family = AF_UNSPEC;     // naudosim arba IPv6 arba IPv4
    hints2.ai_socktype = SOCK_STREAM; // TCP soketas
    hints2.ai_flags = AI_PASSIVE;     // ideda hosto IP adresa

    /* ------------------------------------------------------------ */
    //  gaunam adresu info su getaddrinfo()
    // + validacija, kad gaunam be klaidu, jei yra klaida terminale ziurim

    int initialSocket2;
    struct addrinfo *results2;

    if ((initialSocket2 = getaddrinfo(HOST, PORT2, &hints2, &results2)) != 0)
    {
        printf("2. getaddrinfo() klaida: %s\n", gai_strerror(initialSocket2));
        exit(1);
    }

    /* ------------------------------------------------------------ */
    // iteruojame per gautus adresus (getaddrinfo() rezultatas)
    // ir padarom desktriptoriu ant pirmo veikiancio soketo tolesniam darbui,
    // tada soketa ir deskriptoriu paruosiam naudojimui
    // + validacija kad bind() ir socket() be klaidu, jei yra klaida terminale ziurim

    struct addrinfo *i2;
    int deskr2;

    for (i2 = results2; i2 != NULL; i2->ai_next)
    {
        // deskriptoriaus dalis
        if ((deskr2 = socket(
                 i2->ai_family,
                 i2->ai_socktype,
                 i2->ai_protocol)) == -1)
        {
            perror("socket");
            continue;
        }
        else
        {
            printf("2. deskriptorius gautas...\n");
        }

        // atiduodam deskr. ir soketa bind() f-jai
        if (bind(deskr2, i2->ai_addr, i2->ai_addrlen) == -1)
        {
            close(deskr2);
            perror("bind");
            continue;
        }
        else
        {
            printf("2. soketas prijungtas...\n");
        }

        // atlikus sekmingai darbus, baigiam iteruoti - jau prisijungeme
        break;
    }

    /* ------------------------------------------------------------ */
    // valiodacija ar radom laisva adresa

    if (i2 == NULL)
    {
        printf("2. soketas neprisijunge, uzdaroma programa...\n");
        exit(2);
    }

    /* ------------------------------------------------------------ */
    // ziurime ieskanciu prisijungimo
    // + validacija

    if (listen(deskr2, CLIENT_COUNT) == -1)
    {
        printf("2. listen() klaida, uzdaroma programa...\n");
        exit(3);
    }
    else
    {
        printf("2. klausome...\n");
    }

    /* ------------------------------------------------------------ */
    // priimame nauja klienta ir gaunam nauja deskriptoriu

    int newDeskr2;
    struct sockaddr_storage clientAddr2;
    socklen_t clientAddrSize2;

    clientAddrSize2 = sizeof clientAddr2;

    if ((newDeskr2 = accept(deskr2, (struct sockaddr *)&clientAddr2, &clientAddrSize2)) == -1)
    {
        printf("2. accept() klaida, uzdaroma programa...\n");
        exit(4);
    }
    else
    {
        printf("2. priimame klienta...\n");
    }

    /* ------------------------------------------------------------ */
    /* -----------------TRECIAS PORTAS----------------------------- */
    /* ------------------------------------------------------------ */

    /* ------------------------------------------------------------ */
    // getaddrinfo() parametrai

    struct addrinfo hints3; // definuoja ka getaddrinfo() darys
    memset(&hints3, 0, sizeof hints3);

    hints3.ai_family = AF_UNSPEC;     // naudosim arba IPv6 arba IPv4
    hints3.ai_socktype = SOCK_STREAM; // TCP soketas
    //hints3.ai_flags = AI_PASSIVE;     // ideda hosto IP adresa

    /* ------------------------------------------------------------ */
    //  gaunam adresu info su getaddrinfo()
    // + validacija, kad gaunam be klaidu, jei yra klaida terminale ziurim

    int initialSocket3;
    struct addrinfo *results3;

    if ((initialSocket3 = getaddrinfo(HOST, PORT3, &hints3, &results3)) != 0)
    {
        printf("3. getaddrinfo() klaida: %s\n", gai_strerror(initialSocket3));
        exit(1);
    }

    /* ------------------------------------------------------------ */
    // iteruojame per gautus adresus (getaddrinfo() rezultatas)
    // ir padarom desktriptoriu ant pirmo veikiancio soketo tolesniam darbui,
    // tada soketa ir deskriptoriu paruosiam naudojimui
    // + validacija kad bind() ir socket() be klaidu, jei yra klaida terminale ziurim

    struct addrinfo *i3;
    int deskr3;

    for (i3 = results3; i3 != NULL; i3->ai_next)
    {
        // deskriptoriaus dalis
        if ((deskr3 = socket(
                 i3->ai_family,
                 i3->ai_socktype,
                 i3->ai_protocol)) == -1)
        {
            perror("socket");
            continue;
        }
        else
        {
            printf("3. deskriptorius gautas...\n");
        }

        // atiduodam deskr. ir jungiames
        if (connect(deskr3, i3->ai_addr, i3->ai_addrlen) == -1)
        {
            close(deskr3);
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

    if (i3 == NULL)
    {
        printf("3. soketas neprisijunge, uzdaroma programa...\n");
        exit(2);
    }

    /* ------------------------------------------------------------ */
    /* ------------------------------------------------------------ */
    /* ------------------------------------------------------------ */
    // gaunam zinute

    char buffer[200], buffer2[200];
    char *message;

    for (int i = 0; i < 1; i = 0)
    {
        if (recv(newDeskr, buffer, sizeof buffer, 0) < 1)
        {
            printf("recv() klaida, uzdaroma programa...\n");
            exit(5);
        }
        else
        {
            strcpy(buffer2, buffer);
            printf("1. gavome: %s\n", buffer);

            if (buffer[0] == '0')
            {
                break;
            }
            else
            {
                // siunciame S2
                message = malloc(strlen("K1 atsiunte S1: ") + strlen(buffer) + 1);
                message[0] = '\0';

                strcat(message, "K1 atsiunte S1: ");
                strcat(message, buffer);

                if (send(deskr3, message, strlen(message) + 1, 0) == -1)
                {
                    close(deskr3);
                    perror("send");
                }
                else
                {
                    printf("3. S2 siunciame %s\n", message);
                }
                free(message);
            }
        }

        /* ------------------------------------------------------------ */
        // siunciam zinute didziosiomis K1

        for (int i = 0; i < strlen(buffer); i++)
        {
            buffer[i] = toupper(buffer[i]);
        }

        if (send(newDeskr, buffer, strlen(buffer) + 1, 0) == -1)
        {
            close(newDeskr);
            perror("send");
        }
        else
        {
            // siunciame S2
            message = malloc(strlen("S1 nusiunte K1: ") + strlen(buffer) + 1);
            message[0] = '\0';

            strcat(message, "S1 nusiunte K1: ");
            strcat(message, buffer);

            if (send(deskr3, message, strlen(message) + 1, 0) == -1)
            {
                close(deskr3);
                perror("send");
            }
            else
            {
                printf("3. S2 siunciame %s\n", message);
            }
            free(message);

            printf("1. siunciame zinute soketui...\n");
        }

        /* ------------------------------------------------------------ */
        // siunciam zinute K2

        if (send(newDeskr2, buffer2, strlen(buffer2) + 1, 0) == -1)
        {
            close(newDeskr2);
            perror("send");
        }
        else
        {
            // siunciame S2
            message = malloc(strlen("S1 issiunte K2: ") + strlen(buffer) + 1);
            message[0] = '\0';

            strcat(message, "S1 issiunte K2: ");
            strcat(message, buffer2);

            if (send(deskr3, message, strlen(message) + 1, 0) == -1)
            {
                close(deskr3);
                perror("send");
            }
            else
            {
                printf("3. S2 siunciame %s\n", message);
            }
            free(message);

            printf("2. siunciame zinute soketui...\n");
        }

        /* ------------------------------------------------------------ */
        // gaunam zinute mazosiomis is K2
        if (recv(newDeskr2, buffer2, sizeof buffer2, 0) < 1)
        {
            printf("recv() klaida, uzdaroma programa...\n");
            exit(5);
        }
        else
        {
            // siunciame S2
            message = malloc(strlen("K2 atsiunte S1: ") + strlen(buffer2) + 1);
            message[0] = '\0';

            strcat(message, "K2 atsiunte S1: ");
            strcat(message, buffer2);

            if (send(deskr3, message, strlen(message) + 1, 0) == -1)
            {
                close(deskr3);
                perror("send");
            }
            else
            {
                printf("3. S2 siunciame %s\n", message);
            }
            free(message);

            printf("2. gavome: %s\n", buffer2);
        }

        strcpy(buffer2, "");
        strcpy(buffer, "");
    }

    if (send(newDeskr2, "0", strlen("0") + 1, 0) == -1)
    {
        close(deskr2);
        perror("send");
    }

    if (send(deskr3, "0", strlen("0") + 1, 0) == -1)
    {
        close(deskr3);
        perror("send");
    }

    /* ------------------------------------------------------------ */
    // valymaisi

    close(deskr);
    close(deskr2);
    close(newDeskr2);
    close(newDeskr);
    close(deskr3);
    close(deskr3);
    freeaddrinfo(results);

    return 0;
}