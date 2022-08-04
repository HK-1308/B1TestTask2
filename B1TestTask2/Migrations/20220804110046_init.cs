using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace B1TestTask2.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BalanceАccounts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    BalacnceAccountNumber = table.Column<string>(type: "TEXT", nullable: false),
                    ClassId = table.Column<string>(type: "TEXT", nullable: false),
                    FileId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalanceАccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BalanceАccounts_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BalanceАccounts_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IncomingBalances",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Active = table.Column<decimal>(type: "TEXT", nullable: false),
                    Passive = table.Column<decimal>(type: "TEXT", nullable: false),
                    BalanceAccountId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomingBalances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncomingBalances_BalanceАccounts_BalanceAccountId",
                        column: x => x.BalanceAccountId,
                        principalTable: "BalanceАccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Turnovers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Debit = table.Column<decimal>(type: "TEXT", nullable: false),
                    Credit = table.Column<decimal>(type: "TEXT", nullable: false),
                    BalanceAccountId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnovers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turnovers_BalanceАccounts_BalanceAccountId",
                        column: x => x.BalanceAccountId,
                        principalTable: "BalanceАccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "Name" },
                values: new object[] { "1", "Денежные средства, драгоценные металлы и межбанковские операции" });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "Name" },
                values: new object[] { "2", "Кредитные и иные активные операции с клиентами" });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "Name" },
                values: new object[] { "3", " Счета по операциям клиентов" });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "Name" },
                values: new object[] { "4", " Ценные бумаги" });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "Name" },
                values: new object[] { "5", "Долгосрочные финансовые вложения в уставные фонды юридических лиц, основные средства и прочее имущество" });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "Name" },
                values: new object[] { "6", " Прочие активы и прочие пассивы" });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "Name" },
                values: new object[] { "7", "Собственный капитал банка" });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "Name" },
                values: new object[] { "8", "Доходы банка" });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "Name" },
                values: new object[] { "9", "Расходы банка" });

            migrationBuilder.CreateIndex(
                name: "IX_BalanceАccounts_ClassId",
                table: "BalanceАccounts",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_BalanceАccounts_FileId",
                table: "BalanceАccounts",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomingBalances_BalanceAccountId",
                table: "IncomingBalances",
                column: "BalanceAccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Turnovers_BalanceAccountId",
                table: "Turnovers",
                column: "BalanceAccountId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IncomingBalances");

            migrationBuilder.DropTable(
                name: "Turnovers");

            migrationBuilder.DropTable(
                name: "BalanceАccounts");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Files");
        }
    }
}
