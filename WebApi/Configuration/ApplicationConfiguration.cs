namespace WebApi.Configuration;

public class ApplicationConfiguration
{
    public DataAccessProvider Provider { get; set; } = DataAccessProvider.InMemory;

    public CassandraConfiguration Cassandra { get; } = new();
    
    public SqlServerConfiguration SqlServer { get; } = new();
    
    public MongoDbConfiguration Mongo { get; } = new();
}

public enum DataAccessProvider
{
    InMemory,
    
    SqlServer,
    
    Cassandra,
    
    MongoDb
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