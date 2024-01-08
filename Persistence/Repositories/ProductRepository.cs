using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Persistence.Repositories.Firestore;
using Domain.Firebase;

namespace Persistence.Repositories
{
    public class ProductRepository:FirestoreRepositoryBase<ProductFire>, IProductRepository
    {
        public ProductRepository(FirestoreProvider firestoreProvider) : base(firestoreProvider)
        {
        }
    }
}
