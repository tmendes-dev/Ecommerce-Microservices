using Catalog.API.Products.GetProductById;

namespace Catalog.API.Products.UpdateProduct;

/// <summary>
///     Represents an endpoint for updating a product.
/// </summary>
internal sealed class UpdateProductEndpoint : ICarterModule
{
    /// <summary>
    ///     Adds routes for handling requests to update a product.
    /// </summary>
    /// <param name="app">The endpoint route builder.</param>
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
            {
                GetProductByIdQuery query = new() { Id = request.Id };
                var product = await sender.Send(query);
                if (product.Product is null)
                    return Results.NotFound(request);

                UpdateProductCommand command = new() { Product = request.Adapt<Product>() };
                var result = await sender.Send(command);
                var response = result.Adapt<UpdateProductResponse>();
                return Results.Ok(response);
            })
            .WithName("UpdateProduct")
            .WithSummary("Update a product")
            .WithDescription("Handles requests to update a product details.")
            .Produces<UpdateProductResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithOpenApi();
    }
}