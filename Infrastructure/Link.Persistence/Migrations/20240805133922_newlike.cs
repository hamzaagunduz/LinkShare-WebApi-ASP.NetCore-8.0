using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Link.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class newlike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Like",
                columns: table => new
                {
                    LikeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserID = table.Column<int>(type: "int", nullable: false),
                    ProfileCommentID = table.Column<int>(type: "int", nullable: true),
                    AnswerID = table.Column<int>(type: "int", nullable: true),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Like", x => x.LikeID);
                    table.ForeignKey(
                        name: "FK_Like_Answers_AnswerID",
                        column: x => x.AnswerID,
                        principalTable: "Answers",
                        principalColumn: "AnswerID");
                    table.ForeignKey(
                        name: "FK_Like_AspNetUsers_AppUserID",
                        column: x => x.AppUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Like_ProfileComments_ProfileCommentID",
                        column: x => x.ProfileCommentID,
                        principalTable: "ProfileComments",
                        principalColumn: "ProfileCommentID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Like_AnswerID",
                table: "Like",
                column: "AnswerID");

            migrationBuilder.CreateIndex(
                name: "IX_Like_AppUserID",
                table: "Like",
                column: "AppUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Like_ProfileCommentID",
                table: "Like",
                column: "ProfileCommentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Like");
        }
    }
}
