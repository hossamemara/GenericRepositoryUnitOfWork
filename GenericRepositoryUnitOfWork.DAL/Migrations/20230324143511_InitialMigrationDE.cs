using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GenericRepositoryUnitOfWork.DAL.Migrations
{
    public partial class InitialMigrationDE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupperId",
                table: "Employees");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SupperId",
                table: "Employees",
                type: "int",
                nullable: true);
        }
    }
}
