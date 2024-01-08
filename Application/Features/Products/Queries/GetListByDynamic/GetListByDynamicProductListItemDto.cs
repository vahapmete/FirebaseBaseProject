using Core.Application.Dtos;

namespace Application.Features.Products.Queries.GetListByDynamic;

public class GetListByDynamicProductListItemDto : IDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }
    public double Price { get; set; }
  
}

