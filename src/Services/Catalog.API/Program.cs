using BuildingBlocks.Behaviours;
using Catalog.API.Helpers;
using Catalog.API.Products.CreateProduct;
using Catalog.API.Products.DeleteProduct;
using Catalog.API.Products.GetProductByCategory;
using Catalog.API.Products.GetProductById;
using Catalog.API.Products.GetProducts;
using Catalog.API.Products.UpdateProduct;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var programAsm = typeof(Program).Assembly;
var dbConnectionString = builder.Configuration.GetConnectionString("Database")!;

builder.Services.AddHealthChecks().AddNpgSql(dbConnectionString);
builder.Services.AddMarten(options => options.Connection(dbConnectionString)).UseLightweightSessions();
builder.Services.AddValidatorsFromAssembly(programAsm);
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(programAsm);
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});
builder.Services.AddCarter(configurator: c =>
{
    c.WithModule<CreateProductEndpoint>();
    c.WithModule<GetProductsEndpoint>();
    c.WithModule<GetProductByIdEndpoint>();
    c.WithModule<GetProductsByCategoryEndpoint>();
    c.WithModule<UpdateProductEndpoint>();
    c.WithModule<DeleteProductEndpoint>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.SwaggerDoc("v1", new OpenApiInfo
{
    Version = "v1",
    Title = "Catalog API",
    Description = "An ASP.NET Core Web API for managing products items"
}));

var app = builder.Build();
app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        if (exception is null)
            return;
        ProblemDetails problemDetails = new()
        {
            Title = exception.Message,
            Status = StatusCodes.Status500InternalServerError,
            Detail = exception.StackTrace
        };
        var logger = context.RequestServices.GetService<ILogger<Program>>()!;
        logger.LogError(exception, exception.Message);
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/problem+json";
        await context.Response.WriteAsJsonAsync(problemDetails);
    });
});
app.MapCarter();
app.UseHealthChecks("/health", options: new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    var appLifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();
    appLifetime.ApplicationStarted.Register(async () => await SeedProducts(app));
}

app.Run();


static async Task SeedProducts(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var session = scope.ServiceProvider.GetRequiredService<IDocumentSession>();
    await ProductSeedHelper.Seed(session);
}