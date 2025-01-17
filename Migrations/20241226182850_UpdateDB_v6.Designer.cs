﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Web_DonNghiPhep.Data;

#nullable disable

namespace Web_DonNghiPhep.Migrations
{
    [DbContext(typeof(MyDBContext))]
    [Migration("20241226182850_UpdateDB_v6")]
    partial class UpdateDB_v6
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Web_DonNghiPhep.Models.Department", b =>
                {
                    b.Property<string>("Department_id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ManagerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Department_id");

                    b.HasIndex("ParentId");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("Web_DonNghiPhep.Models.DepartmentEmployee", b =>
                {
                    b.Property<string>("DepartmentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EmployeeId")
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("DepartmentId", "EmployeeId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("DepartmentEmployee");
                });

            modelBuilder.Entity("Web_DonNghiPhep.Models.Employee", b =>
                {
                    b.Property<string>("Employee_ID")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Department_id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title_id")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Employee_ID");

                    b.HasIndex("Department_id");

                    b.HasIndex("Title_id");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("Web_DonNghiPhep.Models.LeaveBalance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Employee_id")
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("RemainingDays")
                        .HasColumnType("int");

                    b.Property<int>("TotalDays")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UsedDays")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Employee_id");

                    b.ToTable("LeaveBalance");
                });

            modelBuilder.Entity("Web_DonNghiPhep.Models.LeaveRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ApprovedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Employee_id")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Employee_id");

                    b.ToTable("LeaveRequest");
                });

            modelBuilder.Entity("Web_DonNghiPhep.Models.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("ActionTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Log");
                });

            modelBuilder.Entity("Web_DonNghiPhep.Models.Roles", b =>
                {
                    b.Property<int>("Role_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Role_ID"));

                    b.Property<string>("Role_Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Role_ID");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Web_DonNghiPhep.Models.Titles", b =>
                {
                    b.Property<string>("Title_id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Title_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Title_id");

                    b.ToTable("Title");
                });

            modelBuilder.Entity("Web_DonNghiPhep.Models.User", b =>
                {
                    b.Property<Guid>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Employee_ID")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("updated_at")
                        .HasColumnType("datetime2");

                    b.HasKey("UserID");

                    b.HasIndex("Employee_ID")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("Web_DonNghiPhep.Models.UserRole", b =>
                {
                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.HasKey("UserID", "RoleID");

                    b.HasIndex("RoleID");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("Web_DonNghiPhep.Models.Department", b =>
                {
                    b.HasOne("Web_DonNghiPhep.Models.Department", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Web_DonNghiPhep.Models.DepartmentEmployee", b =>
                {
                    b.HasOne("Web_DonNghiPhep.Models.Department", "Department")
                        .WithMany("DepartmentEmployees")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Web_DonNghiPhep.Models.Employee", "Employee")
                        .WithMany("DepartmentEmployees")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Web_DonNghiPhep.Models.Employee", b =>
                {
                    b.HasOne("Web_DonNghiPhep.Models.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("Department_id");

                    b.HasOne("Web_DonNghiPhep.Models.Titles", "Title")
                        .WithMany("Employees")
                        .HasForeignKey("Title_id")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Department");

                    b.Navigation("Title");
                });

            modelBuilder.Entity("Web_DonNghiPhep.Models.LeaveBalance", b =>
                {
                    b.HasOne("Web_DonNghiPhep.Models.Employee", "Employee")
                        .WithMany("LeaveBalances")
                        .HasForeignKey("Employee_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Web_DonNghiPhep.Models.LeaveRequest", b =>
                {
                    b.HasOne("Web_DonNghiPhep.Models.Employee", "Employee")
                        .WithMany("LeaveRequests")
                        .HasForeignKey("Employee_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Web_DonNghiPhep.Models.Log", b =>
                {
                    b.HasOne("Web_DonNghiPhep.Models.User", "User")
                        .WithMany("Logs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("User");
                });

            modelBuilder.Entity("Web_DonNghiPhep.Models.User", b =>
                {
                    b.HasOne("Web_DonNghiPhep.Models.Employee", "EmployeeUs")
                        .WithOne("User")
                        .HasForeignKey("Web_DonNghiPhep.Models.User", "Employee_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EmployeeUs");
                });

            modelBuilder.Entity("Web_DonNghiPhep.Models.UserRole", b =>
                {
                    b.HasOne("Web_DonNghiPhep.Models.Roles", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Web_DonNghiPhep.Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Web_DonNghiPhep.Models.Department", b =>
                {
                    b.Navigation("DepartmentEmployees");

                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Web_DonNghiPhep.Models.Employee", b =>
                {
                    b.Navigation("DepartmentEmployees");

                    b.Navigation("LeaveBalances");

                    b.Navigation("LeaveRequests");

                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("Web_DonNghiPhep.Models.Roles", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Web_DonNghiPhep.Models.Titles", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Web_DonNghiPhep.Models.User", b =>
                {
                    b.Navigation("Logs");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
