# Data Access App

## Run Cassandra
```shell
docker network create cassandra

docker run --rm -d --name cassandra --hostname cassandra --network cassandra cassandra
```
Run DataSTAX Studio
```shell
docker run --name datastax-studio -d -p 9091:9091 -e DS_LICENSE=accept --network cassandra datastax/dse-studio
```

Run CQL Shell
```shell
docker run --rm -it --network cassandra nuvo/docker-cqlsh cqlsh cassandra 9042 --cqlversion='3.4.5'
```
