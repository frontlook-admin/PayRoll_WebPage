using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace payroll_app.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grade",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GradeName = table.Column<string>(name: "Grade Name", maxLength: 30, nullable: false),
                    GradeCode = table.Column<string>(name: "Grade Code", maxLength: 30, nullable: false),
                    ArrangeOrder = table.Column<int>(name: "Arrange Order", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grade", x => x.ID);
                    table.UniqueConstraint("AK_Grade_Grade Code", x => x.GradeCode);
                    table.UniqueConstraint("AK_Grade_Grade Name", x => x.GradeName);
                    table.UniqueConstraint("AK_Grade_Grade Code_Grade Name_ID", x => new { x.GradeCode, x.GradeName, x.ID });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Grade");
        }
    }
}
