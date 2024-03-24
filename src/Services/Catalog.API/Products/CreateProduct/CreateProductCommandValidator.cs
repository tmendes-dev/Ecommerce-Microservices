
namespace Catalog.API.Products.CreateProduct;

/// <summary>
/// Validator for the CreateProductCommand.
/// </summary>
internal class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProductCommandValidator"/> class.
    /// </summary>
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required.");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price is required.");
    }
}
