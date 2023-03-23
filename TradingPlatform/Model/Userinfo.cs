using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TradingPlatform.Data
{
    [Table("user_info")]
    public class user_info
    {
        [Key, Required]
        public string id { get; set; }
        public string full_name { get; set; } = string.Empty;
        public string username { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string type { get; set; } = string.Empty;
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime create_date { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime update_date { get; set; }
        public double cash_balance { get; set; }
    }
}
