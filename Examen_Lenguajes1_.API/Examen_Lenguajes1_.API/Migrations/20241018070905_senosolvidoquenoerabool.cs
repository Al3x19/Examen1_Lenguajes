using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen_Lenguajes1_.API.Migrations
{
    /// <inheritdoc />
    public partial class senosolvidoquenoerabool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "state",
                schema: "dbo",
                table: "Requests",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "state",
                schema: "dbo",
                table: "Requests",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);
        }
    }
}
