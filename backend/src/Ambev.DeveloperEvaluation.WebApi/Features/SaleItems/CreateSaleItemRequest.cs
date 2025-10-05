using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleItemRequest
    {
        [Required]
        [DefaultValue("d290f1ee-6c54-4b01-90e6-d701748f0851")]
        public Guid ProductId { get; set; } = Guid.Parse("d290f1ee-6c54-4b01-90e6-d701748f0851");

        [Required]
        [DefaultValue(2)]
        public int Quantity { get; set; } = 2;

        [Required]
        [DefaultValue(150.75)]
        public decimal UnitPrice { get; set; } = 150.75m;
    }

}
