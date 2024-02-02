namespace E_Commerce_Api.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public int total { get; set; }
        public string Customer_Email { get; set; }
    }
}
