namespace MinimalApi_Sample.Dtos
{
    public class CouponDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PercentDiscount { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
