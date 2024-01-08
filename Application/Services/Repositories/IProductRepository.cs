using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Core.Persistence.Repositories;
using Domain.Firebase;


namespace Application.Services.Repositories
{
    public interface IProductRepository: IAsyncRepository<ProductFire>
    {

    }
}
