using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Link.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class follower2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUserFollowingID",
                table: "Followings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppUserFollowingID",
                table: "Followings");
        }
    }
}
