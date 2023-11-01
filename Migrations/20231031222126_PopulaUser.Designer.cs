﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RentAPI.Context;

#nullable disable

namespace RentAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231031222126_PopulaUser")]
    partial class PopulaUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("RentBikeApi.Models.Bike", b =>
                {
                    b.Property<int>("BikeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<int>("TypeBike")
                        .HasColumnType("int");

                    b.HasKey("BikeId");

                    b.ToTable("Bike");
                });

            modelBuilder.Entity("RentBikeApi.Models.Image", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("BikeId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.HasKey("ImageId");

                    b.HasIndex("BikeId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("RentBikeApi.Models.Rent", b =>
                {
                    b.Property<int>("RentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BikeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DateStart")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("RentId");

                    b.HasIndex("BikeId");

                    b.HasIndex("UserId");

                    b.ToTable("Rent");
                });

            modelBuilder.Entity("RentBikeApi.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.HasKey("UserId");

                    b.HasIndex("UserEmail")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("RentBikeApi.Models.Image", b =>
                {
                    b.HasOne("RentBikeApi.Models.Bike", "Bike")
                        .WithMany("Images")
                        .HasForeignKey("BikeId");

                    b.Navigation("Bike");
                });

            modelBuilder.Entity("RentBikeApi.Models.Rent", b =>
                {
                    b.HasOne("RentBikeApi.Models.Bike", "Bike")
                        .WithMany()
                        .HasForeignKey("BikeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RentBikeApi.Models.User", "User")
                        .WithMany("Rent")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bike");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RentBikeApi.Models.Bike", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("RentBikeApi.Models.User", b =>
                {
                    b.Navigation("Rent");
                });
#pragma warning restore 612, 618
        }
    }
}
