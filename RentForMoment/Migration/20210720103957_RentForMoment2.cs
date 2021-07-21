
namespace RentForMoment.Migration
{
    using Microsoft.EntityFrameworkCore.Migrations;
    public partial class RentForMoment2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Skills",
                table: "PersonProfiles",
                type: "nvarchar(88)",
                maxLength: 88,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(888)",
                oldMaxLength: 888);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "PersonProfiles",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "TypeOfWork",
                table: "PersonProfiles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeOfWork",
                table: "PersonProfiles");

            migrationBuilder.AlterColumn<string>(
                name: "Skills",
                table: "PersonProfiles",
                type: "nvarchar(888)",
                maxLength: 888,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(88)",
                oldMaxLength: 88);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "PersonProfiles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);
        }
    }
}
