using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMGCloud.Data.Migrations
{
    public partial class updatecollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_PostCollections_CollectionId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "PostCollections");

            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CollectionName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Collections_CollectionId",
                table: "Posts",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Collections_CollectionId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "Collections");

            migrationBuilder.CreateTable(
                name: "PostCollections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollectionName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCollections", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_PostCollections_CollectionId",
                table: "Posts",
                column: "CollectionId",
                principalTable: "PostCollections",
                principalColumn: "Id");
        }
    }
}
