using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumSystemTeamFour.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    IsBlocked = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Threads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 8192, nullable: true),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    Dislikes = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Threads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Threads_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Replies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThreadId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 8192, nullable: true),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    Dislikes = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Replies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Replies_Threads_ThreadId",
                        column: x => x.ThreadId,
                        principalTable: "Threads",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Replies_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ThreadTags",
                columns: table => new
                {
                    ThreadId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThreadTags", x => new { x.TagId, x.ThreadId });
                    table.ForeignKey(
                        name: "FK_ThreadTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ThreadTags_Threads_ThreadId",
                        column: x => x.ThreadId,
                        principalTable: "Threads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReplyVote",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReplyId = table.Column<int>(type: "int", nullable: false),
                    VoterUsername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VoteType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplyVote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReplyVote_Replies_ReplyId",
                        column: x => x.ReplyId,
                        principalTable: "Replies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "##ModPost" },
                    { 2, "Ufo" },
                    { 3, "Skinwalker" },
                    { 4, "Bigfoot" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "IsAdmin", "IsBlocked", "IsDeleted", "LastName", "Password", "PhoneNumber", "Username" },
                values: new object[,]
                {
                    { 1, "FirstnameOne@Lastname.com", "FirstNameOne", true, false, false, "LastNameOne", "passwordOne", null, "UsernameOne" },
                    { 2, "FirstnameTwo@Lastname.com", "FirstNameTwo", false, true, false, "LastNameTwo", "passwordTwo", null, "UsernameTwo" },
                    { 3, "FirstnameThree@Lastname.com", "FirstNameThree", false, false, false, "LastNameThree", "passwordThree", null, "UsernameThree" },
                    { 4, "FirstnameFour@Lastname.com", "FirstNameFour", false, false, false, "LastNameFour", "passwordFour", null, "UsernameFour" },
                    { 5, "FirstnameFive@Lastname.com", "FirstNameFive", false, false, false, "LastNameFive", "passwordFive", null, "UsernameFive" }
                });

            migrationBuilder.InsertData(
                table: "Threads",
                columns: new[] { "Id", "AuthorId", "Content", "CreationDate", "Dislikes", "IsDeleted", "Likes", "ModificationDate", "Title" },
                values: new object[] { 1, 2, "Hey guys, check out this cool new forum I found!", new DateTime(2023, 6, 28, 16, 27, 2, 404, DateTimeKind.Local).AddTicks(9000), 0, false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "First lol" });

            migrationBuilder.InsertData(
                table: "Threads",
                columns: new[] { "Id", "AuthorId", "Content", "CreationDate", "Dislikes", "IsDeleted", "Likes", "ModificationDate", "Title" },
                values: new object[] { 2, 1, "This is not a forum for the faint of heart. If you need something to get started with, see the pinned threads for some basic resources. We hope you enjoy your venture into the spooks, the creeps and the unknown.", new DateTime(2023, 6, 28, 16, 28, 2, 404, DateTimeKind.Local).AddTicks(9054), 0, false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Welcome to Paranormality." });

            migrationBuilder.InsertData(
                table: "Replies",
                columns: new[] { "Id", "AuthorId", "Content", "CreationDate", "Dislikes", "IsDeleted", "Likes", "ModificationDate", "ThreadId" },
                values: new object[,]
                {
                    { 1, 1, "Some of these lists are still a work in progress, as of this writing.\n\nFilm Recommendations\n\nGame Recommendations\n\nRadio Shows/Podcasts\n\nWebsites of Interest\n\nWikipedia Articles\n\nYouTube Videos & Channels", new DateTime(2023, 6, 28, 16, 29, 2, 406, DateTimeKind.Local).AddTicks(8595), 0, false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 2, 1, "Please note the following:\n\n• This forum desires high quality discussion. High quality posts will be praised.\n\n Low quality posts e.g. \"Is this paranormal?\" or \"I am [insert paranormal entity here] ask me anything,\" etc. will be removed.\n\n• Conspiracy theories are welcome, but please refrain from overly political discussions.• For everything else, refer to global and thread-specific rules.", new DateTime(2023, 6, 28, 16, 30, 2, 406, DateTimeKind.Local).AddTicks(8654), 0, false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.InsertData(
                table: "ThreadTags",
                columns: new[] { "TagId", "ThreadId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Replies_AuthorId",
                table: "Replies",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_ThreadId",
                table: "Replies",
                column: "ThreadId");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyVote_ReplyId",
                table: "ReplyVote",
                column: "ReplyId");

            migrationBuilder.CreateIndex(
                name: "IX_Threads_AuthorId",
                table: "Threads",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ThreadTags_ThreadId",
                table: "ThreadTags",
                column: "ThreadId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReplyVote");

            migrationBuilder.DropTable(
                name: "ThreadTags");

            migrationBuilder.DropTable(
                name: "Replies");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Threads");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
