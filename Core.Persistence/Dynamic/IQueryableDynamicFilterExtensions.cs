using System.Linq;
using System.Text;
using Google.Cloud.Firestore;

namespace Core.Persistence.Dynamic;

public static class IQueryableDynamicFilterExtensions
{
    private static readonly string[] _orders = { "asc", "desc" };

    private static readonly IDictionary<string, string> _operators = new Dictionary<string, string>
    {
        { "eq", "=" },
        { "neq", "!=" },
        { "lt", "<" },
        { "lte", "<=" },
        { "gt", ">" },
        { "gte", ">=" },
        { "isnull", "== null" },
        { "isnotnull", "!= null" },
        { "wherein", "WhereIn" },
        { "endswith", "EndsWith" },
        { "contains", "Contains" },
        { "doesnotcontain", "Contains" }
    };

    public static Query ToDynamic(this Query query, DynamicQuery dynamicQuery)
    {
        if (dynamicQuery.Filter is not null)
            query = Filter(query, dynamicQuery.Filter);
        if (dynamicQuery.Sort is not null)
            query = Sort(query, dynamicQuery.Sort);
        return query;
    }

    private static Query Filter(Query query, Filter filter)
    {
        IList<Filter> filters = GetAllFilters(filter);
        
        foreach (var currentFilter in filters)
        {
            if (!_operators.ContainsKey(currentFilter.Operator))
            {
                throw new ArgumentException("Invalid Operator Type");
            }
            switch (currentFilter.Operator)
            {
                case "eq":
                    query = query.WhereEqualTo(currentFilter.Field, currentFilter.Value);
                    break;
                case "neq":
                    query = query.WhereNotEqualTo(currentFilter.Field, currentFilter.Value);
                    break;
                case "lt":
                    query = query.WhereLessThan(currentFilter.Field, currentFilter.Value);
                    break;
                case "lte":
                    query = query.WhereLessThanOrEqualTo(currentFilter.Field, currentFilter.Value);
                    break;
                case "gt":
                    query = query.WhereGreaterThan(currentFilter.Field, currentFilter.Value);
                    break;
                case "gte":
                    query = query.WhereGreaterThanOrEqualTo(currentFilter.Field, currentFilter.Value);
                    break;
                case "contain":
                    query = query.WhereArrayContains(currentFilter.Field, currentFilter.Value);
                    break;
                case "containsany":
                    query = query.WhereArrayContainsAny(currentFilter.Field, (System.Collections.IEnumerable)currentFilter.Value);
                    break;
                case "wherein":
                    query = query.WhereIn(currentFilter.Field, (System.Collections.IEnumerable)currentFilter.Value);
                    break;
                case "wherenotin":
                    query = query.WhereNotIn(currentFilter.Field, (System.Collections.IEnumerable)currentFilter.Value);
                    break;
            }
        }
        return query;
    }

    private static Query Sort(Query query, Sort sort)
    {

        if (string.IsNullOrEmpty(sort.Field))
            throw new ArgumentException("Invalid Field");
        if (string.IsNullOrEmpty(sort.Dir) || !_orders.Contains(sort.Dir))
            throw new ArgumentException("Invalid Order Type");

        switch (sort.Dir)
        {
            case "asc":
                query = query.OrderBy(sort.Field);
                break;
            case "desc":
                query = query.OrderByDescending(sort.Field); 
                break;
        }
        return query;
    }

    public static IList<Filter> GetAllFilters(Filter filter)
    {
        List<Filter> filters = new();
        GetFilters(filter, filters);
        return filters;
    }

    private static void GetFilters(Filter filter, IList<Filter> filters)
    {
        filters.Add(filter);
        if (filter.Filters is not null && filter.Filters.Any())
            foreach (Filter item in filter.Filters)
                GetFilters(item, filters);
    }

}
