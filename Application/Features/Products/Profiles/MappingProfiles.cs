using Application.Features.Products.Commands.Create;
using Application.Features.Products.Queries.GetList;
using Application.Features.Products.Queries.GetListByDynamic;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Firebase;

namespace Application.Features.Products.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ProductFire, CreateProductCommand>().ReverseMap();
        CreateMap<ProductFire, CreateProductResponse>().ReverseMap();
        CreateMap<ProductFire, GetListProductListItemDto>().ReverseMap();
        CreateMap<ProductFire, GetListByDynamicProductListItemDto>().ReverseMap();
        CreateMap<Paginate<ProductFire>, GetListResponse<GetListProductListItemDto>>().ReverseMap();
        CreateMap<Paginate<ProductFire>, GetListResponse< GetListByDynamicProductListItemDto >>().ReverseMap();
    }
}
