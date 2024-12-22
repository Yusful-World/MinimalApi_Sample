namespace MinimalApi_Sample.Dtos
{
    public class CreateCouponDto
    {
        public string Name { get; set; }
        public int PercentDiscount { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
