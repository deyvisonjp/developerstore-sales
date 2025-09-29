namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public static class DiscountService
    {
        public static decimal GetDiscountForQuantity(int quantity)
        {
            if (quantity >= 10 && quantity <= 20) return 0.20m;
            if (quantity >= 4) return 0.10m;
            return 0.0m;
        }

        public static void ValidateQuantity(int quantity)
        {
            if (quantity > 20)
                throw new Ambev.DeveloperEvaluation.Domain.Validation.DomainValidationException(
                    "Quantidade excedida, 20 unidades por item.");
        }
    }
}