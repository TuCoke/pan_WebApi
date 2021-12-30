﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pan.Infrastructure.Context;

namespace Pan.Infrastructure.Migrations
{
    [DbContext(typeof(EFCoreDbContext))]
    partial class EFCoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.8");

            modelBuilder.Entity("Pan.Infrastructure.Entity.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Account")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreateOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("Gender")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastLoginTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Mail")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .HasMaxLength(22)
                        .HasColumnType("varchar(22)");

                    b.Property<string>("Salt")
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateOn")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("Pan.Infrastructure.Entity.FileStorage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FileExt")
                        .HasColumnType("longtext");

                    b.Property<double>("FileSize")
                        .HasColumnType("double");

                    b.Property<int>("FileType")
                        .HasColumnType("int");

                    b.Property<string>("HashCode")
                        .HasColumnType("longtext");

                    b.Property<string>("OSSBucketName")
                        .HasColumnType("longtext");

                    b.Property<string>("OSSETag")
                        .HasColumnType("longtext");

                    b.Property<string>("OSSName")
                        .HasColumnType("longtext");

                    b.Property<string>("OSSRequestId")
                        .HasColumnType("longtext");

                    b.Property<string>("PartDir")
                        .HasColumnType("longtext");

                    b.Property<string>("PathLocal")
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("UpdateOn")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("FileStorage");
                });

            modelBuilder.Entity("Pan.Infrastructure.Entity.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("request_date")
                        .HasColumnType("longtext");

                    b.Property<string>("request_http")
                        .HasColumnType("longtext");

                    b.Property<string>("request_id")
                        .HasColumnType("longtext");

                    b.Property<string>("request_url")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Log");
                });

            modelBuilder.Entity("Pan.Infrastructure.Entity.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("UpdateOn")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("Pan.Infrastructure.Entity.post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("Tags")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("UpdateOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("aliyun_url")
                        .HasColumnType("longtext");

                    b.Property<string>("createTime")
                        .HasColumnType("longtext");

                    b.Property<string>("del_url")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("htmlContext")
                        .HasColumnType("longtext");

                    b.Property<string>("next_url")
                        .HasColumnType("longtext");

                    b.Property<string>("prev")
                        .HasColumnType("longtext");

                    b.Property<string>("request_id")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("post");
                });

            modelBuilder.Entity("Pan.Infrastructure.Entity.posts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("HtmlContext")
                        .HasColumnType("longtext");

                    b.Property<string>("Request_id")
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Tags")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("UpdateOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("aliyun_url")
                        .HasColumnType("longtext");

                    b.Property<string>("createTime")
                        .HasColumnType("longtext");

                    b.Property<string>("del_url")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("next_url")
                        .HasColumnType("longtext");

                    b.Property<string>("prev")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("posts");
                });
#pragma warning restore 612, 618
        }
    }
}
