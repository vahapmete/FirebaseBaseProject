namespace Core.Persistence.Dynamic;

public class DynamicQuery
{
    public Sort? Sort { get; set; }
    public Filter? Filter { get; set; }

    public DynamicQuery() { }

    public DynamicQuery(Sort? sort, Filter? filter)
    {
        Sort = sort;
        Filter = filter;
    }
}
