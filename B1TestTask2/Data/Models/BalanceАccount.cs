using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace B1TestTask2.Data.Models
{
    public class BalanceАccount
    {
        [Key]
        public string Id { get; set; }

        public string BalacnceAccountNumber { get; set; }

        [ForeignKey("Class")]
        public string ClassId { get; set; }

        public virtual Class Class { get; set; }


        [ForeignKey("FileXlsx")]
        public string FileId { get; set; }

        public virtual FileXlsx File { get; set; }

        public virtual IncomingBalance IncomingBalance { get; set; }

        public virtual Turnover Turnover { get; set; }
    }
}
