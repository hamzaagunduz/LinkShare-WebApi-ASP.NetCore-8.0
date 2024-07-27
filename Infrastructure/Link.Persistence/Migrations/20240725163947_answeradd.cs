using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Link.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class answeradd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUserID",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppUserID",
                table: "Answers");
        }
    }
}
