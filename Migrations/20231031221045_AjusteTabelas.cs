using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentAPI.Migrations
{
    public partial class AjusteTabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Bikes_BikeId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Rents_Bikes_BikeId",
                table: "Rents");

            migrationBuilder.DropForeignKey(
                name: "FK_Rents_Users_UserId",
                table: "Rents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rents",
                table: "Rents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bikes",
                table: "Bikes");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Rents",
                newName: "Rent");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "Image");

            migrationBuilder.RenameTable(
                name: "Bikes",
                newName: "Bike");

            migrationBuilder.RenameIndex(
                name: "IX_Users_UserEmail",
                table: "User",
                newName: "IX_User_UserEmail");

            migrationBuilder.RenameIndex(
                name: "IX_Rents_UserId",
                table: "Rent",
                newName: "IX_Rent_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Rents_BikeId",
                table: "Rent",
                newName: "IX_Rent_BikeId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_BikeId",
                table: "Image",
                newName: "IX_Image_BikeId");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserName",
                keyValue: null,
                column: "UserName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "User",
                type: "varchar(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserEmail",
                keyValue: null,
                column: "UserEmail",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserEmail",
                table: "User",
                type: "varchar(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Password",
                keyValue: null,
                column: "Password",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                type: "varchar(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Image",
                keyColumn: "Url",
                keyValue: null,
                column: "Url",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Image",
                type: "varchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Bike",
                keyColumn: "Name",
                keyValue: null,
                column: "Name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Bike",
                type: "varchar(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Bike",
                keyColumn: "Description",
                keyValue: null,
                column: "Description",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Bike",
                type: "varchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rent",
                table: "Rent",
                column: "RentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Image",
                table: "Image",
                column: "ImageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bike",
                table: "Bike",
                column: "BikeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Bike_BikeId",
                table: "Image",
                column: "BikeId",
                principalTable: "Bike",
                principalColumn: "BikeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rent_Bike_BikeId",
                table: "Rent",
                column: "BikeId",
                principalTable: "Bike",
                principalColumn: "BikeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rent_User_UserId",
                table: "Rent",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Bike_BikeId",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_Rent_Bike_BikeId",
                table: "Rent");

            migrationBuilder.DropForeignKey(
                name: "FK_Rent_User_UserId",
                table: "Rent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rent",
                table: "Rent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Image",
                table: "Image");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bike",
                table: "Bike");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Rent",
                newName: "Rents");

            migrationBuilder.RenameTable(
                name: "Image",
                newName: "Images");

            migrationBuilder.RenameTable(
                name: "Bike",
                newName: "Bikes");

            migrationBuilder.RenameIndex(
                name: "IX_User_UserEmail",
                table: "Users",
                newName: "IX_Users_UserEmail");

            migrationBuilder.RenameIndex(
                name: "IX_Rent_UserId",
                table: "Rents",
                newName: "IX_Rents_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Rent_BikeId",
                table: "Rents",
                newName: "IX_Rents_BikeId");

            migrationBuilder.RenameIndex(
                name: "IX_Image_BikeId",
                table: "Images",
                newName: "IX_Images_BikeId");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(80)",
                oldMaxLength: 80)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "UserEmail",
                table: "Users",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(80)",
                oldMaxLength: 80)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(80)",
                oldMaxLength: 80)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Images",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(300)",
                oldMaxLength: 300)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Bikes",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(80)",
                oldMaxLength: 80)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Bikes",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(300)",
                oldMaxLength: 300)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rents",
                table: "Rents",
                column: "RentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "ImageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bikes",
                table: "Bikes",
                column: "BikeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Bikes_BikeId",
                table: "Images",
                column: "BikeId",
                principalTable: "Bikes",
                principalColumn: "BikeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_Bikes_BikeId",
                table: "Rents",
                column: "BikeId",
                principalTable: "Bikes",
                principalColumn: "BikeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rents_Users_UserId",
                table: "Rents",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
