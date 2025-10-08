using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace forms.Migrations
{
    /// <inheritdoc />
    public partial class FormResponse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "id");

            migrationBuilder.CreateTable(
                name: "form_responses",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    form_id = table.Column<string>(type: "varchar(24)", maxLength: 24, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    submitted_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    answers = table.Column<string>(type: "json", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_form_responses", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_FormResponses_Form_User",
                table: "form_responses",
                columns: new[] { "form_id", "user_id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "form_responses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "id");
        }
    }
}
