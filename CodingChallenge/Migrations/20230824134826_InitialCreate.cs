using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodingChallenge.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Address = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Address);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    TokenId = table.Column<string>(type: "TEXT", nullable: false),
                    WalletAddress = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.TokenId);
                    table.ForeignKey(
                        name: "FK_Tokens_Wallets_WalletAddress",
                        column: x => x.WalletAddress,
                        principalTable: "Wallets",
                        principalColumn: "Address");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_WalletAddress",
                table: "Tokens",
                column: "WalletAddress");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "Wallets");
        }
    }
}
