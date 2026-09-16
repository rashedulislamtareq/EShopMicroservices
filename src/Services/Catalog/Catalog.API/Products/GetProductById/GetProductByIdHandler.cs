using Catalog.API.Exceptions;

namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid id) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(Product Product);

internal class GetProductByIdHandler(IDocumentSession sesson)

    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await sesson.LoadAsync<Product>(query.id, cancellationToken);

        if (product == null)
        {
            throw new ProductNotFoundException(query.id);
        }

        return new GetProductByIdResult(product);
    }
}
