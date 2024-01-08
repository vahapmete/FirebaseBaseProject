using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Application.Responses;

namespace Application.Features.Products.Commands.Create
{
    public class CreateProductResponse : IResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
