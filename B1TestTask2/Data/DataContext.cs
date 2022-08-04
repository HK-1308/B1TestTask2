using B1TestTask2.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace B1TestTask2.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Class> Classes { get; set; }
        public DbSet<FileXlsx> Files { get; set; }

        public DbSet<BalanceАccount> BalanceАccounts { get; set; }

        public DbSet<IncomingBalance> IncomingBalances { get; set; }
        public DbSet<Turnover> Turnovers { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Class>().HasData(new Class[] { new Class { Id = "1", Name = "Денежные средства, драгоценные металлы и межбанковские операции" }, new Class { Id = "2", Name = "Кредитные и иные активные операции с клиентами" }
                                                             , new Class { Id = "3", Name = " Счета по операциям клиентов" }, new Class { Id = "4", Name = " Ценные бумаги" }
                                                             , new Class { Id = "5", Name = "Долгосрочные финансовые вложения в уставные фонды юридических лиц, основные средства и прочее имущество" }, new Class { Id = "6", Name = " Прочие активы и прочие пассивы" }
                                                             , new Class { Id = "7", Name = "Собственный капитал банка" }, new Class { Id = "8", Name = "Доходы банка" }, new Class { Id = "9", Name = "Расходы банка" }});
            base.OnModelCreating(modelBuilder);
        }
    }
}
