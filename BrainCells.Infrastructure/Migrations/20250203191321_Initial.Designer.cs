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
    [Migration("20250203191321_Initial")]
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
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Picture")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("BrainCells.Domain.Entities.Accounts.ForgotPassword", b =>
                {
                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OnetimePassword")
                        .IsRequired()
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
                        .IsRequired()
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

            modelBuilder.Entity("BrainCells.Domain.Entities.Accounts.Account", b =>
                {
                    b.HasOne("BrainCells.Domain.Entities.Accounts.Role", "Role")
                        .WithMany("Accounts")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
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

            modelBuilder.Entity("BrainCells.Domain.Entities.Accounts.Account", b =>
                {
                    b.Navigation("ForgotPassword")
                        .IsRequired();
                });

            modelBuilder.Entity("BrainCells.Domain.Entities.Accounts.Role", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
