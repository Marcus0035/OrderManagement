using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderManagement.Migrations
{
    public partial class RenameDeletedToRemovedInCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "Orders",
                newName: "Removed");

            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "Customers",
                newName: "Removed");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Removed",
                table: "Orders",
                newName: "Deleted");

            migrationBuilder.RenameColumn(
                name: "Removed",
                table: "Customers",
                newName: "Deleted");
        }
    }
}
