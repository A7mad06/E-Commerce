
namespace E_Commerce_Api.Models
{
    public class Cart
    {
        public Guid Product_id { get; set; }
        public int Price { get; set; }
        public string Img1 { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Subtotal { get; set; }
    }
}
