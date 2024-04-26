using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Zeemlin.Data.Migrations
{
    /// <inheritdoc />
    public partial class ClassesUpdatedMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Students_UserId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Questiones_Quizzes_QuizId",
                table: "Questiones");

            migrationBuilder.DropForeignKey(
                name: "FK_Questiones_Teachers_TeacherId",
                table: "Questiones");

            migrationBuilder.DropTable(
                name: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Questiones_QuizId",
                table: "Questiones");

            migrationBuilder.DropIndex(
                name: "IX_Grades_UserId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "QuizId",
                table: "Questiones");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "Questiones",
                newName: "LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_Questiones_TeacherId",
                table: "Questiones",
                newName: "IX_Questiones_LessonId");

            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                table: "Schools",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "StudentId",
                table: "Schools",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Lessons",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<long>(
                name: "StudentId",
                table: "Grades",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                table: "Events",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "StudentId",
                table: "Events",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TeacherId",
                table: "Events",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EventRegistrations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RegistrationCode = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventRegistrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventRegistrations_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoLessonAssets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false),
                    UploadedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LessonId = table.Column<long>(type: "bigint", nullable: false),
                    ContentType = table.Column<string>(type: "text", nullable: true),
                    Size = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoLessonAssets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoLessonAssets_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7392));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7395));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7398));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7400));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7401));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7403));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7404));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7406));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 9L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7407));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 10L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7409));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 11L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7410));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 12L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7412));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 13L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7413));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 14L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7415));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 15L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7416));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 16L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7417));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 17L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7421));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 18L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7423));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 19L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7425));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 20L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7426));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 21L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7427));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 22L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7429));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 23L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7430));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 24L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7431));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 25L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7433));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7770));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7773));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7774));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7776));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7777));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7779));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7780));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7781));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 9L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7782));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 10L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7832));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 11L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7833));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 12L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7834));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 13L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7835));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 14L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7837));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 15L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7838));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 16L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7839));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 17L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7840));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 18L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7842));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 19L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7843));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 20L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7844));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 21L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7845));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 22L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7847));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 23L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7848));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 24L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7849));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 25L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7850));

            migrationBuilder.UpdateData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7490));

            migrationBuilder.UpdateData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7494));

            migrationBuilder.UpdateData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7496));

            migrationBuilder.UpdateData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7500));

            migrationBuilder.UpdateData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7501));

            migrationBuilder.UpdateData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7503));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7903));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7906));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7908));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7909));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7910));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7912));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7913));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7914));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 9L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7915));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 10L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7917));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 11L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7918));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 12L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7919));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 13L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7920));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 14L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7921));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 15L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7922));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 16L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7923));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 17L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7924));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 18L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7926));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 19L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7927));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 20L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7928));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 21L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7929));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 22L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7930));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 23L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7931));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 24L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7933));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 25L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7934));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 26L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7935));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 27L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7936));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 28L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7937));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 29L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7938));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 30L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7939));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 31L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7940));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 32L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7941));

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8084));

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8088));

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8090));

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8091));

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8092));

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8094));

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8095));

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8096));

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 9L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8098));

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 10L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8099));

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 11L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8100));

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 12L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8101));

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7990));

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7995));

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7997));

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7998));

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8000));

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8001));

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8003));

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8004));

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 9L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8005));

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 10L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8007));

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 11L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8009));

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 12L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8010));

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 13L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8011));

            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "ParentId", "StudentId" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7688), null, null });

            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "ParentId", "StudentId" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7693), null, null });

            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "ParentId", "StudentId" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7695), null, null });

            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "ParentId", "StudentId" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7697), null, null });

            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedAt", "ParentId", "StudentId" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7699), null, null });

            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedAt", "ParentId", "StudentId" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7701), null, null });

            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedAt", "ParentId", "StudentId" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7703), null, null });

            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedAt", "ParentId", "StudentId" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7705), null, null });

            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedAt", "ParentId", "StudentId" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7706), null, null });

            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedAt", "ParentId", "StudentId" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7709), null, null });

            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CreatedAt", "ParentId", "StudentId" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7710), null, null });

            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "CreatedAt", "ParentId", "StudentId" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7712), null, null });

            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "CreatedAt", "ParentId", "StudentId" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7714), null, null });

            migrationBuilder.UpdateData(
                table: "SuperAdmins",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7295));

            migrationBuilder.UpdateData(
                table: "SuperAdmins",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7315));

            migrationBuilder.UpdateData(
                table: "SuperAdmins",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7318));

            migrationBuilder.UpdateData(
                table: "SuperAdmins",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7319));

            migrationBuilder.UpdateData(
                table: "SuperAdmins",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7321));

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8150), (byte)3 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8154), (byte)3 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8155), (byte)3 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8157), (byte)3 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8158), (byte)3 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8160), (byte)3 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8161), (byte)3 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8162), (byte)3 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8163), (byte)3 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8165), (byte)3 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8166), (byte)3 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8167), (byte)3 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8168), (byte)3 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8169), (byte)3 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8170), (byte)3 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8171), (byte)3 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8172), (byte)3 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8174), (byte)3 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8175), (byte)3 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 20L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8176), (byte)3 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 21L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8177), (byte)3 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 22L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8178), (byte)3 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 23L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8179), (byte)3 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 24L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8180), (byte)3 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 25L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(8181), (byte)3 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7554), (byte)13 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7559), (byte)4 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7561), (byte)19 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7594), (byte)13 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7596), (byte)11 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7598), (byte)18 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7600), (byte)1 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7603), (byte)2 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7604), (byte)13 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7606), (byte)17 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7608), (byte)4 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7609), (byte)16 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7611), (byte)14 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7612), (byte)4 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7614), (byte)6 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7615), (byte)19 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7617), (byte)17 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7619), (byte)2 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7620), (byte)13 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 20L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7621), (byte)16 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 21L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7623), (byte)18 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 22L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7624), (byte)14 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 23L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7626), (byte)1 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 24L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7627), (byte)4 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 25L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 4, 17, 10, 23, 40, 590, DateTimeKind.Utc).AddTicks(7629), (byte)21 });

            migrationBuilder.CreateIndex(
                name: "IX_Schools_ParentId",
                table: "Schools",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_StudentId",
                table: "Schools",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_StudentId",
                table: "Grades",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_ParentId",
                table: "Events",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_StudentId",
                table: "Events",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_TeacherId",
                table: "Events",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistrations_EventId",
                table: "EventRegistrations",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoLessonAssets_LessonId",
                table: "VideoLessonAssets",
                column: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Parents_ParentId",
                table: "Events",
                column: "ParentId",
                principalTable: "Parents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Students_StudentId",
                table: "Events",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Teachers_TeacherId",
                table: "Events",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Students_StudentId",
                table: "Grades",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questiones_Lessons_LessonId",
                table: "Questiones",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Parents_ParentId",
                table: "Schools",
                column: "ParentId",
                principalTable: "Parents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Students_StudentId",
                table: "Schools",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Parents_ParentId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Students_StudentId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Teachers_TeacherId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Students_StudentId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Questiones_Lessons_LessonId",
                table: "Questiones");

            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Parents_ParentId",
                table: "Schools");

            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Students_StudentId",
                table: "Schools");

            migrationBuilder.DropTable(
                name: "EventRegistrations");

            migrationBuilder.DropTable(
                name: "VideoLessonAssets");

            migrationBuilder.DropIndex(
                name: "IX_Schools_ParentId",
                table: "Schools");

            migrationBuilder.DropIndex(
                name: "IX_Schools_StudentId",
                table: "Schools");

            migrationBuilder.DropIndex(
                name: "IX_Grades_StudentId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Events_ParentId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_StudentId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_TeacherId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "LessonId",
                table: "Questiones",
                newName: "TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_Questiones_LessonId",
                table: "Questiones",
                newName: "IX_Questiones_TeacherId");

            migrationBuilder.AddColumn<long>(
                name: "QuizId",
                table: "Questiones",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Lessons",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SchoolId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Subject = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quizzes_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6685));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6689));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6692));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6693));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6696));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6698));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6700));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6702));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 9L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6703));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 10L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6705));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 11L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6707));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 12L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6708));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 13L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6710));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 14L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6711));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 15L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6713));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 16L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6714));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 17L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6716));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 18L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6718));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 19L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6720));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 20L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6721));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 21L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6723));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 22L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6724));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 23L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6726));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 24L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6733));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 25L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6735));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7126));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7130));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7132));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7133));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7135));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7136));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7138));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7139));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 9L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7140));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 10L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7142));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 11L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7143));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 12L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7145));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 13L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7147));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 14L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7148));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 15L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7150));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 16L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7151));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 17L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7152));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 18L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7154));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 19L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7155));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 20L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7156));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 21L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7158));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 22L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7159));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 23L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7161));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 24L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7162));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 25L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7163));

            migrationBuilder.UpdateData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6815));

            migrationBuilder.UpdateData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6822));

            migrationBuilder.UpdateData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6825));

            migrationBuilder.UpdateData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6827));

            migrationBuilder.UpdateData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6828));

            migrationBuilder.UpdateData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6831));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7226));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7233));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7235));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7236));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7238));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7240));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7241));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7242));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 9L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7244));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 10L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7246));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 11L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7247));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 12L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7248));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 13L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7249));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 14L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7250));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 15L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7252));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 16L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7253));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 17L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7254));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 18L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7256));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 19L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7257));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 20L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7258));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 21L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7259));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 22L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7260));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 23L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7261));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 24L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7263));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 25L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7264));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 26L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7265));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 27L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7266));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 28L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7267));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 29L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7268));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 30L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7270));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 31L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7271));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 32L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7272));

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7413));

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7418));

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7420));

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7421));

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7423));

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7425));

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7426));

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7427));

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 9L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7429));

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 10L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7431));

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 11L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7432));

            migrationBuilder.UpdateData(
                table: "Homeworks",
                keyColumn: "Id",
                keyValue: 12L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7433));

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7338));

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7342));

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7344));

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7346));

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7347));

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7349));

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7351));

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7352));

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 9L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7354));

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 10L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7356));

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 11L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7357));

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 12L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7359));

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 13L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7361));

            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7019));

            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7026));

            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7029));

            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7034));

            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7036));

            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7038));

            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7040));

            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7046));

            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 9L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7048));

            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 10L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7050));

            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 11L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7052));

            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 12L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7056));

            migrationBuilder.UpdateData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: 13L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7058));

            migrationBuilder.UpdateData(
                table: "SuperAdmins",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6564));

            migrationBuilder.UpdateData(
                table: "SuperAdmins",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6588));

            migrationBuilder.UpdateData(
                table: "SuperAdmins",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6590));

            migrationBuilder.UpdateData(
                table: "SuperAdmins",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6592));

            migrationBuilder.UpdateData(
                table: "SuperAdmins",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedAt",
                value: new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6594));

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7484), (byte)2 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7487), (byte)2 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7489), (byte)2 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7490), (byte)2 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7491), (byte)2 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7493), (byte)2 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7495), (byte)2 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7496), (byte)2 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7497), (byte)2 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7499), (byte)2 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7500), (byte)2 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7501), (byte)2 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7502), (byte)2 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7504), (byte)2 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7505), (byte)2 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7506), (byte)2 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7507), (byte)2 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7509), (byte)2 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7510), (byte)2 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 20L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7512), (byte)2 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 21L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7513), (byte)2 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 22L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7514), (byte)2 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 23L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7515), (byte)2 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 24L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7517), (byte)2 });

            migrationBuilder.UpdateData(
                table: "TeacherGroups",
                keyColumn: "Id",
                keyValue: 25L,
                columns: new[] { "CreatedAt", "Role" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(7518), (byte)2 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6901), (byte)12 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6911), (byte)3 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6914), (byte)18 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6916), (byte)12 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6918), (byte)10 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6920), (byte)17 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6922), (byte)0 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6924), (byte)1 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6926), (byte)12 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6929), (byte)16 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6930), (byte)3 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6932), (byte)15 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6933), (byte)13 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6935), (byte)3 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6937), (byte)5 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6938), (byte)18 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6940), (byte)16 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6942), (byte)1 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6944), (byte)12 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 20L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6946), (byte)15 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 21L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6947), (byte)17 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 22L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6949), (byte)13 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 23L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6950), (byte)0 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 24L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6952), (byte)3 });

            migrationBuilder.UpdateData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: 25L,
                columns: new[] { "CreatedAt", "ScienceType" },
                values: new object[] { new DateTime(2024, 3, 27, 9, 1, 5, 961, DateTimeKind.Utc).AddTicks(6954), (byte)20 });

            migrationBuilder.CreateIndex(
                name: "IX_Questiones_QuizId",
                table: "Questiones",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_UserId",
                table: "Grades",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_SchoolId",
                table: "Quizzes",
                column: "SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Students_UserId",
                table: "Grades",
                column: "UserId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questiones_Quizzes_QuizId",
                table: "Questiones",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questiones_Teachers_TeacherId",
                table: "Questiones",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
