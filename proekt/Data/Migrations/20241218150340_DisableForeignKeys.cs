using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proekt.Data.Migrations
{
    public partial class DisableForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Выполняем команду PRAGMA вне транзакции
            migrationBuilder.Sql("PRAGMA foreign_keys = 0;", suppressTransaction: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Восстанавливаем включение внешних ключей
            migrationBuilder.Sql("PRAGMA foreign_keys = 1;", suppressTransaction: true);
        }
    }
}
