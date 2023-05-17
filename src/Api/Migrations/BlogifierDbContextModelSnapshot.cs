﻿// <auto-generated />
using System;
using Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Api.Migrations
{
    [DbContext(typeof(BlogifierDbContext))]
    partial class BlogifierDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("Domain.Models.Blog", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("BlogName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOnDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Published")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Blogs");

                    b.HasData(
                        new
                        {
                            Id = "5b1c2b4d-48c7-402a-80c3-cc796ad49c6b",
                            BlogName = "First Seeded Blog",
                            CreatedOnDate = new DateTime(2023, 4, 15, 0, 24, 19, 851, DateTimeKind.Utc).AddTicks(4737),
                            Published = true
                        },
                        new
                        {
                            Id = "d28888e9-2ba9-473a-a40f-e38cb54f9b35",
                            BlogName = "Second Seeded Blog",
                            CreatedOnDate = new DateTime(2023, 4, 15, 0, 24, 19, 851, DateTimeKind.Utc).AddTicks(4763),
                            Published = true
                        });
                });

            modelBuilder.Entity("Domain.Models.Post", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("BlogId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BlogId");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            Id = "493c3228-3444-4a49-9cc0-e8532edc59b2",
                            BlogId = "d28888e9-2ba9-473a-a40f-e38cb54f9b35",
                            Content = "Test 1 Seeded content",
                            Title = "First Post Seeded"
                        },
                        new
                        {
                            Id = "24810dfc-2d94-4cc7-aab5-cdf98b83f0c9",
                            BlogId = "d28888e9-2ba9-473a-a40f-e38cb54f9b35",
                            Content = "Test 2 Seeded anonymouspost content",
                            Title = "Second post"
                        });
                });

            modelBuilder.Entity("Domain.Models.Post", b =>
                {
                    b.HasOne("Domain.Models.Blog", "Blog")
                        .WithMany("Posts")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.Models.Name", "AuthorName", b1 =>
                        {
                            b1.Property<string>("PostId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("First")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Last")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("PostId");

                            b1.ToTable("Posts");

                            b1.WithOwner()
                                .HasForeignKey("PostId");

                            b1.HasData(
                                new
                                {
                                    PostId = "493c3228-3444-4a49-9cc0-e8532edc59b2",
                                    First = "Andriy",
                                    Last = "Svyryd"
                                },
                                new
                                {
                                    PostId = "24810dfc-2d94-4cc7-aab5-cdf98b83f0c9",
                                    First = "Diego",
                                    Last = "Vega"
                                });
                        });

                    b.Navigation("AuthorName")
                        .IsRequired();

                    b.Navigation("Blog");
                });

            modelBuilder.Entity("Domain.Models.Blog", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}