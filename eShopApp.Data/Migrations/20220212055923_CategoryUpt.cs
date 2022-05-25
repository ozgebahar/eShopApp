using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopApp.Data.Migrations
{
    public partial class CategoryUpt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "Categories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Categories");
        }
    }
}
