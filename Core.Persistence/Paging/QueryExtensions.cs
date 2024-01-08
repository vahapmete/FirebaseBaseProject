
using Core.Persistence.Repositories;
using Google.Cloud.Firestore;

namespace Core.Persistence.Paging;

public static class QueryExtensions
{
    public static async Task<Paginate<T>> ToPaginateAsync<T>(this Query query, int index, int size,string idOfLast) where T : Entity
    {
        var querySnapshot = await query.GetSnapshotAsync();
        int count = querySnapshot.Count;
        if (!string.IsNullOrEmpty(idOfLast) && idOfLast !="0")
        {
            var snapshot = query.Database.Collection(typeof(T).Name.Replace("Fire","") + "s").Document(idOfLast).GetSnapshotAsync().Result;
            query = query.StartAfter(snapshot);
        }
        query =query.Limit(size);
        var items = query.GetSnapshotAsync().Result.Documents.Select(x => x.ConvertTo<T>()).ToList();

        Paginate<T> list =
            new()
            {
                IdOfLast = items.LastOrDefault()!.Id,
                Index = index,
                Size = size,
                Count = count,
                Items = items,
                Pages = (int)Math.Ceiling(count / (double)size)
            };
        return list;
    }
}
