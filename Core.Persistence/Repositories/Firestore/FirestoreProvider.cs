using Google.Cloud.Firestore;

namespace Core.Persistence.Repositories.Firestore;

public class FirestoreProvider 
{

    private readonly FirestoreDb _fireStoreDb = null!;

    public FirestoreProvider(FirestoreDb fireStoreDb)
    {
        _fireStoreDb = fireStoreDb;
    }

   
    public FirestoreDb Db()
    {
        return _fireStoreDb;
    }


}