using Catalog.API.Products.GetProductByCategory;

namespace Catalog.API.Products.DeleteProduct;

/// <summary>
///     Represents an endpoint for deleting product by its unique identifier.
/// </summary>
internal sealed class DeleteProductEndpoint : ICarterModule
{
    /// <summary>
    ///     Adds routes for handling requests to delete product by id.
    /// </summary>
    /// <param name="app">The endpoint route builder.</param>
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
            {
                DeleteProductCommand command = new() { Id = id };
                var result = await sender.Send(command);
                var response = result.Adapt<DeleteProductResponse>();
                return Results.Ok(response);
            })
            .WithName("MapDelete")
            .WithSummary("Delete Product by Id")
            .WithDescription("Handles requests to delete product by id.")
            .Produces<GetProductsByCategoryResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithOpenApi();
    }
}