using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alpaca.Portal.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class Notices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notice",
                columns: table => new
                {
                    NoticeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NoticeTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notice", x => x.NoticeId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notice");
        }
    }
}
