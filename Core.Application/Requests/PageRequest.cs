namespace Core.Application.Requests;

public class PageRequest
{

    public int PageSize { get; set; }
    public int PageIndex { get; set; }
    public string? IdOfLast { get; set; }
}
