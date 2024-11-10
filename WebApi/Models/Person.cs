namespace WebApi.Models;

public abstract class PersonBase
{
    public string Name { get; init; }
    
    public DateTime Birthday { get; init; }
    
    public string Details { get; init; }
}

public class CreatePerson : PersonBase
{
}

public class Person : PersonBase
{
    public Guid PersonId { get; init; }
    
    public DateTime CreatedOn { get; init; }
}