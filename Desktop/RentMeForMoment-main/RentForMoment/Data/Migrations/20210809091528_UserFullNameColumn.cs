
namespace RentForMoment.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class UserFullNameColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Chiefs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chiefs_UserId1",
                table: "Chiefs",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Chiefs_AspNetUsers_UserId1",
                table: "Chiefs",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chiefs_AspNetUsers_UserId1",
                table: "Chiefs");

            migrationBuilder.DropIndex(
                name: "IX_Chiefs_UserId1",
                table: "Chiefs");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Chiefs");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");
        }
    }
}
