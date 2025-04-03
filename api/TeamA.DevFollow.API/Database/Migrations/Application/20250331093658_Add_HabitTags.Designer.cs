﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TeamA.DevFollow.API.Database.Contexts;

#nullable disable

namespace TeamA.DevFollow.API.Database.Migrations.Application
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250331093658_Add_HabitTags")]
    partial class Add_HabitTags
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dev_follow")
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TeamA.DevFollow.API.Entities.Habit", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at_utc");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("description");

                    b.Property<DateOnly?>("EndDate")
                        .HasColumnType("date")
                        .HasColumnName("end_date");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("boolean")
                        .HasColumnName("is_archived");

                    b.Property<DateTime?>("LastCompletedAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_completed_at_utc");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.Property<DateTime?>("UpdatedAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at_utc");

                    b.HasKey("Id")
                        .HasName("pk_habits");

                    b.ToTable("habits", "dev_follow");
                });

            modelBuilder.Entity("TeamA.DevFollow.API.Entities.HabitTag", b =>
                {
                    b.Property<string>("HabitId")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("habit_id");

                    b.Property<string>("TagId")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("tag_id");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at_utc");

                    b.HasKey("HabitId", "TagId")
                        .HasName("pk_habit_tags");

                    b.HasIndex("TagId")
                        .HasDatabaseName("ix_habit_tags_tag_id");

                    b.ToTable("habit_tags", "dev_follow");
                });

            modelBuilder.Entity("TeamA.DevFollow.API.Entities.Tag", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at_utc");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.Property<DateTime?>("UpdatedAtUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at_utc");

                    b.HasKey("Id")
                        .HasName("pk_tags");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_tags_name");

                    b.ToTable("tags", "dev_follow");
                });

            modelBuilder.Entity("TeamA.DevFollow.API.Entities.Habit", b =>
                {
                    b.OwnsOne("TeamA.DevFollow.API.Entities.Frequency", "Frequency", b1 =>
                        {
                            b1.Property<string>("HabitId")
                                .HasColumnType("character varying(500)")
                                .HasColumnName("id");

                            b1.Property<int>("TimesPerPeriod")
                                .HasColumnType("integer")
                                .HasColumnName("frequency_times_per_period");

                            b1.Property<int>("Type")
                                .HasColumnType("integer")
                                .HasColumnName("frequency_type");

                            b1.HasKey("HabitId");

                            b1.ToTable("habits", "dev_follow");

                            b1.WithOwner()
                                .HasForeignKey("HabitId")
                                .HasConstraintName("fk_habits_habits_id");
                        });

                    b.OwnsOne("TeamA.DevFollow.API.Entities.Milestone", "Milestone", b1 =>
                        {
                            b1.Property<string>("HabitId")
                                .HasColumnType("character varying(500)")
                                .HasColumnName("id");

                            b1.Property<int>("Current")
                                .HasColumnType("integer")
                                .HasColumnName("milestone_current");

                            b1.Property<int>("Target")
                                .HasColumnType("integer")
                                .HasColumnName("milestone_target");

                            b1.HasKey("HabitId");

                            b1.ToTable("habits", "dev_follow");

                            b1.WithOwner()
                                .HasForeignKey("HabitId")
                                .HasConstraintName("fk_habits_habits_id");
                        });

                    b.OwnsOne("TeamA.DevFollow.API.Entities.Target", "Target", b1 =>
                        {
                            b1.Property<string>("HabitId")
                                .HasColumnType("character varying(500)")
                                .HasColumnName("id");

                            b1.Property<string>("Unit")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("target_unit");

                            b1.Property<int>("Value")
                                .HasColumnType("integer")
                                .HasColumnName("target_value");

                            b1.HasKey("HabitId");

                            b1.ToTable("habits", "dev_follow");

                            b1.WithOwner()
                                .HasForeignKey("HabitId")
                                .HasConstraintName("fk_habits_habits_id");
                        });

                    b.Navigation("Frequency")
                        .IsRequired();

                    b.Navigation("Milestone");

                    b.Navigation("Target")
                        .IsRequired();
                });

            modelBuilder.Entity("TeamA.DevFollow.API.Entities.HabitTag", b =>
                {
                    b.HasOne("TeamA.DevFollow.API.Entities.Habit", null)
                        .WithMany("HabitTags")
                        .HasForeignKey("HabitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_habit_tags_habits_habit_id");

                    b.HasOne("TeamA.DevFollow.API.Entities.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_habit_tags_tags_tag_id");
                });

            modelBuilder.Entity("TeamA.DevFollow.API.Entities.Habit", b =>
                {
                    b.Navigation("HabitTags");
                });
#pragma warning restore 612, 618
        }
    }
}
