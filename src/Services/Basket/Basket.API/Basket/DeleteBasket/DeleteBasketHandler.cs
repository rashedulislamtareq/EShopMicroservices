namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketCommand(string UserName):ICommand<DeleteBasketResult>;
public record DeleteBasketResult(bool IsSuccess);

internal class DeleteBasketCommandHandler
    : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        //TODO: DeleteBasket From Database And Cache
        //session.Delete(command.Id);

        return new DeleteBasketResult(true);
    }
}
