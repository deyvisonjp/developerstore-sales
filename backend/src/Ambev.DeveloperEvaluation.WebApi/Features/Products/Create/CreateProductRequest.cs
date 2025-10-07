using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.Create
{
    /// <summary>
    /// Request para criação de um produto via API.
    /// </summary>
    public class CreateProductRequest
    {
        [DefaultValue("Notebook")]
        public string Title { get; set; } = "Notebook";

        [DefaultValue("Notebook Dell")]
        public string? Description { get; set; } = "Notebook Dell";

        [DefaultValue("Eletrônicos")]
        public string? Category { get; set; } = "Eletrônicos";

        [DefaultValue(1800)]
        public decimal Price { get; set; } = 1800;

        [DefaultValue(null)]
        public string? Image { get; set; }

        [DefaultValue(0)]
        public double RatingAverage { get; set; } = 0;

        [DefaultValue(0)]
        public decimal RatingReviews { get; set; } = 0;
    }
}
