﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SchoolTest2.Models;

namespace SchoolTest2.Migrations
{
    [DbContext(typeof(SchoolContext))]
    partial class SchoolContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6");

            modelBuilder.Entity("SchoolTest2.Models.Day", b =>
                {
                    b.Property<int>("DayId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("DayId");

                    b.ToTable("Days");
                });

            modelBuilder.Entity("SchoolTest2.Models.DaySubject", b =>
                {
                    b.Property<int>("DayId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SubjectId")
                        .HasColumnType("INTEGER");

                    b.HasKey("DayId", "SubjectId");

                    b.HasIndex("SubjectId");

                    b.ToTable("DaySubjects");
                });

            modelBuilder.Entity("SchoolTest2.Models.Seminar", b =>
                {
                    b.Property<int>("SeminarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("SeminarId");

                    b.ToTable("Seminars");
                });

            modelBuilder.Entity("SchoolTest2.Models.SeminarDay", b =>
                {
                    b.Property<int>("SeminarId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DayId")
                        .HasColumnType("INTEGER");

                    b.HasKey("SeminarId", "DayId");

                    b.HasIndex("DayId");

                    b.ToTable("SeminarDays");
                });

            modelBuilder.Entity("SchoolTest2.Models.Subject", b =>
                {
                    b.Property<int>("SubjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RequiredReading")
                        .HasColumnType("TEXT");

                    b.HasKey("SubjectId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("SchoolTest2.Models.DaySubject", b =>
                {
                    b.HasOne("SchoolTest2.Models.Day", "Day")
                        .WithMany("DaySubjects")
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolTest2.Models.Subject", "Subject")
                        .WithMany("DaySubjects")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SchoolTest2.Models.SeminarDay", b =>
                {
                    b.HasOne("SchoolTest2.Models.Day", "Day")
                        .WithMany("SeminarDays")
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolTest2.Models.Seminar", "Seminar")
                        .WithMany("SeminarDays")
                        .HasForeignKey("SeminarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
