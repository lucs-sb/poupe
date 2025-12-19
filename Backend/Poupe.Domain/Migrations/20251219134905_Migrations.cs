using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poupe.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Migrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "PoupeDB");

            migrationBuilder.CreateTable(
                name: "tb_category",
                schema: "PoupeDB",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    purpose = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_user",
                schema: "PoupeDB",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_transaction",
                schema: "PoupeDB",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    value = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_transaction", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_transaction_tb_category_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "PoupeDB",
                        principalTable: "tb_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_transaction_tb_user_UserId",
                        column: x => x.UserId,
                        principalSchema: "PoupeDB",
                        principalTable: "tb_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_transaction_CategoryId",
                schema: "PoupeDB",
                table: "tb_transaction",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_transaction_UserId",
                schema: "PoupeDB",
                table: "tb_transaction",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_transaction",
                schema: "PoupeDB");

            migrationBuilder.DropTable(
                name: "tb_category",
                schema: "PoupeDB");

            migrationBuilder.DropTable(
                name: "tb_user",
                schema: "PoupeDB");
        }
    }
}
