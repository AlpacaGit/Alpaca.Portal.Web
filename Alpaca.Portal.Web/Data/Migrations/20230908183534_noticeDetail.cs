using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alpaca.Portal.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class noticeDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NoticeDetail",
                columns: table => new
                {
                    NoticeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NoticeBody = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoticeDetail", x => x.NoticeId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NoticeDetail");
        }
    }
}
