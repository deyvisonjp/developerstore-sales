using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.SaleItems.CreateSaleItem
{
    /// <summary>
    /// Command for creating a new SaleItem
    /// </summary>
    public class CreateSaleItemCommand : IRequest<CreateSaleItemResult>
    {
        public Guid SaleId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
