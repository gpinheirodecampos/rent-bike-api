using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentAPI.Migrations
{
    public partial class PopulaRent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Rent(DateStart, BikeId, UserId) Values(now(), 1, 1)");
            migrationBuilder.Sql("Insert into Rent(DateStart, BikeId, UserId) Values(now(), 2, 2)");
            migrationBuilder.Sql("Insert into Rent(DateStart, BikeId, UserId) Values(now(), 3, 3)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Rent");
        }
    }
}
