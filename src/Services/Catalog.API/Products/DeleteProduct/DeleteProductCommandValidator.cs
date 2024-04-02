namespace Catalog.API.Products.DeleteProduct;

/// <summary>
///     Validator for the DeleteProductCommand.
/// </summary>
internal class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DeleteProductCommandValidator" /> class.
    /// </summary>
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("Id is required.");
    }
}