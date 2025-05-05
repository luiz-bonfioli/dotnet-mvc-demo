using FluentNHibernate.Mapping;

namespace Demo.Domains.Products.Models;

public class ProductMap : ClassMap<Product>
{
    public ProductMap()
    {
        Table("Products");
        Id(x => x.Id).GeneratedBy.Identity();
        Map(x => x.Name).Not.Nullable();
        Map(x => x.Price).Not.Nullable();
    }
}
