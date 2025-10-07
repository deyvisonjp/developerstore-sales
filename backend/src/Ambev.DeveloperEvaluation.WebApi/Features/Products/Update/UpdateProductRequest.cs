using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.Update
{
    /// <summary>
    /// Request para atualização de um produto via API.
    /// </summary>
    public class UpdateProductRequest
    {
        [DefaultValue("Notebook")]
        public string Title { get; set; } = "Notebook";

        [DefaultValue("Notebook Dell")]
        public string? Description { get; set; } = "Notebook Dell";

        [DefaultValue("Eletrônicos")]
        public string? Category { get; set; } = "Eletrônicos";

        [DefaultValue(1800)]
        public decimal Price { get; set; } = 1800;

        [DefaultValue("https://via.placeholder.com/150")]
        public string? Image { get; set; } = "https://via.placeholder.com/150";

        [DefaultValue(0)]
        public double RatingAverage { get; set; } = 0;

        [DefaultValue(0)]
        public int RatingReviews { get; set; } = 0;
    }
}
