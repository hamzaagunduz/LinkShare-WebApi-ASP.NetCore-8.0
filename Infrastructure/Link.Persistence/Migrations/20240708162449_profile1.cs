using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Link.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class profile1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_ProfileComment_ProfileCommentID",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_Follower_AspNetUsers_AppUserID",
                table: "Follower");

            migrationBuilder.DropForeignKey(
                name: "FK_Following_AspNetUsers_AppUserID",
                table: "Following");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileComment_AspNetUsers_AppUserID",
                table: "ProfileComment");

            migrationBuilder.DropTable(
                name: "Link");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfileComment",
                table: "ProfileComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Following",
                table: "Following");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Follower",
                table: "Follower");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Answer",
                table: "Answer");

            migrationBuilder.RenameTable(
                name: "ProfileComment",
                newName: "ProfileComments");

            migrationBuilder.RenameTable(
                name: "Following",
                newName: "Followings");

            migrationBuilder.RenameTable(
                name: "Follower",
                newName: "Followers");

            migrationBuilder.RenameTable(
                name: "Answer",
                newName: "Answers");

            migrationBuilder.RenameIndex(
                name: "IX_ProfileComment_AppUserID",
                table: "ProfileComments",
                newName: "IX_ProfileComments_AppUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Following_AppUserID",
                table: "Followings",
                newName: "IX_Followings_AppUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Follower_AppUserID",
                table: "Followers",
                newName: "IX_Followers_AppUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Answer_ProfileCommentID",
                table: "Answers",
                newName: "IX_Answers_ProfileCommentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfileComments",
                table: "ProfileComments",
                column: "ProfileCommentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Followings",
                table: "Followings",
                column: "FollowingID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Followers",
                table: "Followers",
                column: "FollowerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answers",
                table: "Answers",
                column: "AnswerID");

            migrationBuilder.CreateTable(
                name: "Linkes",
                columns: table => new
                {
                    LinkeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Linkes", x => x.LinkeID);
                    table.ForeignKey(
                        name: "FK_Linkes_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Linkes_AppUserId",
                table: "Linkes",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_ProfileComments_ProfileCommentID",
                table: "Answers",
                column: "ProfileCommentID",
                principalTable: "ProfileComments",
                principalColumn: "ProfileCommentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Followers_AspNetUsers_AppUserID",
                table: "Followers",
                column: "AppUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Followings_AspNetUsers_AppUserID",
                table: "Followings",
                column: "AppUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileComments_AspNetUsers_AppUserID",
                table: "ProfileComments",
                column: "AppUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_ProfileComments_ProfileCommentID",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Followers_AspNetUsers_AppUserID",
                table: "Followers");

            migrationBuilder.DropForeignKey(
                name: "FK_Followings_AspNetUsers_AppUserID",
                table: "Followings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileComments_AspNetUsers_AppUserID",
                table: "ProfileComments");

            migrationBuilder.DropTable(
                name: "Linkes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfileComments",
                table: "ProfileComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Followings",
                table: "Followings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Followers",
                table: "Followers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Answers",
                table: "Answers");

            migrationBuilder.RenameTable(
                name: "ProfileComments",
                newName: "ProfileComment");

            migrationBuilder.RenameTable(
                name: "Followings",
                newName: "Following");

            migrationBuilder.RenameTable(
                name: "Followers",
                newName: "Follower");

            migrationBuilder.RenameTable(
                name: "Answers",
                newName: "Answer");

            migrationBuilder.RenameIndex(
                name: "IX_ProfileComments_AppUserID",
                table: "ProfileComment",
                newName: "IX_ProfileComment_AppUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Followings_AppUserID",
                table: "Following",
                newName: "IX_Following_AppUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Followers_AppUserID",
                table: "Follower",
                newName: "IX_Follower_AppUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Answers_ProfileCommentID",
                table: "Answer",
                newName: "IX_Answer_ProfileCommentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfileComment",
                table: "ProfileComment",
                column: "ProfileCommentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Following",
                table: "Following",
                column: "FollowingID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Follower",
                table: "Follower",
                column: "FollowerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answer",
                table: "Answer",
                column: "AnswerID");

            migrationBuilder.CreateTable(
                name: "Link",
                columns: table => new
                {
                    LinkID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    LinkName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Link", x => x.LinkID);
                    table.ForeignKey(
                        name: "FK_Link_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Link_AppUserId",
                table: "Link",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_ProfileComment_ProfileCommentID",
                table: "Answer",
                column: "ProfileCommentID",
                principalTable: "ProfileComment",
                principalColumn: "ProfileCommentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Follower_AspNetUsers_AppUserID",
                table: "Follower",
                column: "AppUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Following_AspNetUsers_AppUserID",
                table: "Following",
                column: "AppUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileComment_AspNetUsers_AppUserID",
                table: "ProfileComment",
                column: "AppUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
