using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EMService.Migrations
{
    public partial class EntityDevSystem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

         

            migrationBuilder.CreateTable(
                name: "EMS_Modeling_Class",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DeviceType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMS_Modeling_Class", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EMS_Modeling_System",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SystemGroup = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SystemClass = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMS_Modeling_System", x => x.Id);
                });

         
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EMS_Modeling_Class");

            migrationBuilder.DropTable(
                name: "EMS_Modeling_System");

        }
    }
}
