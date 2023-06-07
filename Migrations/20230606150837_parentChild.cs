using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace CurriculumTemplateDemo.Migrations
{
    /// <inheritdoc />
    public partial class parentChild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Student = table.Column<string>(type: "longtext", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false),
                    staff = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CurriculumEventTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurriculumEventTypes", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Curriculums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Program = table.Column<string>(type: "longtext", nullable: false),
                    Campus = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curriculums", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CurriculumsModules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    CurriculumId = table.Column<int>(type: "int", nullable: false),
                    required = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurriculumsModules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurriculumsModules_Curriculums_CurriculumId",
                        column: x => x.CurriculumId,
                        principalTable: "Curriculums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CurriculumEventTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CurriculumSectionId = table.Column<int>(type: "int", nullable: false),
                    CurriculumEventTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurriculumEventTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurriculumEventTemplates_CurriculumEventTypes_CurriculumEven~",
                        column: x => x.CurriculumEventTypeId,
                        principalTable: "CurriculumEventTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurriculumEventTemplates_CurriculumsModules_CurriculumSectio~",
                        column: x => x.CurriculumSectionId,
                        principalTable: "CurriculumsModules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StudentEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    EventDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CurriculumEventTemplateId = table.Column<int>(type: "int", nullable: false),
                    StudentFirstName = table.Column<string>(type: "longtext", nullable: false),
                    StudentLastName = table.Column<string>(type: "longtext", nullable: false),
                    Cohort = table.Column<string>(type: "longtext", nullable: false),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AttendanceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentEvents_Attendances_AttendanceId",
                        column: x => x.AttendanceId,
                        principalTable: "Attendances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentEvents_CurriculumEventTemplates_CurriculumEventTempla~",
                        column: x => x.CurriculumEventTemplateId,
                        principalTable: "CurriculumEventTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumEventTemplates_CurriculumEventTypeId",
                table: "CurriculumEventTemplates",
                column: "CurriculumEventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumEventTemplates_CurriculumSectionId",
                table: "CurriculumEventTemplates",
                column: "CurriculumSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumsModules_CurriculumId",
                table: "CurriculumsModules",
                column: "CurriculumId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEvents_AttendanceId",
                table: "StudentEvents",
                column: "AttendanceId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEvents_CurriculumEventTemplateId",
                table: "StudentEvents",
                column: "CurriculumEventTemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentEvents");

            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "CurriculumEventTemplates");

            migrationBuilder.DropTable(
                name: "CurriculumEventTypes");

            migrationBuilder.DropTable(
                name: "CurriculumsModules");

            migrationBuilder.DropTable(
                name: "Curriculums");
        }
    }
}
