namespace Catalog.API.Helpers;

/// <summary>
/// Helper class to seed the database with sample products.
/// </summary>
public static class ProductSeedHelper
{
    /// <summary>
    /// Seeds the database with sample products if no products exist.
    /// </summary>
    /// <param name="session">The Marten document session.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task Seed(IDocumentSession session)
    {
        // Check if products exist in the database
        Product? existingProduct = await session.Query<Product>().FirstOrDefaultAsync();

        if (existingProduct is not null)
            return;

        // Add sample products
        List<Product> products = [];
        Random random = new();
        const int NUMBER_OF_PRODUCTS = 500;

        for (int i = 0; i < NUMBER_OF_PRODUCTS; i++)
            products.Add(new()
            {
                Id = Guid.NewGuid(),
                Name = $"Product {i + 1}",
                Category = GenerateRandomCategories(random),
                Description = $"Description for Product {i + 1}",
                ImageFile = $"/path/to/image{i + 1}.jpg",
                Price = (decimal)random.NextDouble() * 100 // Random price between 0 and 100
            });

        products.ForEach(p => session.Store(p));
        await session.SaveChangesAsync();
        Console.WriteLine("Database seeded with sample products.");
    }

    /// <summary>
    /// Generates a list of random categories.
    /// </summary>
    /// <param name="random">The random number generator.</param>
    /// <returns>A list of randomly generated categories.</returns>
    private static List<string> GenerateRandomCategories(Random random)
    {
        var categories = new List<string> { "Category A", "Category B", "Category C", "Category D", "Category E" };
        var randomCategories = new List<string>();

        // Generate random number of categories (up to 3)
        int numCategories = random.Next(1, 4);
        for (int i = 0; i < numCategories; i++)
        {
            // Randomly select categories from the list
            int index = random.Next(categories.Count);
            randomCategories.Add(categories[index]);
            categories.RemoveAt(index);
        }
        return randomCategories;
    }
}
