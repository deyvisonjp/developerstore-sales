using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequest
    {
        [Required]
        [DefaultValue("V20251005-01")]
        public string SaleNumber { get; set; } = "V20251005-01";

        [Required]
        [DefaultValue("3fa85f64-5717-4562-b3fc-2c963f66afa6")]
        public Guid CustomerId { get; set; } = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");

        [Required]
        [DefaultValue("John Doe")]
        public string CustomerName { get; set; } = "John Doe";

        [Required]
        [DefaultValue("6f9619ff-8b86-d011-b42d-00cf4fc964ff")]
        public Guid BranchId { get; set; } = Guid.Parse("6f9619ff-8b86-d011-b42d-00cf4fc964ff");

        [Required]
        [DefaultValue("Main Branch")]
        public string BranchName { get; set; } = "Main Branch";

        public List<CreateSaleItemRequest> Items { get; set; } = new List<CreateSaleItemRequest>
        {
            new CreateSaleItemRequest()
        };
    }
}