using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Firebase;
using MediatR;

namespace Application.Features.Products.Queries.GetListByDynamic;

public class GetListByDynamicProductQuery : IRequest<GetListResponse<GetListByDynamicProductListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    public DynamicQuery DynamicQuery { get; set; }

    public class GetListProductByDynamicQueryHandler
        : IRequestHandler<GetListByDynamicProductQuery, GetListResponse<GetListByDynamicProductListItemDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetListProductByDynamicQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }


        public async Task<GetListResponse<GetListByDynamicProductListItemDto>> Handle(
            GetListByDynamicProductQuery request,
            CancellationToken cancellationToken
        )
        {
            Paginate<ProductFire> cars = await _productRepository.GetListByDynamicAsync(
                request.DynamicQuery,
                idOfLast: request.PageRequest.IdOfLast!,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize
            );
            var mappedCarListModel = _mapper.Map<GetListResponse<GetListByDynamicProductListItemDto>>(cars);
            return mappedCarListModel;
        }
    }
}
