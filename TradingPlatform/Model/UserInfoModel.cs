namespace TradingPlatform.Model
{
    public class UserInfoModel
    {
        public string id { get; set; } = string.Empty;
        public string full_name { get; set; } = string.Empty;
        public string username { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string type { get; set; } = string.Empty;
        public DateTime create_date { get; set; }
        public DateTime update_date { get; set; }
        public double cash_balance { get; set; }
        public string passcode { get; set; } = string.Empty;

    }
}
