namespace E_Commerce_Api.Models
{
    public class Item
    {
        public Guid Id { get; set; }
        public Guid Cart_Id {  get; set; } 
        public Guid Product_Id { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string? Description { get; set; }
        public int subtotal { get; set; }
    }
}
