using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using System.Linq;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a sale transaction composed of multiple sale items.
    /// </summary>
    public class Sale : BaseEntity
    {
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public Guid BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public SaleStatus Status { get; private set; } = SaleStatus.Active;
        public List<SaleItem> Items { get; private set; } = new();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        private Sale()
        {
            Items = new List<SaleItem>();
        }

        /// <summary>
        /// Calculates the total amount based on items and discounts.
        /// </summary>
        public void CalculateTotal()
        {
            TotalAmount = Items.Sum(i => i.TotalAmount);
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Cancels the sale.
        /// </summary>
        public void Cancel()
        {
            Status = SaleStatus.Cancelled;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Validates the Sale entity using the SaleValidator.
        /// </summary>
        public ValidationResultDetail Validate()
        {
            var validator = new SaleValidator();
            var result = validator.Validate(this);

            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
