namespace MinimalApi_Sample.Models
{
    public class Coupon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PercentDiscount {  get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
