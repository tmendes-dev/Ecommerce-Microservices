namespace Catalog.API.Products.CreateProduct;

/// <summary>
/// Handles the creation of a product.
/// </summary>
internal class CreateProductHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    /// <summary>
    /// Handles the creation of a product based on the provided command.
    /// </summary>
    /// <param name="request">The command containing product information.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of creating the product.</returns>
    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Create a new product based on the command
        Product product = new()
        {
            Category = request.Category,
            Description = request.Description,
            ImageFile = request.ImageFile,
            Name = request.Name,
            Price = request.Price
        };

        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        // Return the result containing the product's unique identifier
        return new CreateProductResult() { Id = product.Id };
    }
}