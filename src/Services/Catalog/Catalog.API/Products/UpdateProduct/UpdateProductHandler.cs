namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, string Description, List<string> Category, string ImageFile, decimal Price)
    : ICommand<UpdateProductResult>;
public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("{PropertyName} Is Required.");
        RuleFor(x => x.Name).NotEmpty().WithMessage("{PropertyName} Is Required.")
            .Length(2, 150).WithMessage("{PropertyName} Length Must Be Between 2 to 150 Character Long");
        RuleFor(x => x.Category).NotEmpty().WithMessage("{PropertyName} Is Required.");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("{PropertyName} Is Required.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("{PropertyName} Is Required.");
    }
}

internal class UpdateProductCommandHandler
    (
        IDocumentSession session,
        ILogger<UpdateProductCommandHandler> logger
    )
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException();
        }

        product.Name = command.Name;
        product.Description = command.Description;
        product.Category = command.Category;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;

        session.Update(product);
        await session.SaveChangesAsync();

        return new UpdateProductResult(true);
    }
}
