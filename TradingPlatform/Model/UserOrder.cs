using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace TradingPlatform.Data
{
    [Table("user_order")]
    public class user_order
    {
        [Key, Required]
        public int id { get; set; }
        public string user_id { get; set; }
        public int stock_id { get; set; }
        public string order_nature { get; set; } = string.Empty;
        public int quantity { get; set; }
        public string status { get; set; } = string.Empty;
        public string status_reason { get; set; } = string.Empty;
        public string order_type { get; set; } = string.Empty;
        public double limit_price { get; set; }
        public DateTime limit_expiration { get; set; }
        public double transaction_execution_price { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime create_date { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime update_date { get; set; }


    }
}



