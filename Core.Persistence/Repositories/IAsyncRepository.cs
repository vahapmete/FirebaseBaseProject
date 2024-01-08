using System.Linq.Expressions;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Google.Cloud.Firestore;

namespace Core.Persistence.Repositories;

public interface IAsyncRepository<TEntity>
    where TEntity : Entity
{
    //Task<TEntity?> GetAsync(
    //    Expression<Func<TEntity, bool>> predicate,
    //    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
    //    bool withDeleted = false,
    //    bool enableTracking = true,
    //    CancellationToken cancellationToken = default
    //);

    //Task<IPaginate<TEntity>> GetListAsync(
    //    int index = 0,
    //    int size = 10,
    //    bool withDeleted = false,
    //    bool enableTracking = true,
    //    CancellationToken cancellationToken = default
    //);

    //Task<IPaginate<TEntity>> GetListByDynamicAsync(
    //    DynamicQuery dynamic,
    //    int index = 0,
    //    int size = 10,
    //    CancellationToken cancellationToken = default
    //);

    //Task<bool> AnyAsync(
    //    Expression<Func<TEntity, bool>>? predicate = null,
    //    bool withDeleted = false,
    //    bool enableTracking = true,
    //    CancellationToken cancellationToken = default
    //);

    Task<TEntity> AddAsync(TEntity entity);
    Task<Paginate<TEntity>> GetListByDynamicAsync(DynamicQuery dynamic, string idOfLast, int index , int size);
    Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entity);
    Task<TEntity> DeleteAsync(TEntity entity, bool permanent = false);
    Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entity, bool permanent = false);
    Task<Paginate<TEntity>> GetList(Query? query, string idOfLast, int index, int size);
}
