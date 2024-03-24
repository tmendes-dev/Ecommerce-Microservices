﻿namespace Catalog.API.Products.GetProducts;

internal class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            GetProductsResult result = await sender.Send(new GetProductsQuery());
            GetProductsResponse response = result.Adapt<GetProductsResponse>();
            return Results.Ok(response);
        })
        .WithName("GetProducts")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("GetProducts")
        .WithDescription("GetProducts")
        .WithOpenApi();
    }
}