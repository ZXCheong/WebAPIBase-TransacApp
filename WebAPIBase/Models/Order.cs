namespace WebAPIBase.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Order_date { get; set; }
        public string Order_status { get; set; }
        public string Billing_address { get; set;}
        public string Shipping_address { get; set; }
        public int Customer_id { get; set; }
    }
}
