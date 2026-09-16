namespace Catalog.API.Products.GetProducts;


public record GetProductsQuery(int? page = 1, int? size = 10) : IQuery<GetProductsResult>;
public record GetProductsResult(IEnumerable<Product> Products);


internal class GetProductsHandler(IDocumentSession session)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await session
            .Query<Product>()
            .ToPagedListAsync(query.page ?? 1, query.size ?? 10, cancellationToken);

        return new GetProductsResult(products);
    }
}
