﻿// <auto-generated />
using System;
using DataAccess.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.DbContext.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230907060141_4")]
    partial class _4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("DataAccess.Entities.Communication", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ContactId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id")
                        .HasName("PK_Communications");

                    b.HasIndex("CompanyId");

                    b.HasIndex("ContactId");

                    b.ToTable("Communications", null, t =>
                        {
                            t.HasComment("Средства коммуникации");
                        });
                });

            modelBuilder.Entity("DataAccess.Entities.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Comment")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("DecisionMakerId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Level")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ModificationTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id")
                        .HasName("PK_Companies");

                    b.HasIndex("DecisionMakerId")
                        .IsUnique();

                    b.ToTable("Companies", null, t =>
                        {
                            t.HasComment("Компании");
                        });

                    b.HasData(
                        new
                        {
                            Id = new Guid("282fceba-385f-44b8-a633-cebcda811753"),
                            Comment = "Добавлено с помощью миграции",
                            CreationTime = new DateTime(2023, 9, 7, 9, 1, 40, 911, DateTimeKind.Local).AddTicks(9110),
                            Level = 3,
                            ModificationTime = new DateTime(2023, 9, 7, 9, 1, 40, 911, DateTimeKind.Local).AddTicks(9122),
                            Name = "Литобзор"
                        });
                });

            modelBuilder.Entity("DataAccess.Entities.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("TEXT")
                        .HasComputedColumnSql("Surname || ' ' || Name || IIF(MiddleName IS NULL, '', ' ' || MiddleName)", true);

                    b.Property<bool>("IsDecisionMaker")
                        .HasColumnType("INTEGER");

                    b.Property<string>("JobTitle")
                        .HasColumnType("TEXT");

                    b.Property<string>("MiddleName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ModificationTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id")
                        .HasName("PK_Contacts");

                    b.HasIndex("CompanyId");

                    b.ToTable("Contacts", null, t =>
                        {
                            t.HasComment("Сотрудники");
                        });

                    b.HasData(
                        new
                        {
                            Id = new Guid("d239a4c2-3d24-4c6d-b142-82d212d3a34f"),
                            CompanyId = new Guid("282fceba-385f-44b8-a633-cebcda811753"),
                            CreationTime = new DateTime(2023, 9, 7, 9, 1, 40, 915, DateTimeKind.Local).AddTicks(5463),
                            FullName = "Иванов Иван Иванович",
                            IsDecisionMaker = false,
                            JobTitle = "Менеджер",
                            MiddleName = "Иванович",
                            ModificationTime = new DateTime(2023, 9, 7, 9, 1, 40, 915, DateTimeKind.Local).AddTicks(5467),
                            Name = "Иван",
                            Surname = "Иванов"
                        });
                });

            modelBuilder.Entity("DataAccess.Entities.Communication", b =>
                {
                    b.HasOne("DataAccess.Entities.Company", "Company")
                        .WithMany("Communications")
                        .HasForeignKey("CompanyId")
                        .HasConstraintName("FK_Communications_CompanyId");

                    b.HasOne("DataAccess.Entities.Contact", "Contact")
                        .WithMany("Communications")
                        .HasForeignKey("ContactId")
                        .HasConstraintName("FK_Communications_ContactId");

                    b.Navigation("Company");

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("DataAccess.Entities.Company", b =>
                {
                    b.HasOne("DataAccess.Entities.Contact", "DecisionMaker")
                        .WithOne()
                        .HasForeignKey("DataAccess.Entities.Company", "DecisionMakerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("FK_Companies_DecisionMakerId");

                    b.Navigation("DecisionMaker");
                });

            modelBuilder.Entity("DataAccess.Entities.Contact", b =>
                {
                    b.HasOne("DataAccess.Entities.Company", "Company")
                        .WithMany("Contacts")
                        .HasForeignKey("CompanyId")
                        .HasConstraintName("FK_Contacts_CompanyId");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("DataAccess.Entities.Company", b =>
                {
                    b.Navigation("Communications");

                    b.Navigation("Contacts");
                });

            modelBuilder.Entity("DataAccess.Entities.Contact", b =>
                {
                    b.Navigation("Communications");
                });
#pragma warning restore 612, 618
        }
    }
}
