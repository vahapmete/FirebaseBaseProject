namespace Core.Persistence.Dynamic;

public class Filter
{
    public string Field { get; set; }
    public string Operator { get; set; }
    public object Value { get; set; }
    public string? Logic { get; set; }
    public IEnumerable<Filter>? Filters { get; set; }

    public Filter()
    {
        
    }
    public Filter(object value)
    {
        Value = value;
        Field = string.Empty;
        Operator = string.Empty;
    }

    public Filter(string field, string @operator, object value)
    {
        Field = field;
        Operator = @operator;
        Value = value;
    }
}
