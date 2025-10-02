using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleItemAddedEvent
    {
        public SaleItem SaleItem { get; }

        public SaleItemAddedEvent(SaleItem saleItem)
        {
            SaleItem = saleItem;
        }
    }
}
