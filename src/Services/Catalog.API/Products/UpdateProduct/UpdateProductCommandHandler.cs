
namespace Catalog.API.Products.UpdateProduct;
/// <summary>
/// Handles the command to update a product.
/// </summary>
internal sealed class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductCommandResult>
{
    /// <summary>
    /// Handles the command to update a product.
    /// </summary>
    /// <param name="command">The command containing the updated product information.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the update operation.</returns>
    public async Task<UpdateProductCommandResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        session.Update(command.Product);
        await session.SaveChangesAsync(cancellationToken);
        return new() { Product = command.Product };
    }
}
