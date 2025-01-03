﻿using MinimalApi_Sample.Models;

namespace MinimalApi_Sample.Data
{
    public static class CouponStore
    {
        public static List<Coupon> couponList = new List<Coupon>
        {
            new Coupon { Id = 1, Name = "10-Off", PercentDiscount = 10, IsActive = true },
            new Coupon { Id = 2, Name = "20-Off", PercentDiscount = 20, IsActive = false }
        };
    }
}
