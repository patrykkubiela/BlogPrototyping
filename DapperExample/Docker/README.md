# Docker-Compose for [programista-doswiadczony] (https://programista-doswiadczony.pl/) website post about [Dapper lib] (https://github.com/StackExchange/Dapper)

Next to this readme file you see docker-compose.yml file.
It's placed here for easy build and run two docker containers. One for postgresql server and second one for pgadmin container.
All of this is needed to quick run DapperExample project which is made for my website post as an example.

Below are the only command that you will need to use mentioned docker-compose.yml file.
OFC you will need it if you are docker newbie ;-)

## 1. Prerequisites
- Install [Docker] (https://www.docker.com/)
- Install [Docker-compose] (https://docs.docker.com/compose/install/)

## 2. Build and Run containers

To build images and run containers just run this command under your shell:

```
docker-compose up
```

This command will download base images, and build images on your own docker, also it will run containers. Except that it will create volumes and networks for those containes:

Volumes
- **docker_pgadmin**
- **docker_postgres**

Network
- **docker_postgres**


If you want to stop those containers you have to use command:

```
docker-compose stop
```

If you want to remove those containers, images, volumes and networks you have to use command:

```
docker-compose down
```

## 3. For the rest of docker informations I refer to the [docker-compose documentation] (https://docs.docker.com/compose/)