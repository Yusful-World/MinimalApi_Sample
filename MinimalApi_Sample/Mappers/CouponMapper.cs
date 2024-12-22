using MinimalApi_Sample.Dtos;
using MinimalApi_Sample.Models;

namespace MinimalApi_Sample.Mappers
{
    public static class CouponMapper
    {
        public static Coupon ToCouponFromCreate(this CreateCouponDto createCouponDto)
        {
            return new Coupon()
            {
                Name = createCouponDto.Name,
                PercentDiscount = createCouponDto.PercentDiscount,
                CreatedOn = createCouponDto.CreatedOn,
                LastUpdated = createCouponDto.LastUpdated
            };
        }
    }
}
