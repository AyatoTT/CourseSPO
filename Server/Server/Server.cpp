#include <iostream>
#include <winsock2.h>
#include <vector>
#include <thread>

#pragma comment(lib, "ws2_32.lib")

struct User {
    SOCKET clientSocket;
    char login[32] = "";
};

std::vector<User*> users;

void HandleClient(User* user) {
    char buffer[1024];
    while (true) {
        memset(buffer, 0, sizeof(buffer));
        int bytesRead = recv(user->clientSocket, buffer, sizeof(buffer), 0);

        std::string command(buffer);
        if (command.find("message|") == 0) {
            std::string message = command.substr(8);
            std::cout << "Сообщение -> " << message << std::endl;

            send(user->clientSocket, buffer, bytesRead, 0);

            for (const User* otherUser : users) {
                if (otherUser->clientSocket != user->clientSocket) {
                    send(otherUser->clientSocket, buffer, bytesRead, 0);
                }
            }
        }
        else if (command.find("messageto|") == 0) {
            const char* prefix = "messageto|";
            const size_t prefixLength = strlen(prefix);
            const char* dataStart = buffer + prefixLength;
            char request[1024];
            strcpy_s(request, dataStart);
            size_t lastPipeIndex = command.find_last_of('|');
            size_t firstPipeIndex = command.find('|');
            size_t secondPipeIndex = command.find('|', firstPipeIndex + 1);
            std::string login = command.substr(command.find('|') + 1, secondPipeIndex - command.find('|') - 1);
            std::string message = command.substr(lastPipeIndex + 1);

            for (const User* otherUser : users) {
                if (otherUser->login == login) {
                    send(otherUser->clientSocket, buffer, bytesRead, 0);
                }
            }

            sprintf_s(buffer, "%s%s", "messagefr|", request);
            send(user->clientSocket, buffer, bytesRead, 0);
        }
        else if (command.find("login|") == 0) {
            std::string loginInfo = command.substr(6);
            bool loginExists = false;
            for (const User* otherUser : users) {
                if (strcmp(otherUser->login, loginInfo.c_str()) == 0) {
                    loginExists = true;
                    break;
                }
            }
            if (loginExists) {
                send(user->clientSocket, "login_error|", strlen("login_error|"), 0);
            }
            else {
                strncpy_s(user->login, loginInfo.c_str(), sizeof(user->login) - 1);
                std::cout << "Вошёл: " << user->login << std::endl;
                send(user->clientSocket, "login_success|", strlen("login_success|"), 0);
                sprintf_s(buffer, "%s%s", "getusers|", user->login);
                int len_buffer = strlen(buffer);
                for (const User* otherUser : users) {
                    if (otherUser->clientSocket != user->clientSocket) {
                        if (strlen(otherUser->login) != 0) {
                            send(otherUser->clientSocket, buffer, len_buffer, 0);
                        }
                    }
                }
            }
        }
        else if (command.find("getusers|") == 0) {
            for (const User* otherUser : users) {
                if (otherUser->clientSocket != user->clientSocket) {
                    if (strlen(otherUser->login) != 0) {
                        sprintf_s(buffer, "%s%s", "getusers|", otherUser->login);
                        send(user->clientSocket, buffer, strlen(buffer), 0);
                    }
                }
            }
        }
        else if (command.find("disconnect|") == 0) {
            closesocket(user->clientSocket);

            for (const User* otherUser : users) {
                if (otherUser->clientSocket != user->clientSocket) {
                    if (strlen(otherUser->login) != 0) {
                        sprintf_s(buffer, "%s%s", "disconnect|", user->login);
                        send(otherUser->clientSocket, buffer, strlen(buffer), 0);
                    }
                }
            }

            std::cout << "Отключился: " << user->login << std::endl;

            auto it = std::find_if(users.begin(), users.end(),
                [user](const User* u) { return u->clientSocket == user->clientSocket; });

            if (it != users.end()) {
                users.erase(it);
            }

            break;
        }
    }
}

int main() {
    SetConsoleOutputCP(CP_UTF8);
    WSADATA wsaData;
    WSAStartup(MAKEWORD(2, 2), &wsaData);

    SOCKET serverSocket = socket(AF_INET, SOCK_STREAM, 0);
    sockaddr_in serverAddr;
    serverAddr.sin_family = AF_INET;
    serverAddr.sin_addr.s_addr = INADDR_ANY;
    serverAddr.sin_port = htons(5400);

    bind(serverSocket, (struct sockaddr*)&serverAddr, sizeof(serverAddr));
    listen(serverSocket, 5);

    while (true) {
        User* newUser = new User;
        newUser->clientSocket = accept(serverSocket, NULL, NULL);
        users.push_back(newUser);

        std::thread(HandleClient, newUser).detach();
    }

    closesocket(serverSocket);
    WSACleanup();

    return 0;
}
