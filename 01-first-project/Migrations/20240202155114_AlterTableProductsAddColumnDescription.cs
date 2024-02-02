using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _01_first_project.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableProductsAddColumnDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "product",
                type: "VARCHAR(255)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "product");
        }
    }
}
