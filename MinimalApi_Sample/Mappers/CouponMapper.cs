using MinimalApi_Sample.Dtos;
using MinimalApi_Sample.Models;

namespace MinimalApi_Sample.Mappers
{
    public static class CouponMapper
    {
        public static Coupon ToCouponFromUpdateDto(this UpdateCouponDto updateCouponDto)
        {
            return new Coupon()
            {
                Id = updateCouponDto.Id,
                Name = updateCouponDto.Name,
                PercentDiscount = updateCouponDto.PercentDiscount,
                IsActive = updateCouponDto.IsActive
            };
        }
        public static Coupon ToCouponFromCreateDto(this CreateCouponDto createCouponDto)
        {
            return new Coupon()
            {
                Name = createCouponDto.Name,
                PercentDiscount = createCouponDto.PercentDiscount,
                IsActive = createCouponDto.IsActive
            };
        }

        public static CouponDto ToCouponDtoFromCoupon(this Coupon coupon)
        {
            return new CouponDto()
            {
                Id = coupon.Id,
                Name = coupon.Name,
                PercentDiscount = coupon.PercentDiscount,
                IsActive = coupon.IsActive,
                CreatedOn = coupon.CreatedOn
            };
        }
    }
}
