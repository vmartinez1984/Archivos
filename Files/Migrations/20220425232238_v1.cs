using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Files.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Folder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FolderId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DateRegistration = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DateRegistration = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FilesUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArchiveId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DateRegistration = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilesUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    FilesUsersId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DateRegistration = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_FilesUsers_FilesUsersId",
                        column: x => x.FilesUsersId,
                        principalTable: "FilesUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Archive",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Base64 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FolderId = table.Column<int>(type: "int", nullable: false),
                    OnlyRead = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DateRegistration = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Archive", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Archive_Folder_FolderId",
                        column: x => x.FolderId,
                        principalTable: "Folder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Archive_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "DateRegistration", "IsActive", "Name" },
                values: new object[] { 1, new DateTime(2022, 4, 25, 18, 22, 37, 603, DateTimeKind.Local).AddTicks(4303), true, "Administrador" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "DateRegistration", "IsActive", "Name" },
                values: new object[] { 2, new DateTime(2022, 4, 25, 18, 22, 37, 603, DateTimeKind.Local).AddTicks(5205), true, "Colaborador" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "DateRegistration", "FilesUsersId", "IsActive", "LastName", "Name", "Password", "RoleId", "UserName" },
                values: new object[] { 1, new DateTime(2022, 4, 25, 18, 22, 37, 605, DateTimeKind.Local).AddTicks(2560), null, true, "", "Administrador", "123456", 1, "administrador" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "DateRegistration", "FilesUsersId", "IsActive", "LastName", "Name", "Password", "RoleId", "UserName" },
                values: new object[] { 2, new DateTime(2022, 4, 25, 18, 22, 37, 605, DateTimeKind.Local).AddTicks(4525), null, true, "", "Colaborador", "123456", 2, "colaborador" });

            migrationBuilder.CreateIndex(
                name: "IX_Archive_FolderId",
                table: "Archive",
                column: "FolderId");

            migrationBuilder.CreateIndex(
                name: "IX_Archive_UserId",
                table: "Archive",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FilesUsers_ArchiveId",
                table: "FilesUsers",
                column: "ArchiveId");

            migrationBuilder.CreateIndex(
                name: "IX_User_FilesUsersId",
                table: "User",
                column: "FilesUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_FilesUsers_Archive_ArchiveId",
                table: "FilesUsers",
                column: "ArchiveId",
                principalTable: "Archive",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Archive_Folder_FolderId",
                table: "Archive");

            migrationBuilder.DropForeignKey(
                name: "FK_Archive_User_UserId",
                table: "Archive");

            migrationBuilder.DropTable(
                name: "Folder");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "FilesUsers");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Archive");
        }
    }
}
