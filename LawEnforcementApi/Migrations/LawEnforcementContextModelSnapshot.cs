﻿// <auto-generated />
using System;
using LawEnforcementApi.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LawEnforcementApi.Migrations
{
    [DbContext(typeof(LawEnforcementContext))]
    partial class LawEnforcementContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("LawEnforcementApi.Entities.CrimeEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CrimeEventId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OfficerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OfficerId");

                    b.ToTable("CrimeEvents");
                });

            modelBuilder.Entity("LawEnforcementApi.Entities.Officer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CallSign")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<Guid>("OfficerRankId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CallSign")
                        .IsUnique();

                    b.HasIndex("OfficerRankId");

                    b.ToTable("Officers");
                });

            modelBuilder.Entity("LawEnforcementApi.Entities.Rank", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Ranks");
                });

            modelBuilder.Entity("LawEnforcementApi.Entities.CrimeEvent", b =>
                {
                    b.HasOne("LawEnforcementApi.Entities.Officer", null)
                        .WithMany("CrimeEvents")
                        .HasForeignKey("OfficerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LawEnforcementApi.Entities.Officer", b =>
                {
                    b.HasOne("LawEnforcementApi.Entities.Rank", "OfficerRank")
                        .WithMany("Officers")
                        .HasForeignKey("OfficerRankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OfficerRank");
                });

            modelBuilder.Entity("LawEnforcementApi.Entities.Officer", b =>
                {
                    b.Navigation("CrimeEvents");
                });

            modelBuilder.Entity("LawEnforcementApi.Entities.Rank", b =>
                {
                    b.Navigation("Officers");
                });
#pragma warning restore 612, 618
        }
    }
}
