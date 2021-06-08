﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WordApi.Models;

namespace WordApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210608202337_initSQLite")]
    partial class initSQLite
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("WordApi.Models.WordColor", b =>
                {
                    b.Property<int>("WordColorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Color")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("Word")
                        .HasColumnType("TEXT");

                    b.HasKey("WordColorId");

                    b.ToTable("WordColors");
                });
#pragma warning restore 612, 618
        }
    }
}
