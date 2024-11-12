namespace WebApi.Models;

using System.ComponentModel.DataAnnotations;

public abstract class PersonBase
{
    [Required]
    [MaxLength(50)]
    public string Name { get; init; }
    
    [Required]
    public DateTime Birthday { get; init; }
    
    [Required]
    [MaxLength(100)]
    public string Details { get; init; }
}

public class CreatePerson : PersonBase
{
}

public class Person : PersonBase
{
    [Required]
    public Guid PersonId { get; init; }
    
    [Required]
    public DateTime CreatedOn { get; init; }
}