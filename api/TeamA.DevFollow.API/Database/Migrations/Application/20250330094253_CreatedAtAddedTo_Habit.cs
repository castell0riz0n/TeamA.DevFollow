using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamA.DevFollow.API.Database.Migrations.Application;

/// <inheritdoc />
public partial class CreatedAtAddedTo_Habit : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateTime>(
            name: "created_at_utc",
            schema: "dev_follow",
            table: "habits",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "created_at_utc",
            schema: "dev_follow",
            table: "habits");
    }
}
