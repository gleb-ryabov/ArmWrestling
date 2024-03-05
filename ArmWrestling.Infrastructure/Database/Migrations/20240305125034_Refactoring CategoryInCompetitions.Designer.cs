﻿// <auto-generated />
using System;
using ArmWrestling.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ArmWrestling.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240305125034_Refactoring CategoryInCompetitions")]
    partial class RefactoringCategoryInCompetitions
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("ArmWrestling.Domain.Database.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryGroupId")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("MaxAge")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("MaxWeight")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("MinAge")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CategoryGroupId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ArmWrestling.Domain.Database.CategoryGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CategoryGroups");
                });

            modelBuilder.Entity("ArmWrestling.Domain.Database.CategoryInCompetition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CompetitionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CompetitionId");

                    b.ToTable("CategoryInCompetitions");
                });

            modelBuilder.Entity("ArmWrestling.Domain.Database.Competition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte>("CountTable")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<char>("FirstArm")
                        .HasColumnType("TEXT");

                    b.Property<string>("TypeCompetition")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TypeJudging")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("WeightTolerance")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Competitions");
                });

            modelBuilder.Entity("ArmWrestling.Domain.Database.Duel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<char>("Arm")
                        .HasColumnType("TEXT");

                    b.Property<int>("CategoryInCompetitionId")
                        .HasColumnType("INTEGER");

                    b.Property<char?>("Group")
                        .HasColumnType("TEXT");

                    b.Property<int>("LooserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TourNumber")
                        .HasColumnType("INTEGER");

                    b.Property<byte?>("TypeDuel")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WinnerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CategoryInCompetitionId");

                    b.HasIndex("LooserId");

                    b.HasIndex("WinnerId");

                    b.ToTable("Duels");
                });

            modelBuilder.Entity("ArmWrestling.Domain.Database.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("CategoryInCompetitionId")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Score")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("TeamId")
                        .HasColumnType("INTEGER");

                    b.Property<float>("Weight")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("CategoryInCompetitionId");

                    b.HasIndex("TeamId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("ArmWrestling.Domain.Database.ResultPerson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryInCompetitionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Place")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ReasonAward")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CategoryInCompetitionId");

                    b.HasIndex("PersonId");

                    b.ToTable("ResultPersons");
                });

            modelBuilder.Entity("ArmWrestling.Domain.Database.ResultTeam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CompetitionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Place")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TeamId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CompetitionId");

                    b.HasIndex("TeamId");

                    b.ToTable("ResultTeams");
                });

            modelBuilder.Entity("ArmWrestling.Domain.Database.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CompetitionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Score")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CompetitionId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("ArmWrestling.Domain.Database.Category", b =>
                {
                    b.HasOne("ArmWrestling.Domain.Database.CategoryGroup", "CategoryGroup")
                        .WithMany()
                        .HasForeignKey("CategoryGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoryGroup");
                });

            modelBuilder.Entity("ArmWrestling.Domain.Database.CategoryInCompetition", b =>
                {
                    b.HasOne("ArmWrestling.Domain.Database.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArmWrestling.Domain.Database.Competition", "Competition")
                        .WithMany()
                        .HasForeignKey("CompetitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Competition");
                });

            modelBuilder.Entity("ArmWrestling.Domain.Database.Duel", b =>
                {
                    b.HasOne("ArmWrestling.Domain.Database.CategoryInCompetition", "CategoryInCompetition")
                        .WithMany()
                        .HasForeignKey("CategoryInCompetitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArmWrestling.Domain.Database.Person", "Looser")
                        .WithMany()
                        .HasForeignKey("LooserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArmWrestling.Domain.Database.Person", "Winner")
                        .WithMany()
                        .HasForeignKey("WinnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoryInCompetition");

                    b.Navigation("Looser");

                    b.Navigation("Winner");
                });

            modelBuilder.Entity("ArmWrestling.Domain.Database.Person", b =>
                {
                    b.HasOne("ArmWrestling.Domain.Database.CategoryInCompetition", "CategoryInCompetition")
                        .WithMany()
                        .HasForeignKey("CategoryInCompetitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArmWrestling.Domain.Database.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId");

                    b.Navigation("CategoryInCompetition");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("ArmWrestling.Domain.Database.ResultPerson", b =>
                {
                    b.HasOne("ArmWrestling.Domain.Database.CategoryInCompetition", "CategoryInCompetition")
                        .WithMany()
                        .HasForeignKey("CategoryInCompetitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArmWrestling.Domain.Database.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoryInCompetition");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("ArmWrestling.Domain.Database.ResultTeam", b =>
                {
                    b.HasOne("ArmWrestling.Domain.Database.Competition", "Competition")
                        .WithMany()
                        .HasForeignKey("CompetitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArmWrestling.Domain.Database.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Competition");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("ArmWrestling.Domain.Database.Team", b =>
                {
                    b.HasOne("ArmWrestling.Domain.Database.Competition", "Competition")
                        .WithMany()
                        .HasForeignKey("CompetitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Competition");
                });
#pragma warning restore 612, 618
        }
    }
}
