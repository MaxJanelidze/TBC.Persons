using Microsoft.EntityFrameworkCore.Migrations;

namespace TBC.Persons.Infrastructure.Persistence.Migrations
{
    public partial class Make_PersonalNumberIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PersonalNumber",
                table: "Persons",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_PersonalNumber",
                table: "Persons",
                column: "PersonalNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Persons_PersonalNumber",
                table: "Persons");

            migrationBuilder.AlterColumn<string>(
                name: "PersonalNumber",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 11);
        }
    }
}
