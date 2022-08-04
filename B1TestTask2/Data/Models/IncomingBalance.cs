

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace B1TestTask2.Data.Models
{
    public class IncomingBalance
    {
        [Key]
        public string Id { get; set; }

        public decimal Active { get; set; }

        public decimal Passive { get; set; }

        [ForeignKey("BalanceAccount")]
        public string BalanceAccountId { get; set; }
        public virtual BalanceАccount BalanceAccount { get; set; }
    }
}
