using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillSnap.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PortfolioUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
             name: "Projects",
             columns: table => new
             {
                 Id = table.Column<int>(type: "int", nullable: false)
                     .Annotation("SqlServer:Identity", "1, 1"),
                 Título = table.Column<string>(type: "nvarchar(max)", nullable: false),
                 Descripción = table.Column<string>(type: "nvarchar(max)", nullable: false),
                 ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                 PortfolioUserId = table.Column<int>(type: "int", nullable: false)
             },
             constraints: table =>
             {
                 table.PrimaryKey("PK_Projects", x => x.Id);
                 table.ForeignKey(
                     name: "FK_Projects_PortfolioUsers_PortfolioUserId",
                     column: x => x.PortfolioUserId,
                     principalTable: "PortfolioUsers",
                     principalColumn: "Id",
                     onDelete: ReferentialAction.Cascade);
             });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nivel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PortfolioUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_PortfolioUsers_PortfolioUserId",
                        column: x => x.PortfolioUserId,
                        principalTable: "PortfolioUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {           

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "PortfolioUsers");
        }
    }
}
