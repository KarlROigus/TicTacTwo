﻿// <auto-generated />
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241108141439_RenameOldTableToNewTable")]
    partial class RenameOldTableToNewTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("Domain.Config", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConfigJsonString")
                        .IsRequired()
                        .HasMaxLength(10240)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Configs");
                });

            modelBuilder.Entity("Domain.GameStateJson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ConfigId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("GameStateJsonString")
                        .IsRequired()
                        .HasMaxLength(10240)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ConfigId");

                    b.ToTable("GameStates");
                });

            modelBuilder.Entity("Domain.GameStateJson", b =>
                {
                    b.HasOne("Domain.Config", "Config")
                        .WithMany("GameStateJsons")
                        .HasForeignKey("ConfigId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Config");
                });

            modelBuilder.Entity("Domain.Config", b =>
                {
                    b.Navigation("GameStateJsons");
                });
#pragma warning restore 612, 618
        }
    }
}
