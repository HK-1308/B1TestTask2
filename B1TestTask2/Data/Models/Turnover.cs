using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace B1TestTask2.Data.Models
{
    public class Turnover
    {
        [Key]
        public string Id { get; set; }

        public decimal Debit { get; set; }

        public decimal Credit { get; set; }


        [ForeignKey("BalanceAccount")]
        public string BalanceAccountId { get; set; }
        public virtual BalanceАccount BalanceAccount { get; set; }
    }
}
