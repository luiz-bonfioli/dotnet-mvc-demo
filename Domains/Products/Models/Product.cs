namespace Demo.Domains.Products.Models;

public class Product
{
    public virtual int Id { get; set; }
    public virtual string Name { get; set; } = string.Empty;
    public virtual double Price { get; set; }
}
