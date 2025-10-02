using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Representa uma venda/transação por coleção de itens
    /// </summary>
    public class Sale : BaseEntity
    {
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Customer { get; set; } = string.Empty;
        public string Branch { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public SaleStatus Status { get; set; } = SaleStatus.Active;
        public List<SaleItem> Items { get; set; } = new();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Adiciona um item à venda com validações básicas de domínio.
        /// </summary>
        public void AddItem(SaleItem item)
        {
            if (item.Quantity > 20)
                throw new DomainValidationException("Quantidade excedida, 20 unidades por item!");

            Items.Add(item);
            RecalculateTotals();
        }

        /// <summary>
        /// Rwaliza o recalculo total da venda com os itens atuais.
        /// </summary>
        public void RecalculateTotals()
        {
            TotalAmount = Items.Sum(i => i.TotalItem);
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Cancelamento da venda.
        /// </summary>
        public void Cancel()
        {
            Status = SaleStatus.Cancelled;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}