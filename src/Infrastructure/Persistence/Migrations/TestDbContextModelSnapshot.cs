﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(TestDbContext))]
    partial class TestDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Entities.ConsumerBase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("ConsumerDateFieldOne")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ConsumerDateFieldTwo")
                        .HasColumnType("datetime2");

                    b.Property<string>("ConsumerStringFieldOne")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConsumerStringFieldThree")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConsumerStringFieldTwo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsOrganisation")
                        .HasColumnType("bit");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Consumers", (string)null);

                    b.HasDiscriminator<bool>("IsOrganisation");
                });

            modelBuilder.Entity("Domain.Entities.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DocumentDateFieldOne")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DocumentDateFieldTwo")
                        .HasColumnType("datetime2");

                    b.Property<string>("DocumentStringFieldOne")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DocumentStringFieldThree")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DocumentStringFieldTwo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Documents", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.OrderBase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Identifier")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Orders", (string)null);

                    b.HasDiscriminator<int>("OrderType");
                });

            modelBuilder.Entity("Domain.Entities.CaveatOrder", b =>
                {
                    b.HasBaseType("Domain.Entities.OrderBase");

                    b.Property<DateTime>("CaveatDateFieldOne")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CaveatDateFieldTwo")
                        .HasColumnType("datetime2");

                    b.Property<string>("CaveatStringFieldOne")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CaveatStringFieldThree")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CaveatStringFieldTwo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("Domain.Entities.IndividualConsumer", b =>
                {
                    b.HasBaseType("Domain.Entities.ConsumerBase");

                    b.Property<string>("IndividualName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("OrderId");

                    b.HasDiscriminator().HasValue(false);
                });

            modelBuilder.Entity("Domain.Entities.OrganisationalConsumer", b =>
                {
                    b.HasBaseType("Domain.Entities.ConsumerBase");

                    b.Property<string>("CompanyType")
                        .IsRequired()
                        .HasColumnType("char(20)");

                    b.Property<string>("OrganisationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("OrderId");

                    b.HasDiscriminator().HasValue(true);
                });

            modelBuilder.Entity("Domain.Entities.SettlementOrder", b =>
                {
                    b.HasBaseType("Domain.Entities.OrderBase");

                    b.Property<DateTime>("SettlementDateFieldOne")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SettlementDateFieldTwo")
                        .HasColumnType("datetime2");

                    b.Property<string>("SettlementStringFieldOne")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SettlementStringFieldThree")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SettlementStringFieldTwo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("Domain.Entities.ConsumerBase", b =>
                {
                    b.OwnsOne("Domain.ValueObjects.CommonAddress", "Address", b1 =>
                        {
                            b1.Property<int>("ConsumerBaseId")
                                .HasColumnType("int");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("State");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Street");

                            b1.Property<string>("Suburb")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Suburb");

                            b1.HasKey("ConsumerBaseId");

                            b1.ToTable("Consumers");

                            b1.WithOwner()
                                .HasForeignKey("ConsumerBaseId");
                        });

                    b.Navigation("Address")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Document", b =>
                {
                    b.HasOne("Domain.Entities.OrderBase", "Order")
                        .WithMany("Documents")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Domain.Entities.OrderBase", b =>
                {
                    b.OwnsOne("Domain.ValueObjects.CommonAddress", "PropertyAddress", b1 =>
                        {
                            b1.Property<int>("OrderBaseId")
                                .HasColumnType("int");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Suburb")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("OrderBaseId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderBaseId");
                        });

                    b.Navigation("PropertyAddress")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.IndividualConsumer", b =>
                {
                    b.HasOne("Domain.Entities.OrderBase", "Order")
                        .WithMany("IndividualConsumers")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Domain.Entities.OrganisationalConsumer", b =>
                {
                    b.HasOne("Domain.Entities.OrderBase", "Order")
                        .WithMany("OrganisationalConsumers")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Domain.Entities.SettlementOrder", b =>
                {
                    b.OwnsOne("Domain.ValueObjects.ForwardingAddress", "ForwardingAddress", b1 =>
                        {
                            b1.Property<int>("SettlementOrderId")
                                .HasColumnType("int");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ForwardingAddressCountry");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ForwardingAddressState");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ForwardingAddressStreet");

                            b1.Property<string>("Suburb")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ForwardingAddressSuburb");

                            b1.HasKey("SettlementOrderId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("SettlementOrderId");
                        });

                    b.Navigation("ForwardingAddress")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.OrderBase", b =>
                {
                    b.Navigation("Documents");

                    b.Navigation("IndividualConsumers");

                    b.Navigation("OrganisationalConsumers");
                });
#pragma warning restore 612, 618
        }
    }
}
