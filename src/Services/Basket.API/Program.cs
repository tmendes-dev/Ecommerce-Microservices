using BuildingBlocks.Behaviours;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

Assembly programAsm = typeof(Program).Assembly;
string dbConnectionString = builder.Configuration.GetConnectionString("Database")!;

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

});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.SwaggerDoc("v1", info: new()
{
    Version = "v1",
    Title = "Basket API",
    Description = "An ASP.NET Core Web API for managing basket"
}));

WebApplication app = builder.Build();
app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        Exception? exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        if (exception is null)
            return;
        ProblemDetails problemDetails = new()
        {
            Title = exception.Message,
            Status = StatusCodes.Status500InternalServerError,
            Detail = exception.StackTrace
        };
        ILogger<Program> logger = context.RequestServices.GetService<ILogger<Program>>()!;
        logger.LogError(exception, exception.Message);
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/problem+json";
        await context.Response.WriteAsJsonAsync(problemDetails);
    });
});
app.MapCarter();
app.UseHealthChecks("/health", options: new() { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.Run();