﻿// <auto-generated />
using System;
using BrainCells.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BrainCells.Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20250308123733_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BrainCells.Domain.Entities.Accounts.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Picture")
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("RoleId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("BrainCells.Domain.Entities.Accounts.ForgotPassword", b =>
                {
                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OnetimePassword")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AccountId");

                    b.ToTable("ForgotPasswords");
                });

            modelBuilder.Entity("BrainCells.Domain.Entities.Accounts.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-fa00-add0-0000-000000000000"),
                            Name = "ADMIN"
                        },
                        new
                        {
                            Id = new Guid("00000000-fa00-acc0-0cca-000000000000"),
                            Name = "ACCOUNT"
                        });
                });

            modelBuilder.Entity("BrainCells.Domain.Entities.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("BrainCells.Domain.Entities.Todo.TodoList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Picture")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("TodoLists");
                });

            modelBuilder.Entity("BrainCells.Domain.Entities.Todo.TodoSubTask", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Completion")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TodoTaskId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TodoTaskId");

                    b.ToTable("TodoSubTasks");
                });

            modelBuilder.Entity("BrainCells.Domain.Entities.Todo.TodoTask", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Completion")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DueDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Icon")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PriorityLevel")
                        .HasColumnType("int");

                    b.Property<string>("Reminder")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SetDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TodoListId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TodoListId");

                    b.ToTable("TodoTasks");
                });

            modelBuilder.Entity("BrainCells.Domain.Entities.Accounts.Account", b =>
                {
                    b.HasOne("BrainCells.Domain.Entities.Accounts.Role", "Role")
                        .WithMany("Accounts")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("BrainCells.Domain.Entities.Accounts.ForgotPassword", b =>
                {
                    b.HasOne("BrainCells.Domain.Entities.Accounts.Account", "Account")
                        .WithOne("ForgotPassword")
                        .HasForeignKey("BrainCells.Domain.Entities.Accounts.ForgotPassword", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("BrainCells.Domain.Entities.Todo.TodoList", b =>
                {
                    b.HasOne("BrainCells.Domain.Entities.Accounts.Account", "Account")
                        .WithMany("TodoLists")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("BrainCells.Domain.Entities.Todo.TodoSubTask", b =>
                {
                    b.HasOne("BrainCells.Domain.Entities.Todo.TodoTask", "TodoTask")
                        .WithMany("SubTasks")
                        .HasForeignKey("TodoTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TodoTask");
                });

            modelBuilder.Entity("BrainCells.Domain.Entities.Todo.TodoTask", b =>
                {
                    b.HasOne("BrainCells.Domain.Entities.Todo.TodoList", "TodoList")
                        .WithMany("Tasks")
                        .HasForeignKey("TodoListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TodoList");
                });

            modelBuilder.Entity("BrainCells.Domain.Entities.Accounts.Account", b =>
                {
                    b.Navigation("ForgotPassword");

                    b.Navigation("TodoLists");
                });

            modelBuilder.Entity("BrainCells.Domain.Entities.Accounts.Role", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("BrainCells.Domain.Entities.Todo.TodoList", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("BrainCells.Domain.Entities.Todo.TodoTask", b =>
                {
                    b.Navigation("SubTasks");
                });
#pragma warning restore 612, 618
        }
    }
}
