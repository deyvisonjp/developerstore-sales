using Ambev.DeveloperEvaluation.Application.Products.Handlers.CreateProduct;
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
                Id: Guid.NewGuid(),
                Name: f.Commerce.ProductName(),
                Description: f.Commerce.ProductDescription(),
                Category: f.Commerce.Categories(1)[0],
                Price: f.Random.Decimal(1, 1000),
                RatingAverage: Math.Round(f.Random.Double(0, 5), 2),
                RatingReviews: f.Random.Decimal(0, 500)
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
