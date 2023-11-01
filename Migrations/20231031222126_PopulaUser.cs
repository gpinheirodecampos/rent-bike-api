using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentAPI.Migrations
{
    public partial class PopulaUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into User(UserName, UserEmail, Password) Values('holymoly', 'gabs@mail.com', 'senha')");
            migrationBuilder.Sql("Insert into User(UserName, UserEmail, Password) Values('digs', 'digs@mail.com', 'senha')");
            migrationBuilder.Sql("Insert into User(UserName, UserEmail, Password) Values('lanchinho', 'lanche@mail.com', 'senha')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from User");
        }
    }
}
