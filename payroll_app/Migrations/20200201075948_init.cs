using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace payroll_app.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DepartmentName = table.Column<string>(name: "Department Name", maxLength: 30, nullable: false),
                    DepartmentCode = table.Column<string>(name: "Department Code", maxLength: 30, nullable: false),
                    ArrangeOrder = table.Column<int>(name: "Arrange Order", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.ID);
                    table.UniqueConstraint("AK_Department_Department Code", x => x.DepartmentCode);
                    table.UniqueConstraint("AK_Department_Department Name", x => x.DepartmentName);
                    table.UniqueConstraint("AK_Department_Department Code_Department Name_ID", x => new { x.DepartmentCode, x.DepartmentName, x.ID });
                });

            migrationBuilder.CreateTable(
                name: "Worker Type",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CategoryName = table.Column<string>(name: "Category Name", maxLength: 30, nullable: false),
                    CategoryCode = table.Column<string>(name: "Category Code", maxLength: 30, nullable: false),
                    ArrangeOrder = table.Column<int>(name: "Arrange Order", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worker Type", x => x.ID);
                    table.UniqueConstraint("AK_Worker Type_Category Code", x => x.CategoryCode);
                    table.UniqueConstraint("AK_Worker Type_Category Name", x => x.CategoryName);
                    table.UniqueConstraint("AK_Worker Type_Category Code_Category Name_ID", x => new { x.CategoryCode, x.CategoryName, x.ID });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Worker Type");
        }
    }
}
