﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TemplateDotNet.Migrations;

#nullable disable

namespace TemplateDotNet.Migrations.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    partial class ApiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TemplateDotNet.Domains.Line", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasComment("Identifiant de la ligne");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("now()")
                        .HasComment("Date de création de la ligne");

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("text")
                        .HasColumnName("error_message")
                        .HasComment("Message d'erreur");

                    b.Property<bool?>("IsError")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasColumnName("is_error")
                        .HasDefaultValueSql("false")
                        .HasComment("Si c'est une erreur");

                    b.Property<int>("Number")
                        .HasColumnType("integer")
                        .HasColumnName("number")
                        .HasComment("Numéro de la ligne");

                    b.Property<Guid>("TestId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("now()")
                        .HasComment("Date d'update de la ligne");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("line", "public");
                });

            modelBuilder.Entity("TemplateDotNet.Domains.Test", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasComment("Identifiant du test");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("now()")
                        .HasComment("Date de création du test");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name")
                        .HasComment("Nom du test");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("now()")
                        .HasComment("Date d'update du test");

                    b.HasKey("Id");

                    b.ToTable("test", "public");
                });

            modelBuilder.Entity("TemplateDotNet.Domains.Line", b =>
                {
                    b.HasOne("TemplateDotNet.Domains.Test", "Test")
                        .WithMany("Lines")
                        .HasForeignKey("TestId")
                        .IsRequired()
                        .HasConstraintName("line_fk_test");

                    b.Navigation("Test");
                });

            modelBuilder.Entity("TemplateDotNet.Domains.Test", b =>
                {
                    b.Navigation("Lines");
                });
#pragma warning restore 612, 618
        }
    }
}
