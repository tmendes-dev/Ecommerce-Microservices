namespace Catalog.API.Products.GetProductById;
/// <summary>
/// Validator for the GetProductByIdQuery.
/// </summary>
internal class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetProductByIdQueryValidator"/> class.
    /// </summary>
    public GetProductByIdQueryValidator() => RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("Id is required.");
}