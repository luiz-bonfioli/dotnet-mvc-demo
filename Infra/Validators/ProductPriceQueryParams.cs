using System.ComponentModel.DataAnnotations;

namespace Demo.Infra.Validators;

public class ProductPriceQueryParams
{

    [Range(0, double.MaxValue, ErrorMessage = "MinPrice must be >= 0.")]
    public double? MinPrice { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "MaxPrice must be >= 0.")]
    public double? MaxPrice { get; set; }

}
