namespace E_Commerce_Api.Products
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Product_category { get; set; }
        public int? Stoke { get; set; }
        public int Price { get; set; }
        public string? Img1 { get; set; }
        public string? Img2 { get; set; }
        public string? Img3 { get; set; }

    }
}