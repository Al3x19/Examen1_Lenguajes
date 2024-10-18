using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen_Lenguajes1_.API.Migrations
{
    /// <inheritdoc />
    public partial class requesttableXD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "Requests",
                schema: "security",
                newName: "Requests",
                newSchema: "dbo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Requests",
                schema: "dbo",
                newName: "Requests",
                newSchema: "security");
        }
    }
}
