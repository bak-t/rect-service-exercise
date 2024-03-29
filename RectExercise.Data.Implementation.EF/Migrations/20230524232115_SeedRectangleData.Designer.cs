﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RectExercise.Data.Implementation.EF.Database;

#nullable disable

namespace RectExercise.Data.Implementation.EF.Migrations
{
    [DbContext(typeof(RectDbContext))]
    [Migration("20230524232115_SeedRectangleData")]
    partial class SeedRectangleData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RectExercise.Data.Contract.Models.Rectangle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Bottom")
                        .HasColumnType("int");

                    b.Property<int>("Left")
                        .HasColumnType("int");

                    b.Property<int>("Right")
                        .HasColumnType("int");

                    b.Property<int>("Top")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Rectangles");
                });
#pragma warning restore 612, 618
        }
    }
}
