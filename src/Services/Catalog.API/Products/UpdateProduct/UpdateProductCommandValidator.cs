namespace Catalog.API.Products.UpdateProduct;

/// <summary>
///     Validator for the UpdateProductCommand.
/// </summary>
internal class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UpdateProductCommandValidator" /> class.
    /// </summary>
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Product.Id).NotNull().NotEmpty().WithMessage("Id is required.");
        RuleFor(x => x.Product.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Product.Category).NotEmpty().WithMessage("Category is required.");
        RuleFor(x => x.Product.ImageFile).NotEmpty().WithMessage("ImageFile is required.");
        RuleFor(x => x.Product.Price).GreaterThan(0).WithMessage("Price is required.");
    }
}