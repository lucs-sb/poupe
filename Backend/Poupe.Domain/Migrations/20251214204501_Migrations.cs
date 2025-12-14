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
                name: "tb_user",
                schema: "PoupeDB",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    identifier = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_user", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_user",
                schema: "PoupeDB");
        }
    }
}
