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
    [Migration("20230822093407_2")]
    partial class _2
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
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ContactId")
                        .IsRequired()
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
                            Id = new Guid("c4b89657-abc7-4675-b43d-0880ab421c1b"),
                            Comment = "Добавлено с помощью миграции",
                            CreationTime = new DateTime(2023, 8, 22, 12, 34, 7, 755, DateTimeKind.Local).AddTicks(894),
                            Level = 2,
                            ModificationTime = new DateTime(2023, 8, 22, 12, 34, 7, 755, DateTimeKind.Local).AddTicks(903),
                            Name = "Литобзор"
                        });
                });

            modelBuilder.Entity("DataAccess.Entities.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CompanyId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .IsRequired()
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
                });

            modelBuilder.Entity("DataAccess.Entities.Communication", b =>
                {
                    b.HasOne("DataAccess.Entities.Company", "Company")
                        .WithMany("Communications")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Communications_CompanyId");

                    b.HasOne("DataAccess.Entities.Contact", "Contact")
                        .WithMany("Communications")
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
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
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
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
