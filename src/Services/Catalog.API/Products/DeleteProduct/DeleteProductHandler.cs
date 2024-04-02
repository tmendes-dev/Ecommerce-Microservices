namespace Catalog.API.Products.DeleteProduct;

/// <summary>
///     Handles the command to delete a product.
/// </summary>
internal sealed class DeleteProductHandler(IDocumentSession session) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    /// <summary>
    ///     Handles the command to delete a product.
    /// </summary>
    /// <param name="command">The command containing the product identifier to be deleted.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        session.Delete<Product>(command.Id);
        await session.SaveChangesAsync(cancellationToken);
        return new DeleteProductResult { Success = true };
    }
}