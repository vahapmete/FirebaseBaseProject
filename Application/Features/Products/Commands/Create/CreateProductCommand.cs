using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Firebase;
using MediatR;

namespace Application.Features.Products.Commands.Create
{
    public class CreateProductCommand : IRequest<CreateProductResponse>
    {
        public string Name { get; set; }
        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResponse>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMapper _mapper;
            public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }

            public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                ProductFire mappedBrand = _mapper.Map<ProductFire>(request);
                ProductFire createdBrand = await _productRepository.AddAsync(mappedBrand);
                CreateProductResponse createdBrandResponse = _mapper.Map<CreateProductResponse>(createdBrand);
                return createdBrandResponse;
            }
        }
    }
}
