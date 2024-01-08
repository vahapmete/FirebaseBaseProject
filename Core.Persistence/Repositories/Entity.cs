using System.ComponentModel.DataAnnotations.Schema;
using Google.Cloud.Firestore;

namespace Core.Persistence.Repositories;

[FirestoreData]
public class Entity : IEntityTimestamps
{
    [FirestoreProperty]
    public string Id { get; set; }
    [FirestoreProperty]
    public DateTime CreatedDate { get; set; }
    [FirestoreProperty]
    public DateTime? UpdatedDate { get; set; }
    [FirestoreProperty]
    public DateTime? DeletedDate { get; set; }

    public Entity()
    {
        Id = default!;
    }

    
}
