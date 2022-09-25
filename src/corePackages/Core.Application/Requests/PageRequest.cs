namespace Core.Application.Requests;


//page requestin sorgu sırasında kullanılan yapısıdır.

public class PageRequest
{
    public int Page { get; set; } 
    public int PageSize { get; set; }
}