namespace WebApi.Configuration;

public class ApplicationConfiguration
{
    public DataAccessProvider Provider { get; set; } = DataAccessProvider.InMemory;

    public InMemoryConfiguration InMemory { get; } = new();
    
    public CassandraConfiguration Cassandra { get; } = new();
    
    public SqlServerConfiguration SqlServer { get; } = new();

    public MongoDbConfiguration Mongo { get; } = new();
    
    public MySqlConfiguration MySql { get; } = new();
}

public enum DataAccessProvider
{
    InMemory,
    
    SqlServer,
    
    Cassandra,
    
    MongoDb,
    
    MySql
}

public class InMemoryConfiguration
{
}

public class CassandraConfiguration
{
}

public class SqlServerConfiguration
{
}

public class MongoDbConfiguration
{
}

public class MySqlConfiguration
{
}
