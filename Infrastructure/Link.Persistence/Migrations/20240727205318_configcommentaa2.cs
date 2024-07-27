using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Link.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class configcommentaa2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_ProfileComments_ProfileCommentID",
                table: "Answers");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_ProfileComments_ProfileCommentID",
                table: "Answers",
                column: "ProfileCommentID",
                principalTable: "ProfileComments",
                principalColumn: "ProfileCommentID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_ProfileComments_ProfileCommentID",
                table: "Answers");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_ProfileComments_ProfileCommentID",
                table: "Answers",
                column: "ProfileCommentID",
                principalTable: "ProfileComments",
                principalColumn: "ProfileCommentID");
        }
    }
}
