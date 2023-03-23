using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace TradingPlatform.Data
{
    [Table("user_stock")]
    public class user_stock
    {
        [Key, Required]
        public int id { get; set; }
        public string user_id { get; set; }
        public int stock_id { get; set; }
        public int quantity { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime update_date { get; set; }

    }
}


