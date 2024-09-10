using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieServer.Migrations
{
    /// <inheritdoc />
    public partial class RemoveActorInputField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Actores",
                table: "Movies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Actores",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
