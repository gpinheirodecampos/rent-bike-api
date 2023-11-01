using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentAPI.Migrations
{
    public partial class PopulaImagem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Image(Url, BikeId) Values('caloi.png', 1)");
            migrationBuilder.Sql("Insert into Image(Url, BikeId) Values('mountain.png', 2)");
            migrationBuilder.Sql("Insert into Image(Url, BikeId) Values('hero.png', 3)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Image");
        }
    }
}
