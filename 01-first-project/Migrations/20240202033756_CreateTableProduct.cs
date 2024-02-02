﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _01_first_project.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    code = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    name = table.Column<string>(type: "VARCHAR(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.code);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product");
        }
    }
}
