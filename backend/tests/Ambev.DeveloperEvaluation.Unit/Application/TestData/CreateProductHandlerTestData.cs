using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain
{
    /// <summary>
    /// Provides methods for generating test data for CreateProductCommand using Bogus.
    /// Ensures valid random product data for unit tests.
    /// </summary>
    public static class CreateProductHandlerTestData
    {
        private static readonly Faker<CreateProductCommand> createProductFaker = new Faker<CreateProductCommand>()
            .CustomInstantiator(f => new CreateProductCommand(
                Name: f.Commerce.ProductName(),
                Description: f.Commerce.ProductDescription(),
                Price: f.Random.Decimal(1, 1000),
                StockQuantity: f.Random.Int(0, 100)
            ));

        /// <summary>
        /// Generates a valid CreateProductCommand with randomized data.
        /// </summary>
        /// <returns>A valid CreateProductCommand instance.</returns>
        public static CreateProductCommand GenerateValidCommand()
        {
            return createProductFaker.Generate();
        }
    }
}
