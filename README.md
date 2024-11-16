# Data access drag racing
The application can run with different data storage providers.

## Local development
In this scenario, you are running the web api in an IDE, but you want the dependencies to be running. We leverage docker compose for this and have a separate file that can be used for development. This is a different docker compose file than is used for running the drag races, which would include the configured providers plus the web api in a single docker compose file.

### 1. Running the dependencies
To run the dependencies only so you can run the web application in an IDE, run:
```shell
docker-compose -f docker-compose-development.yaml up -d
```
To stop everything, run:
```shell
docker-compose -f docker-compose-development.yaml down
```

### 2. Running the application
Run the application in an IDE or using the `dotnet` CLI from the root.
```shell
dotnet run --project .\WebApi\WebApi.csproj
```
You can also run the application in Docker without any dependencies. In this situation, it will use the default in-memory store. Run these commands from the Docker folder.
```shell
docker build -t test/poc-data-access:TAG -f Dockerfile.WebApi .
docker run -p 5010:5010 test/poc-data-access:TAG
```


## Load testing
Build the project using docker compose v2:
```shell
docker compose -f compose-load-testing.yaml build
```
Run the containers:
```shell
docker compose -f compose-load-testing.yaml up -d
```
To stop everything, run:
```shell
docker compose -f compose-load-testing.yaml down
```

## Preparing target providers
### SQL Server
1. Install local tools from tool manifest file:
```shell
dotnet tool restore
```
2. Create SQL Server database
```shell
dotnet ef migrations add InitialCreate --project .\WebApi\WebApi.csproj
dotnet ef database update --connection "Server=YOUR_SQL_SERVER;Database=YOUR_DB_NAME;User Id=YOUR_USER;Password=YOUR_PASSWORD;"
```
### Cassandra
1. Create keyspace and table
```shell
CREATE KEYSPACE IF NOT EXISTS application_keyspace WITH replication = {'class': 'SimpleStrategy', 'replication_factor': 1};

USE application_keyspace;

CREATE TABLE IF NOT EXISTS Person (
    PersonId UUID PRIMARY KEY,
    CreatedOn timestamp,
    Name text,
    Birthday timestamp,
    Details text
);
```

