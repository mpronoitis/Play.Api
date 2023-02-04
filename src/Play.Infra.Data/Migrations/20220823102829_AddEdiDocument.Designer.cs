﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Play.Infra.Data.Context;

#nullable disable

namespace Play.Infra.Data.Migrations
{
    [DbContext(typeof(PlayContext))]
    [Migration("20220823102829_AddEdiDocument")]
    partial class AddEdiDocument
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Play.Domain.Edi.Models.EdiConnection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<Guid>("Customer_Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Customer_Id");

                    b.Property<string>("Ftp_Hostname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Ftp_Hostname");

                    b.Property<string>("Ftp_Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Ftp_Password");

                    b.Property<string>("Ftp_Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Ftp_Username");

                    b.Property<Guid>("Model_Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Model_Id");

                    b.Property<Guid>("Org_Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Org_Id");

                    b.Property<Guid>("Profile_Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Profile_Id");

                    b.HasKey("Id");

                    b.ToTable("EdiConnections");
                });

            modelBuilder.Entity("Play.Domain.Edi.Models.EdiDocument", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<Guid>("Customer_Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Customer_Id");

                    b.Property<string>("DocumentPayload")
                        .HasColumnType("text")
                        .HasColumnName("DocumentPayload");

                    b.Property<string>("EdiPayload")
                        .HasColumnType("text")
                        .HasColumnName("EdiPayload");

                    b.Property<string>("Hedentid")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Hedentid");

                    b.Property<bool>("IsProcessed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("IsProcessed");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.ToTable("EdiDocuments");
                });

            modelBuilder.Entity("Play.Domain.Edi.Models.EdiModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("ElementSeparator")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)")
                        .HasColumnName("ElementSeparator");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit")
                        .HasColumnName("Enabled");

                    b.Property<Guid>("Org_Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Org_Id");

                    b.Property<string>("SegmentTerminator")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)")
                        .HasColumnName("SegmentTerminator");

                    b.Property<string>("SubElementSeparator")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)")
                        .HasColumnName("SubElementSeparator");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.ToTable("EdiModels");
                });

            modelBuilder.Entity("Play.Domain.Edi.Models.EdiOrganization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("EdiOrganizations");
                });

            modelBuilder.Entity("Play.Domain.Edi.Models.EdiProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<Guid>("Customer_Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Customer_Id");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit")
                        .HasColumnName("Enabled");

                    b.Property<Guid>("Model_Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Model_Id");

                    b.Property<string>("Payload")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Payload");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.ToTable("EdiProfiles");
                });

            modelBuilder.Entity("Play.Domain.Edi.Models.EdiSegment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("Description");

                    b.Property<Guid>("Model_Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Model_Id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.ToTable("EdiSegments");
                });

            modelBuilder.Entity("Play.Domain.Edi.Models.EdiVariable", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("Description");

                    b.Property<string>("Placeholder")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Placeholder");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.ToTable("EdiVariables");
                });

            modelBuilder.Entity("Play.Domain.Models.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });
#pragma warning restore 612, 618
        }
    }
}
