﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamA.DevFollow.API.Database.Migrations.Application;

/// <inheritdoc />
public partial class Add_Habit : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "dev_follow");

        migrationBuilder.CreateTable(
            name: "habits",
            schema: "dev_follow",
            columns: table => new
            {
                id = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                type = table.Column<int>(type: "integer", nullable: false),
                frequency_type = table.Column<int>(type: "integer", nullable: false),
                frequency_times_per_period = table.Column<int>(type: "integer", nullable: false),
                target_value = table.Column<int>(type: "integer", nullable: false),
                target_unit = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                status = table.Column<int>(type: "integer", nullable: false),
                is_archived = table.Column<bool>(type: "boolean", nullable: false),
                end_date = table.Column<DateOnly>(type: "date", nullable: true),
                milestone_target = table.Column<int>(type: "integer", nullable: true),
                milestone_current = table.Column<int>(type: "integer", nullable: true),
                updated_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                last_completed_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_habits", x => x.id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "habits",
            schema: "dev_follow");
    }
}
