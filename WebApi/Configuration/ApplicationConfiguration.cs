namespace WebApi.Configuration;

public class ApplicationConfiguration
{
    public DataAccessProvider Provider { get; set; } = DataAccessProvider.InMemory;

    public DebugConfiguration Debug { get; } = new();

    public InMemoryConfiguration InMemory { get; } = new();
    
    public CassandraConfiguration Cassandra { get; } = new();
    
    public SqlServerConfiguration SqlServer { get; } = new();

    public MongoDbConfiguration Mongo { get; } = new();
    
    public MySqlConfiguration MySql { get; } = new();
}

public class DebugConfiguration
{
    public bool DangerousLogConfiguration { get; init; }
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
    public string ContactPoint { get; init; }
}

public class SqlServerConfiguration
{
    public string ConnectionString { get; init; }
}

public class MongoDbConfiguration
{
}

public class MySqlConfiguration
{
}
