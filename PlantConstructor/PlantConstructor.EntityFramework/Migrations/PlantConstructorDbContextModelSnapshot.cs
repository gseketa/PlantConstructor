﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlantConstructor.EntityFramework;

namespace PlantConstructor.EntityFramework.Migrations
{
    [DbContext(typeof(PlantConstructorDbContext))]
    partial class PlantConstructorDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PlantConstructor.WPF.Model.Project", b =>
                {
                    b.Property<int>("ProjectID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProjectID");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("PlantConstructor.WPF.Model.Site", b =>
                {
                    b.Property<int>("SiteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProjectsProjectID")
                        .HasColumnType("int");

                    b.HasKey("SiteID");

                    b.HasIndex("ProjectsProjectID");

                    b.ToTable("Sites");
                });

            modelBuilder.Entity("PlantConstructor.WPF.Model.Site", b =>
                {
                    b.HasOne("PlantConstructor.WPF.Model.Project", "Projects")
                        .WithMany()
                        .HasForeignKey("ProjectsProjectID");
                });
#pragma warning restore 612, 618
        }
    }
}
