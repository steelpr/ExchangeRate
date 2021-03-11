using Microsoft.EntityFrameworkCore.Migrations;

namespace ASPCore.Data.Migrations
{
    public partial class InitialFavoriteCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserFavoriteCurrencies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    ExchangeRateUserId = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavoriteCurrencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFavoriteCurrencies_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFavoriteCurrencies_AspNetUsers_ExchangeRateUserId",
                        column: x => x.ExchangeRateUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoriteCurrencies_CurrencyId",
                table: "UserFavoriteCurrencies",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoriteCurrencies_ExchangeRateUserId",
                table: "UserFavoriteCurrencies",
                column: "ExchangeRateUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFavoriteCurrencies");
        }
    }
}
