using System.ComponentModel.DataAnnotations;

namespace B1TestTask2.Data.Models
{
    public class FileXlsx
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public List<BalanceАccount> BalanceАccounts { get; set; } = new List<BalanceАccount>();

    }
}
