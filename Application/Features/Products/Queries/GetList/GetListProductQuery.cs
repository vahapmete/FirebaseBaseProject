using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Firebase;
using MediatR;

namespace Application.Features.Products.Queries.GetList;

public class GetListProductQuery : IRequest<GetListResponse<GetListProductListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    //public bool BypassCache { get; }
    //public string CacheKey => $"GetListBrands({PageRequest.PageIndex},{PageRequest.PageSize})";
    //public string CacheGroupKey => "GetBrands";
    //public TimeSpan? SlidingExpiration { get; }

    public class GetListProductQueryHandler : IRequestHandler<GetListProductQuery, GetListResponse<GetListProductListItemDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetListProductQueryHandler(IProductRepository brandRepository, IMapper mapper)
        {
            _productRepository = brandRepository;
            _mapper = mapper;
        }


        public async Task<GetListResponse<GetListProductListItemDto>> Handle(GetListProductQuery request, CancellationToken cancellationToken)
        {
            Paginate<ProductFire> products = await _productRepository.GetList(null,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                idOfLast:request.PageRequest.IdOfLast
            );
            var mappedProductListModel = _mapper.Map<GetListResponse<GetListProductListItemDto>>(products);
            return mappedProductListModel;
        }
    }
}
