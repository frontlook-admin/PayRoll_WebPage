﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using payroll_app.Data;

namespace payroll_app.Migrations
{
    [DbContext(typeof(payroll_app_context))]
    partial class payroll_app_contextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("payroll_app.Models.repository.AttendanceRegister", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Attendance");

                    b.Property<DateTime?>("AttendanceTime")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnName("AttendanceTime");

                    b.Property<int>("EmployeeId");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("AttendanceRegister");
                });

            modelBuilder.Entity("payroll_app.Models.repository.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<int>("ArrangeOrder")
                        .HasColumnName("ArrangeOrder")
                        .HasMaxLength(11);

                    b.Property<string>("DepartmentCode")
                        .IsRequired()
                        .HasColumnName("DepartmentCode")
                        .HasMaxLength(30);

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasColumnName("DepartmentName")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.HasAlternateKey("DepartmentCode");

                    b.HasAlternateKey("DepartmentName");

                    b.HasAlternateKey("DepartmentCode", "DepartmentName", "Id");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("payroll_app.Models.repository.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("Address1")
                        .IsRequired()
                        .HasColumnName("Address1")
                        .HasMaxLength(400);

                    b.Property<string>("Address2")
                        .HasColumnName("Address2")
                        .HasMaxLength(400);

                    b.Property<string>("Address3")
                        .HasColumnName("Address3")
                        .HasMaxLength(400);

                    b.Property<string>("AreaStdCode")
                        .HasColumnName("AreaStdCode")
                        .HasMaxLength(6);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnName("City")
                        .HasMaxLength(100);

                    b.Property<string>("District")
                        .HasColumnName("District")
                        .HasMaxLength(100);

                    b.Property<string>("EmailId")
                        .IsRequired()
                        .HasColumnName("EmailId")
                        .HasMaxLength(400);

                    b.Property<byte[]>("EmployeePhoto")
                        .HasColumnName("EmployeePhoto");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnName("FirstName")
                        .HasMaxLength(200);

                    b.Property<string>("FullName")
                        .HasColumnName("FullName")
                        .HasMaxLength(700);

                    b.Property<string>("Gender")
                        .HasColumnName("Gender")
                        .HasMaxLength(20);

                    b.Property<string>("LastName")
                        .HasColumnName("LastName")
                        .HasMaxLength(200);

                    b.Property<string>("MiddleName")
                        .HasColumnName("MiddleName")
                        .HasMaxLength(200);

                    b.Property<string>("PhoneNo")
                        .HasMaxLength(8);

                    b.Property<string>("Pin")
                        .IsRequired()
                        .HasColumnName("Pin")
                        .HasMaxLength(6);

                    b.Property<string>("PoliceStation")
                        .IsRequired()
                        .HasColumnName("PoliceStation")
                        .HasMaxLength(100);

                    b.Property<string>("PostOffice")
                        .IsRequired()
                        .HasColumnName("PostOffice")
                        .HasMaxLength(100);

                    b.Property<string>("PrimaryMobileNo")
                        .IsRequired()
                        .HasColumnName("PrimaryMobileNo")
                        .HasMaxLength(10);

                    b.Property<string>("SecondaryMobileNo")
                        .IsRequired()
                        .HasColumnName("SecondaryMobileNo")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.HasAlternateKey("EmailId");

                    b.HasAlternateKey("PrimaryMobileNo");

                    b.HasAlternateKey("SecondaryMobileNo");

                    b.HasAlternateKey("EmailId", "Id", "PrimaryMobileNo", "SecondaryMobileNo");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("payroll_app.Models.repository.Grade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<string>("ArrangeOrder")
                        .HasColumnName("ArrangeOrder")
                        .HasMaxLength(11);

                    b.Property<string>("GradeCode")
                        .IsRequired()
                        .HasColumnName("GradeCode")
                        .HasMaxLength(30);

                    b.Property<string>("GradeName")
                        .IsRequired()
                        .HasColumnName("GradeName")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.HasAlternateKey("GradeCode");

                    b.HasAlternateKey("GradeName");

                    b.HasAlternateKey("GradeCode", "GradeName", "Id");

                    b.ToTable("Grade");
                });

            modelBuilder.Entity("payroll_app.Models.repository.WorkerType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<string>("ArrangeOrder")
                        .HasColumnName("ArrangeOrder")
                        .HasMaxLength(11);

                    b.Property<string>("CategoryCode")
                        .IsRequired()
                        .HasColumnName("CategoryCode")
                        .HasMaxLength(30);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnName("CategoryName")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.HasAlternateKey("CategoryCode");

                    b.HasAlternateKey("CategoryName");

                    b.HasAlternateKey("CategoryCode", "CategoryName", "Id");

                    b.ToTable("WorkerType");
                });

            modelBuilder.Entity("payroll_app.Models.repository.AttendanceRegister", b =>
                {
                    b.HasOne("payroll_app.Models.repository.Employee", "Employees")
                        .WithMany("SalaryHeads")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}