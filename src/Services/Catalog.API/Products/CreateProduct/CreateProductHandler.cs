namespace Catalog.API.Products.CreateProduct;

/// <summary>
/// Handles the creation of a product.
/// </summary>
internal sealed class CreateProductHandler(IDocumentSession session, ILogger<CreateProductHandler> logger) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    /// <summary>
    /// Handles the creation of a product based on the provided command.
    /// </summary>
    /// <param name="command">The command containing product information.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of creating the product.</returns>
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateProductHandler.Handle called with {@Command}", JsonSerializer.Serialize(command));

        // Create a new product based on the command
        Product product = new()
        {
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Name = command.Name,
            Price = command.Price
        };

        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        // Return the result containing the product's unique identifier
        return new() { Id = product.Id };
    }
}