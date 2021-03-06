// <auto-generated />
using System;
using DataMigration.DataClass;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataMigration.DataClass.Migrations
{
    [DbContext(typeof(DataMigrationDbContext))]
    [Migration("20220213092357_addMigrationTable")]
    partial class addMigrationTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Data.Domain.DestinationTable", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("SourceTable")
                        .HasColumnType("int");

                    b.Property<int>("Sum")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("SourceTable");

                    b.ToTable("DestinationTable");
                });

            modelBuilder.Entity("DataMigration.Domain.SourceTable", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("FirstNumber")
                        .HasColumnType("int");

                    b.Property<int>("SecondNumber")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("SourceTable");
                });

            modelBuilder.Entity("DataMigration.DomainClass.DataMigrationTable", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("End")
                        .HasColumnType("int");

                    b.Property<int>("Start")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("DataMigrationTable");
                });

            modelBuilder.Entity("Data.Domain.DestinationTable", b =>
                {
                    b.HasOne("DataMigration.Domain.SourceTable", "SourceTableId")
                        .WithMany()
                        .HasForeignKey("SourceTable");

                    b.Navigation("SourceTableId");
                });
#pragma warning restore 612, 618
        }
    }
}
