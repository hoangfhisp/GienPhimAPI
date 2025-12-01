using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GienPhim_Services.InteractionService.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class chat1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatGroups",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhimId = table.Column<int>(type: "int", nullable: false),
                    PhimName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllowChat = table.Column<bool>(type: "bit", nullable: false),
                    AllowInvite = table.Column<bool>(type: "bit", nullable: false),
                    AllowJoinByLink = table.Column<bool>(type: "bit", nullable: false),
                    IsPlaying = table.Column<bool>(type: "bit", nullable: false),
                    CurrentPlaybackPosition = table.Column<TimeSpan>(type: "time", nullable: false),
                    LastPlaybackUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShowStartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShowEndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AutoDeleteAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    InviteCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InviteCodeExpireAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatGroups", x => x.GroupId);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CommentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhimId = table.Column<int>(type: "int", nullable: false),
                    ParentCommentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BinhLuan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThoiGianTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comment_Comment_ParentCommentId",
                        column: x => x.ParentCommentId,
                        principalTable: "Comment",
                        principalColumn: "CommentId");
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    MessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsSystemMessage = table.Column<bool>(type: "bit", nullable: false),
                    LoaiTinNhan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_ChatMessages_ChatGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "ChatGroups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupMembers",
                columns: table => new
                {
                    GroupMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsOwner = table.Column<bool>(type: "bit", nullable: false),
                    CanChat = table.Column<bool>(type: "bit", nullable: false),
                    CanControlFilm = table.Column<bool>(type: "bit", nullable: false),
                    IsMuted = table.Column<bool>(type: "bit", nullable: false),
                    JoinedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMembers", x => x.GroupMemberId);
                    table.ForeignKey(
                        name: "FK_GroupMembers_ChatGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "ChatGroups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JoinRequests",
                columns: table => new
                {
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JoinRequests", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_JoinRequests_ChatGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "ChatGroups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    FeedbackId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhimId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LuotThichP = table.Column<bool>(type: "bit", nullable: false),
                    LuotDislikeP = table.Column<bool>(type: "bit", nullable: false),
                    ThoiGianTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.FeedbackId);
                    table.ForeignKey(
                        name: "FK_Feedback_Comment_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comment",
                        principalColumn: "CommentId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_GroupId",
                table: "ChatMessages",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ParentCommentId",
                table: "Comment",
                column: "ParentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_CommentId",
                table: "Feedback",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_GroupId_UserId",
                table: "GroupMembers",
                columns: new[] { "GroupId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JoinRequests_GroupId_RequestUserId",
                table: "JoinRequests",
                columns: new[] { "GroupId", "RequestUserId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "GroupMembers");

            migrationBuilder.DropTable(
                name: "JoinRequests");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "ChatGroups");
        }
    }
}
