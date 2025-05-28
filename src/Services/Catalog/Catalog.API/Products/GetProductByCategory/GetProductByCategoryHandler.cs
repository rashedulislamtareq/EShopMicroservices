namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);

public class GetProductByCategoryQueryHandler
    (
        IDocumentSession session,
        ILogger<GetProductByCategoryQueryHandler> logger
    )
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>().Where(x => x.Category.Contains(query.Category)).ToListAsync();

        return new GetProductByCategoryResult(products);
    }
}
