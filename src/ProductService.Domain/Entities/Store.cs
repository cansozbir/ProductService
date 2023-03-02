using Domain.Entities.Common;

namespace ProductService.Domain.Entities;

public class Store : BaseEntity
{ 
    public string StoreName { get; set; }
}