namespace E_Commerce_Api.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime Order_Date { get; set; }
        public bool Is_Delivered { get; set; }
        public DateTime Deliver_Date { get; set; }
        public Guid Customer_Id { get; set; }
        public int Carrier_Id { get; set; }
        public int Phone {  get; set; }
        public string Address { get; set; }
        public int Total {  get; set; }
    }
}
