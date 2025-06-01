namespace Discount.Grpc.Services;

public class DiscountService
    (
        DiscountContext dbContext,
        ILogger<DiscountService> logger
    )
    : DiscountProtoService.DiscountProtoServiceBase
{
    public async override Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext
            .Coupons
            .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

        if (coupon is null)
        {
            coupon = new Coupon() { ProductName = "No Discount", Amount = 0, Description = "No Discount" };
        }

        return coupon.Adapt<CouponModel>();
    }

    public async override Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Argument"));
        }

        dbContext.Coupons.Add(coupon);
        await dbContext.SaveChangesAsync();

        return coupon.Adapt<CouponModel>();
    }

    public async override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Argument"));
        }

        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync();

        return coupon.Adapt<CouponModel>();
    }

    public async override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext
            .Coupons
            .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

        if (coupon is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Product {request.ProductName} Not Found"));
        }

        dbContext.Remove(coupon);
        await dbContext.SaveChangesAsync();

        return new DeleteDiscountResponse() { Success = true };
    }
}
