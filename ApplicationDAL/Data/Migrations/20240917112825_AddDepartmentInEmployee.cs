using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationDAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDepartmentInEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_employees_DepartmentId",
                table: "employees",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_employees_departments_DepartmentId",
                table: "employees",
                column: "DepartmentId",
                principalTable: "departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employees_departments_DepartmentId",
                table: "employees");

            migrationBuilder.DropIndex(
                name: "IX_employees_DepartmentId",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "employees");
        }
    }
}
