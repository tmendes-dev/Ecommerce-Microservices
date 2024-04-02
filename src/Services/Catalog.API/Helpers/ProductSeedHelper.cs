namespace Catalog.API.Helpers;

/// <summary>
///     Helper class to seed the database with sample products.
/// </summary>
public static class ProductSeedHelper
{
    /// <summary>
    ///     Seeds the database with sample products if no products exist.
    /// </summary>
    /// <param name="session">The Marten document session.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task Seed(IDocumentSession session)
    {
        var existingProduct = await session.Query<Product>().FirstOrDefaultAsync();
        if (existingProduct is not null)
            return;

        List<Product> products = new List<Product>();
        Random randomGenerator = new();
        const int numberOfProducts = 500;

        for (var i = 0; i < numberOfProducts; i++)
        {
            var product = CreateProduct(i, randomGenerator);
            products.Add(product);
        }

        products.ForEach(p => session.Store(p));
        await session.SaveChangesAsync();
        Console.WriteLine("Database seeded with sample products.");
    }

    private static Product CreateProduct(int productIndex, Random randomGenerator) =>
        new()
        {
            Id = Guid.NewGuid(),
            Name = $"Product {productIndex + 1}",
            Category = GenerateRandomCategories(randomGenerator),
            Description = $"Description for Product {productIndex + 1}",
            ImageFile = $"/path/to/image{productIndex + 1}.jpg",
            Price = (decimal)randomGenerator.NextDouble() * 100
        };

    private static List<string> GenerateRandomCategories(Random randomGenerator)
    {
        List<string> categories =  [ "Category A", "Category B", "Category C", "Category D", "Category E" ];
        List<string> randomCategories = new ();
        var numCategories = randomGenerator.Next(1, 4);

        for (var i = 0; i < numCategories; i++)
        {
            var index = randomGenerator.Next(categories.Count);
            randomCategories.Add(categories[index]);
            categories.RemoveAt(index);
        }

        return randomCategories;
    }
}