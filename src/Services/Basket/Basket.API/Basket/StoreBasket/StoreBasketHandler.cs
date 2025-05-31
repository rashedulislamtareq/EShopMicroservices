namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);  

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("{PropertyName} Cannot be Null");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("{PropertyName} Is Required");
    }
}

internal class StoreBasketCommandHandler 
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart cart = command.Cart;

        //TODO: Store Basket To The Database Using Marten UPSERT
        //TODO: Update Redish Cache

        return new StoreBasketResult(cart.UserName);
    }
}
