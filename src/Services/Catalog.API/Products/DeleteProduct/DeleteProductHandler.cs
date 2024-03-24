
namespace Catalog.API.Products.DeleteProduct;

/// <summary>
/// Handles the command to delete a product.
/// </summary>
internal sealed class DeleteProductHandler(IDocumentSession session, ILogger<DeleteProductHandler> logger) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    /// <summary>
    /// Handles the command to delete a product.
    /// </summary>
    /// <param name="command">The command containing the product identifier to be deleted.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteProductHandler.Handle called with {@Command}", JsonSerializer.Serialize(command));
        session.Delete<Product>(command.Id);
        await session.SaveChangesAsync(cancellationToken);
        return new() { Success = true };
    }
}
