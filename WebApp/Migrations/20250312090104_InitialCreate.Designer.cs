﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApp.Database;

#nullable disable

namespace WebApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250312090104_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.2");

            modelBuilder.Entity("WebApp.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<int?>("StockId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("StockId");

                    b.ToTable("Comment");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Content = "Apple's recent earnings report exceeded expectations.",
                            CreatedOn = new DateTime(2020, 3, 11, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            StockId = 1,
                            Title = "Strong Performance"
                        },
                        new
                        {
                            Id = 2,
                            Content = "Microsoft continues to show strong revenue growth in cloud services.",
                            CreatedOn = new DateTime(2025, 3, 11, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            StockId = 2,
                            Title = "Consistent Growth"
                        },
                        new
                        {
                            Id = 3,
                            Content = "Tesla's stock is overvalued according to analysts.",
                            CreatedOn = new DateTime(2025, 3, 11, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            StockId = 3,
                            Title = "High Valuation"
                        },
                        new
                        {
                            Id = 4,
                            Content = "Alphabet's AI initiatives are expected to drive future growth.",
                            CreatedOn = new DateTime(2025, 3, 11, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            StockId = 4,
                            Title = "Innovative Push"
                        },
                        new
                        {
                            Id = 5,
                            Content = "Amazon dominates the e-commerce market, but faces supply chain challenges.",
                            CreatedOn = new DateTime(2025, 3, 11, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            StockId = 5,
                            Title = "Market Leader"
                        },
                        new
                        {
                            Id = 6,
                            Content = "Apple's move into VR is exciting for the future.",
                            CreatedOn = new DateTime(2025, 3, 11, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            StockId = 1,
                            Title = "Future Prospects"
                        },
                        new
                        {
                            Id = 7,
                            Content = "Microsoft Azure is outperforming competitors.",
                            CreatedOn = new DateTime(2025, 3, 11, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            StockId = 2,
                            Title = "Cloud Strength"
                        });
                });

            modelBuilder.Entity("WebApp.Models.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Industry")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("LastDiv")
                        .HasColumnType("TEXT");

                    b.Property<long>("MarketCap")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Purchase")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Stock");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CompanyName = "Apple Inc.",
                            Industry = "Technology",
                            LastDiv = 0.88m,
                            MarketCap = 2500000000000L,
                            Purchase = 150.25m,
                            Symbol = "AAPL"
                        },
                        new
                        {
                            Id = 2,
                            CompanyName = "Microsoft Corporation",
                            Industry = "Technology",
                            LastDiv = 1.12m,
                            MarketCap = 2400000000000L,
                            Purchase = 320.50m,
                            Symbol = "MSFT"
                        },
                        new
                        {
                            Id = 3,
                            CompanyName = "Tesla Inc.",
                            Industry = "Automotive",
                            LastDiv = 0.00m,
                            MarketCap = 1000000000000L,
                            Purchase = 800.75m,
                            Symbol = "TSLA"
                        },
                        new
                        {
                            Id = 4,
                            CompanyName = "Alphabet Inc.",
                            Industry = "Technology",
                            LastDiv = 0.75m,
                            MarketCap = 1800000000000L,
                            Purchase = 2800.90m,
                            Symbol = "GOOGL"
                        },
                        new
                        {
                            Id = 5,
                            CompanyName = "Amazon.com Inc.",
                            Industry = "E-Commerce",
                            LastDiv = 0.00m,
                            MarketCap = 1700000000000L,
                            Purchase = 3450.60m,
                            Symbol = "AMZN"
                        });
                });

            modelBuilder.Entity("WebApp.Models.Comment", b =>
                {
                    b.HasOne("WebApp.Models.Stock", "Stock")
                        .WithMany("Comments")
                        .HasForeignKey("StockId");

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("WebApp.Models.Stock", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
