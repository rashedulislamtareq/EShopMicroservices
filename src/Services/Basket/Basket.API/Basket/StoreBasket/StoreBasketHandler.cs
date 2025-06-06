﻿using Discount.Grpc;

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
    (
        IBasketRepository repository,
        DiscountProtoService.DiscountProtoServiceClient discountProto
    )
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        await DeductDiscount(command.Cart, cancellationToken);

        //TODO: Store Basket To The Database Using Marten UPSERT
        //TODO: Update Redish Cache
        await repository.StoreBasket(command.Cart, cancellationToken);

        return new StoreBasketResult(command.Cart.UserName);
    }

    private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken) 
    {
        //TODO: Communicate With Discount Grpc & Calculate Latest Product Price
        foreach (var item in cart.Items)
        {
            var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest() { ProductName = item.ProductName });
            item.Price -= coupon.Amount;
        }
    }
}
