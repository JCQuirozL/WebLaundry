using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebLaundry.Migrations
{
    public partial class ServiceTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<int>(
                name: "ServiceTypeId",
                table: "CLOTHING_TYPE",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ServiceTypes",
                columns: table => new
                {
                    ServiceTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTypes", x => x.ServiceTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CLOTHING_TYPE_ServiceTypeId",
                table: "CLOTHING_TYPE",
                column: "ServiceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CLOTHING_TYPE_ServiceTypes_ServiceTypeId",
                table: "CLOTHING_TYPE",
                column: "ServiceTypeId",
                principalTable: "ServiceTypes",
                principalColumn: "ServiceTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CLOTHING_TYPE_ServiceTypes_ServiceTypeId",
                table: "CLOTHING_TYPE");

            migrationBuilder.DropTable(
                name: "ServiceTypes");

            migrationBuilder.DropIndex(
                name: "IX_CLOTHING_TYPE_ServiceTypeId",
                table: "CLOTHING_TYPE");

           
            migrationBuilder.DropColumn(
                name: "ServiceTypeId",
                table: "CLOTHING_TYPE");

           
        }
    }
}
