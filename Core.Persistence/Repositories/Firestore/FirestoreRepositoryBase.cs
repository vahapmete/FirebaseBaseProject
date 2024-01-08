using System.Linq.Expressions;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using static Grpc.Core.Metadata;

namespace Core.Persistence.Repositories.Firestore;

public class FirestoreRepositoryBase<TEntity> : IAsyncRepository<TEntity>
    where TEntity : Entity
{
    private FirestoreProvider _firestoreProvider;

    public FirestoreRepositoryBase(FirestoreProvider firestoreProvider)
    {
        _firestoreProvider = firestoreProvider;
    }


    public Dictionary<string, object?> ToDictionary(TEntity entity)
    {
        Dictionary<string, object?> dict = new Dictionary<string, object?>();

        foreach (var prop in entity.GetType().GetProperties())
        {
            dict[prop.Name] = prop.GetValue(entity);
        }
        return dict;
    }
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        entity.Id = Guid.NewGuid().ToString();
        entity.CreatedDate = DateTime.UtcNow;
        await _firestoreProvider.Db().Collection(typeof(TEntity).Name.Replace("Fire","") + "s").Document(entity.Id).CreateAsync(entity);
        return entity;
    }

    public async Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities)
    {
        WriteBatch batch = _firestoreProvider.Db().StartBatch();
        foreach (var entity in entities)
        {
            entity.Id = Guid.NewGuid().ToString();
            entity.CreatedDate = DateTime.UtcNow;
            DocumentReference documentReference =
                _firestoreProvider.Db().Collection(typeof(TEntity).Name.Replace("Fire", "") + "s").Document(entity.Id);
            batch.Create(documentReference, entity);
        }
        await batch.CommitAsync();
        return entities;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        DocumentReference documentReference =
            _firestoreProvider.Db().Collection(typeof(TEntity).Name.Replace("Fire", "") + "s").Document(entity.Id);
        await documentReference.UpdateAsync(ToDictionary(entity));
        return entity;
    }

    public async Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities)
    {
        WriteBatch batch = _firestoreProvider.Db().StartBatch();
        foreach (var entity in entities)
        {
            DocumentReference documentReference =
                _firestoreProvider.Db().Collection(typeof(TEntity).Name.Replace("Fire", "") + "s").Document(entity.Id);
            batch.Update(documentReference, ToDictionary(entity));
        }
        await batch.CommitAsync();
        return entities;
    }

    public async Task<TEntity> DeleteAsync(TEntity entity, bool permanent = false)
    {
        DocumentReference documentReference =
            _firestoreProvider.Db().Collection(typeof(TEntity).Name.Replace("Fire", "") + "s").Document(entity.Id);
        await documentReference.DeleteAsync();
        return entity;
    }

    public async Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entities, bool permanent = false)
    {
        WriteBatch batch = _firestoreProvider.Db().StartBatch();
        foreach (var entity in entities)
        {
            DocumentReference documentReference =
                _firestoreProvider.Db().Collection(typeof(TEntity).Name.Replace("Fire", "") + "s").Document(entity.Id);
            batch.Delete(documentReference);
        }
        await batch.CommitAsync();
        return entities;
    }
    public async Task<TEntity?> GetByIdAsync(string id)
    {
        var document = _firestoreProvider.Db().Collection(typeof(TEntity).Name.Replace("Fire", "") + "s").Document(id);
        var snapshot = await document.GetSnapshotAsync();
        return snapshot.ConvertTo<TEntity>();
    }
    public async Task<Paginate<TEntity>> GetListByDynamicAsync(DynamicQuery dynamic, string idOfLast,int index = 0, int size = 10)
    {

        Query query = _firestoreProvider.Db().Collection(typeof(TEntity).Name.Replace("Fire", "") + "s").ToDynamic(dynamic);
        Paginate<TEntity> paginateModel =await query.ToPaginateAsync<TEntity>(index, size, idOfLast);
        return paginateModel;
    }
    public async Task<IList<TEntity>> GetAllAsync()
    {
        var collection = _firestoreProvider.Db().Collection(typeof(TEntity).Name.Replace("Fire", "") + "s");
        var snapshot = await collection.GetSnapshotAsync();
        return snapshot.Documents.Select(x => x.ConvertTo<TEntity>()).ToList();
    }
    public async Task<Paginate<TEntity>> GetList(Query? query, string idOfLast, int index = 0, int size = 10)
    {
        try
        {
            query ??= _firestoreProvider.Db().Collection(typeof(TEntity).Name.Replace("Fire", "") + "s");
            return await query.ToPaginateAsync<TEntity>(index, size, idOfLast);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<IList<T>> GetSelectList<T>(Query query)
    {
        try
        {
            var fiealds = typeof(T).GetProperties().Select(x => x.Name).ToArray();

            var snapshot = await query.Select(fiealds).GetSnapshotAsync();
            return snapshot.Documents.Select(x => x.ConvertTo<T>()).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

}
