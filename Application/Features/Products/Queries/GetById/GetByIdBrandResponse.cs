using Core.Application.Responses;

namespace Application.Features.Brands.Queries.GetById;

public class GetByIdBrandResponse : IResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
}
