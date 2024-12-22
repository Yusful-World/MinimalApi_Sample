namespace MinimalApi_Sample.Dtos
{
    public class CreateCouponDto
    {
        public string Name { get; set; }
        public int PercentDiscount { get; set; }
        public bool IsActive { get; set; }
    }
}
