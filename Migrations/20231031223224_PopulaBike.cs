using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentAPI.Migrations
{
    public partial class PopulaBike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Bike(Name, Description, TypeBike) Values('Caloi', 'Bike incrivel', 1)");
            migrationBuilder.Sql("Insert into Bike(Name, Description, TypeBike) Values('Mountain', 'Bike otima', 2)");
            migrationBuilder.Sql("Insert into Bike(Name, Description, TypeBike) Values('Hero', 'Bike top', 1)");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Bike");
        }
    }
}
