using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamA.DevFollow.API.Database.Migrations.Application;

public partial class Add_UserId_Reference : Migration
{
    private static readonly string[] TagsUserIdNameColumns = { "user_id", "name" };
    private static readonly string[] HabitsUserIdColumns = { "user_id" };

    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(
            """
            DELETE FROM dev_follow.habit_tags;
            DELETE FROM dev_follow.habits;
            DELETE FROM dev_follow.tags;
            """
        );

        migrationBuilder.DropIndex(
            name: "ix_tags_name",
            schema: "dev_follow",
            table: "tags");

        migrationBuilder.AddColumn<string>(
            name: "user_id",
            schema: "dev_follow",
            table: "tags",
            type: "character varying(500)",
            maxLength: 500,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "user_id",
            schema: "dev_follow",
            table: "habits",
            type: "character varying(500)",
            maxLength: 500,
            nullable: false,
            defaultValue: "");

        migrationBuilder.CreateIndex(
            name: "ix_tags_user_id_name",
            schema: "dev_follow",
            table: "tags",
            columns: TagsUserIdNameColumns,
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_habits_user_id",
            schema: "dev_follow",
            table: "habits",
            columns: HabitsUserIdColumns);

        migrationBuilder.AddForeignKey(
            name: "fk_habits_users_user_id",
            schema: "dev_follow",
            table: "habits",
            column: "user_id",
            principalSchema: "dev_follow",
            principalTable: "users",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_tags_users_user_id",
            schema: "dev_follow",
            table: "tags",
            column: "user_id",
            principalSchema: "dev_follow",
            principalTable: "users",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_habits_users_user_id",
            schema: "dev_follow",
            table: "habits");

        migrationBuilder.DropForeignKey(
            name: "fk_tags_users_user_id",
            schema: "dev_follow",
            table: "tags");

        migrationBuilder.DropIndex(
            name: "ix_tags_user_id_name",
            schema: "dev_follow",
            table: "tags");

        migrationBuilder.DropIndex(
            name: "ix_habits_user_id",
            schema: "dev_follow",
            table: "habits");

        migrationBuilder.DropColumn(
            name: "user_id",
            schema: "dev_follow",
            table: "tags");

        migrationBuilder.DropColumn(
            name: "user_id",
            schema: "dev_follow",
            table: "habits");

        migrationBuilder.CreateIndex(
            name: "ix_tags_name",
            schema: "dev_follow",
            table: "tags",
            column: "name",
            unique: true);
    }
}
