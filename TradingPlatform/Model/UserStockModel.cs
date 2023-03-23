namespace TradingPlatform.Model
{
    public class UserStockModel
    {

        public string ticker { get; set; } = string.Empty;
        public int quantity { get; set; }
        public string company_name { get; set; } = string.Empty;
        public int stock_id { get; set; }
        public double current_price { get; set; }
        public double current_amount { get; set; }
    }
}


