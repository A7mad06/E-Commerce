namespace E_Commerce_Api.Products
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Product_Category { get; set; }
        public int Stoke { get; set; }
        public int Price { get; set; }
    }
}