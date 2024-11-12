namespace WebApi.Models;

public class SearchParams
{
    public int Page { get; set; }
    
    public int PageSize { get; set; }
    
    public string OrderBy { get; set; }
    
    public string Filter { get; set; }
}