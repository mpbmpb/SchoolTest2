using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolTest2.Migrations
{
    public partial class addedCourseDesign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseDesign",
                columns: table => new
                {
                    CourseDesignId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CourseDesignId1 = table.Column<int>(nullable: true),
                    SeminarId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseDesign", x => x.CourseDesignId);
                    table.ForeignKey(
                        name: "FK_CourseDesign_CourseDesign_CourseDesignId1",
                        column: x => x.CourseDesignId1,
                        principalTable: "CourseDesign",
                        principalColumn: "CourseDesignId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseDesign_Seminars_SeminarId",
                        column: x => x.SeminarId,
                        principalTable: "Seminars",
                        principalColumn: "SeminarId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseSeminars",
                columns: table => new
                {
                    CourseDesignId = table.Column<int>(nullable: false),
                    SeminarId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSeminars", x => new { x.CourseDesignId, x.SeminarId });
                    table.ForeignKey(
                        name: "FK_CourseSeminars_CourseDesign_CourseDesignId",
                        column: x => x.CourseDesignId,
                        principalTable: "CourseDesign",
                        principalColumn: "CourseDesignId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseSeminars_Seminars_SeminarId",
                        column: x => x.SeminarId,
                        principalTable: "Seminars",
                        principalColumn: "SeminarId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseDesign_CourseDesignId1",
                table: "CourseDesign",
                column: "CourseDesignId1");

            migrationBuilder.CreateIndex(
                name: "IX_CourseDesign_SeminarId",
                table: "CourseDesign",
                column: "SeminarId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSeminars_SeminarId",
                table: "CourseSeminars",
                column: "SeminarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseSeminars");

            migrationBuilder.DropTable(
                name: "CourseDesign");
        }
    }
}
