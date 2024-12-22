namespace MinimalApi_Sample.Dtos
{
    public class UpdateCouponDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PercentDiscount { get; set; }
        public bool IsActive { get; set; }
    }
}
