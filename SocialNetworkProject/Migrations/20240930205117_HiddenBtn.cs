﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialNetworkProject.Migrations
{
    /// <inheritdoc />
    public partial class HiddenBtn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHidden",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHidden",
                table: "Posts");
        }
    }
}
