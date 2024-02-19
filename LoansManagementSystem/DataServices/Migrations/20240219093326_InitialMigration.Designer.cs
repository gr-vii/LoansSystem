﻿// <auto-generated />
using System;
using LoansManagementSystem.DataServices.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LoansManagementSystem.Migrations
{
    [DbContext(typeof(Db))]
    [Migration("20240219093326_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LoansManagementSystem.Entities.Models.Administrator", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Blocked")
                        .HasColumnType("boolean");

                    b.Property<int>("FailedLoginCounter")
                        .HasColumnType("integer");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<DateTime>("InsDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<DateTime>("LupDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.Property<string>("Salt")
                        .HasColumnType("text");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Administrator");
                });

            modelBuilder.Entity("LoansManagementSystem.Entities.Models.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AccountNumber")
                        .HasColumnType("text");

                    b.Property<bool>("Blocked")
                        .HasColumnType("boolean");

                    b.Property<int>("FailedLoginCounter")
                        .HasColumnType("integer");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<DateTime>("InsDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<DateTime>("LupDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("Salt")
                        .HasColumnType("text");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("LoansManagementSystem.Entities.Models.LoanApplication", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<bool>("ApprovalStatus")
                        .HasColumnType("boolean");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<string>("EmploymentType")
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .HasColumnType("text");

                    b.Property<DateTime>("InsDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("LupDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("MonthIncome")
                        .HasColumnType("integer");

                    b.Property<string>("Purpose")
                        .HasColumnType("text");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.Property<int>("Term")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("LoanApplication");
                });

            modelBuilder.Entity("LoansManagementSystem.Entities.Models.LoanPayment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("InsDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("LoanId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LupDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("LoanId");

                    b.ToTable("LoanPayment");
                });

            modelBuilder.Entity("LoansManagementSystem.Entities.Models.LoanApplication", b =>
                {
                    b.HasOne("LoansManagementSystem.Entities.Models.Client", "Client")
                        .WithMany("Loans")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_Loans_Client");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("LoansManagementSystem.Entities.Models.LoanPayment", b =>
                {
                    b.HasOne("LoansManagementSystem.Entities.Models.LoanApplication", "Loan")
                        .WithMany("LoanPayment")
                        .HasForeignKey("LoanId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_Loans_LoansPayment");

                    b.Navigation("Loan");
                });

            modelBuilder.Entity("LoansManagementSystem.Entities.Models.Client", b =>
                {
                    b.Navigation("Loans");
                });

            modelBuilder.Entity("LoansManagementSystem.Entities.Models.LoanApplication", b =>
                {
                    b.Navigation("LoanPayment");
                });
#pragma warning restore 612, 618
        }
    }
}
