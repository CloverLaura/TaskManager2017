﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using TaskManager2017.Models;

namespace TaskManager2017.Migrations
{
    [DbContext(typeof(TaskManager2017Context))]
    [Migration("20180515190238_Project")]
    partial class Project
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TaskManager.Models.Project", b =>
                {
                    b.Property<int>("ProjectID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int>("TaskCount");

                    b.Property<string>("TeamP");

                    b.Property<int?>("UserID");

                    b.HasKey("ProjectID");

                    b.HasIndex("UserID");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("TaskManager.Models.Task", b =>
                {
                    b.Property<int>("TaskID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Completed");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Description");

                    b.Property<bool>("IsTaken");

                    b.Property<string>("Name");

                    b.Property<string>("Project");

                    b.Property<int?>("ProjectID");

                    b.Property<string>("TakenBy");

                    b.Property<int?>("UserID");

                    b.HasKey("TaskID");

                    b.HasIndex("ProjectID");

                    b.HasIndex("UserID");

                    b.ToTable("Task");
                });

            modelBuilder.Entity("TaskManager.Models.Team", b =>
                {
                    b.Property<int>("TeamID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int?>("UserID");

                    b.HasKey("TeamID");

                    b.HasIndex("UserID");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("TaskManager.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<bool>("LoggedOn");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("UserID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("TaskManager2017.Models.UserTeam", b =>
                {
                    b.Property<int>("UserTeamID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Team");

                    b.Property<string>("User");

                    b.HasKey("UserTeamID");

                    b.ToTable("UserTeam");
                });

            modelBuilder.Entity("TaskManager.Models.Project", b =>
                {
                    b.HasOne("TaskManager.Models.User")
                        .WithMany("UserProjects")
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("TaskManager.Models.Task", b =>
                {
                    b.HasOne("TaskManager.Models.Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectID");

                    b.HasOne("TaskManager.Models.User")
                        .WithMany("UserTasks")
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("TaskManager.Models.Team", b =>
                {
                    b.HasOne("TaskManager.Models.User")
                        .WithMany("UserTeams")
                        .HasForeignKey("UserID");
                });
#pragma warning restore 612, 618
        }
    }
}
