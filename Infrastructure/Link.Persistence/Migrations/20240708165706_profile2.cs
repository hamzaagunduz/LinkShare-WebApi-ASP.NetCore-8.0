using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Link.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class profile2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Linkes_AspNetUsers_AppUserId",
                table: "Linkes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileComments_AspNetUsers_AppUserID",
                table: "ProfileComments");

            migrationBuilder.RenameColumn(
                name: "AppUserID",
                table: "ProfileComments",
                newName: "ProfileID");

            migrationBuilder.RenameIndex(
                name: "IX_ProfileComments_AppUserID",
                table: "ProfileComments",
                newName: "IX_ProfileComments_ProfileID");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Linkes",
                newName: "AppUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Linkes_AppUserId",
                table: "Linkes",
                newName: "IX_Linkes_AppUserID");

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    ProfileID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.ProfileID);
                    table.ForeignKey(
                        name: "FK_Profiles_AspNetUsers_AppUserID",
                        column: x => x.AppUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_AppUserID",
                table: "Profiles",
                column: "AppUserID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Linkes_AspNetUsers_AppUserID",
                table: "Linkes",
                column: "AppUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileComments_Profiles_ProfileID",
                table: "ProfileComments",
                column: "ProfileID",
                principalTable: "Profiles",
                principalColumn: "ProfileID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Linkes_AspNetUsers_AppUserID",
                table: "Linkes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileComments_Profiles_ProfileID",
                table: "ProfileComments");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.RenameColumn(
                name: "ProfileID",
                table: "ProfileComments",
                newName: "AppUserID");

            migrationBuilder.RenameIndex(
                name: "IX_ProfileComments_ProfileID",
                table: "ProfileComments",
                newName: "IX_ProfileComments_AppUserID");

            migrationBuilder.RenameColumn(
                name: "AppUserID",
                table: "Linkes",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Linkes_AppUserID",
                table: "Linkes",
                newName: "IX_Linkes_AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Linkes_AspNetUsers_AppUserId",
                table: "Linkes",
                column: "AppUserId",
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
    }
}
