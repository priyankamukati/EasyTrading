namespace TradingPlatform.Model
{
    public class UserOrderModel
    {
        public int id { get; set; }
        public int stock_id { get; set; }
        public int quantity { get; set; }
        public string order_nature { get; set; } = string.Empty;

        public string order_type { get; set; } = string.Empty;
        public double limit_price { get; set; }

        public string ticker { get; set; } = string.Empty;
        public string company_name { get; set; } = string.Empty;

        public string status { get; set; } = string.Empty;
        public string status_reason { get; set; } = string.Empty;
        public DateTime update_date { get; set; }
    }
}


