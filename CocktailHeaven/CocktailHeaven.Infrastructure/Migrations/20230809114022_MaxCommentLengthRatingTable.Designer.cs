﻿// <auto-generated />
using System;
using CocktailHeaven.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CocktailHeaven.Infrastructure.Migrations
{
    [DbContext(typeof(CocktailHeavenDbContext))]
    [Migration("20230809114022_MaxCommentLengthRatingTable")]
    partial class MaxCommentLengthRatingTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CocktailHeaven.Infrastructure.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsDeleted = false,
                            Name = "Contemporary Classics"
                        },
                        new
                        {
                            Id = 2,
                            IsDeleted = false,
                            Name = "New Era Drinks"
                        },
                        new
                        {
                            Id = 3,
                            IsDeleted = false,
                            Name = "The Unforgettables"
                        });
                });

            modelBuilder.Entity("CocktailHeaven.Infrastructure.Models.Cocktail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("AddedByUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Garnish")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<Guid>("ImageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Instruction")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("AddedByUserId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ImageId")
                        .IsUnique();

                    b.ToTable("Cocktails");
                });

            modelBuilder.Entity("CocktailHeaven.Infrastructure.Models.CocktailIngredient", b =>
                {
                    b.Property<int>("CocktailId")
                        .HasColumnType("int");

                    b.Property<int>("IngredientId")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Quantity")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CocktailId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.ToTable("CocktailIngredients");
                });

            modelBuilder.Entity("CocktailHeaven.Infrastructure.Models.Identity.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("d273e367-ebf6-44b3-afa7-7759a0a579ee"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "9621cd70-441b-40eb-8f4e-ae5ac12b9550",
                            Email = "gmihov@mail.com",
                            EmailConfirmed = false,
                            IsDeleted = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "GMIHOV@MAIL.COM",
                            NormalizedUserName = "GEORGE",
                            PasswordHash = "AQAAAAEAACcQAAAAEKsbDHya/RrYeWhsVzA7UN7fqFCSNkWcA0tjExxtANxdkp+NugFA/vG4R4P/KAGL/Q==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "316be357-ba73-440d-ba21-42be5eb9d72a",
                            TwoFactorEnabled = false,
                            UserName = "George"
                        },
                        new
                        {
                            Id = new Guid("9622851e-71b4-439b-8d86-9e8cba27ca1e"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "a86c8d2e-c638-4f7d-9a0d-8f40ce563b36",
                            Email = "devora@mail.com",
                            EmailConfirmed = false,
                            IsDeleted = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "DEVORA@MAIL.COM",
                            NormalizedUserName = "DEV",
                            PasswordHash = "AQAAAAEAACcQAAAAEF3kHFt5LohdHMD5R+wLrUrkd0jqpwxQUt4+FPfycstydppwe1GDFw4cVvrhK3k4/Q==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "1f1ad3c1-ced6-4991-bd79-59651498a887",
                            TwoFactorEnabled = false,
                            UserName = "Dev"
                        },
                        new
                        {
                            Id = new Guid("4e797b0b-c669-4bf1-913c-a90fe951241f"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "4e7858d4-e98a-4c80-bc04-d1935e4530df",
                            Email = "aleks@mail.com",
                            EmailConfirmed = false,
                            IsDeleted = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "ALEKS@MAIL.COM",
                            NormalizedUserName = "ALEKS",
                            PasswordHash = "AQAAAAEAACcQAAAAEDlbcx+aujDYyoqm+TdLudUnbq7Llh93eIfi7QN0GDwxzu64xUaZA9wKgZPDaLCFAw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "bb779f04-1d82-4b6e-b26b-e725d6d11e63",
                            TwoFactorEnabled = false,
                            UserName = "aleks"
                        });
                });

            modelBuilder.Entity("CocktailHeaven.Infrastructure.Models.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Extension")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("ExternalURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.HasKey("Id");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("CocktailHeaven.Infrastructure.Models.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("CocktailHeaven.Infrastructure.Models.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("AddedByUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CocktailId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<float>("Value")
                        .HasMaxLength(5)
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("AddedByUserId");

                    b.HasIndex("CocktailId");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("CocktailHeaven.Infrastructure.Models.UserCollection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("AddedByUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CocktailId")
                        .HasColumnType("int");

                    b.Property<bool?>("HasTried")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsFavourite")
                        .HasColumnType("bit");

                    b.Property<bool?>("WishList")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AddedByUserId");

                    b.HasIndex("CocktailId");

                    b.ToTable("UserCollections");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("ebabec4b-1413-4d79-8d3a-b55742b6f7b6"),
                            ConcurrencyStamp = "280c1900-8b1a-452b-8c4f-98171ecdb050",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = new Guid("ed23ddd6-0cab-4a38-943a-61c5d396bfba"),
                            ConcurrencyStamp = "184d9703-3e95-46a7-b267-ebf7909c904c",
                            Name = "Cocktail Editor",
                            NormalizedName = "COCKTAIL EDITOR"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = new Guid("4e797b0b-c669-4bf1-913c-a90fe951241f"),
                            RoleId = new Guid("ed23ddd6-0cab-4a38-943a-61c5d396bfba")
                        },
                        new
                        {
                            UserId = new Guid("d273e367-ebf6-44b3-afa7-7759a0a579ee"),
                            RoleId = new Guid("ebabec4b-1413-4d79-8d3a-b55742b6f7b6")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("CocktailHeaven.Infrastructure.Models.Cocktail", b =>
                {
                    b.HasOne("CocktailHeaven.Infrastructure.Models.Identity.ApplicationUser", "AddedByUser")
                        .WithMany()
                        .HasForeignKey("AddedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CocktailHeaven.Infrastructure.Models.Category", "Category")
                        .WithMany("Cocktails")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CocktailHeaven.Infrastructure.Models.Image", "Image")
                        .WithOne("Cocktail")
                        .HasForeignKey("CocktailHeaven.Infrastructure.Models.Cocktail", "ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AddedByUser");

                    b.Navigation("Category");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("CocktailHeaven.Infrastructure.Models.CocktailIngredient", b =>
                {
                    b.HasOne("CocktailHeaven.Infrastructure.Models.Cocktail", "Cocktail")
                        .WithMany("Ingredients")
                        .HasForeignKey("CocktailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CocktailHeaven.Infrastructure.Models.Ingredient", "Ingredient")
                        .WithMany("Cocktails")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cocktail");

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("CocktailHeaven.Infrastructure.Models.Rating", b =>
                {
                    b.HasOne("CocktailHeaven.Infrastructure.Models.Identity.ApplicationUser", "AddedByUser")
                        .WithMany("Ratings")
                        .HasForeignKey("AddedByUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CocktailHeaven.Infrastructure.Models.Cocktail", "Cocktail")
                        .WithMany("Ratings")
                        .HasForeignKey("CocktailId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AddedByUser");

                    b.Navigation("Cocktail");
                });

            modelBuilder.Entity("CocktailHeaven.Infrastructure.Models.UserCollection", b =>
                {
                    b.HasOne("CocktailHeaven.Infrastructure.Models.Identity.ApplicationUser", "AddedByUser")
                        .WithMany("UserCollection")
                        .HasForeignKey("AddedByUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CocktailHeaven.Infrastructure.Models.Cocktail", "Cocktail")
                        .WithMany("UserCollection")
                        .HasForeignKey("CocktailId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AddedByUser");

                    b.Navigation("Cocktail");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("CocktailHeaven.Infrastructure.Models.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("CocktailHeaven.Infrastructure.Models.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CocktailHeaven.Infrastructure.Models.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("CocktailHeaven.Infrastructure.Models.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CocktailHeaven.Infrastructure.Models.Category", b =>
                {
                    b.Navigation("Cocktails");
                });

            modelBuilder.Entity("CocktailHeaven.Infrastructure.Models.Cocktail", b =>
                {
                    b.Navigation("Ingredients");

                    b.Navigation("Ratings");

                    b.Navigation("UserCollection");
                });

            modelBuilder.Entity("CocktailHeaven.Infrastructure.Models.Identity.ApplicationUser", b =>
                {
                    b.Navigation("Ratings");

                    b.Navigation("UserCollection");
                });

            modelBuilder.Entity("CocktailHeaven.Infrastructure.Models.Image", b =>
                {
                    b.Navigation("Cocktail")
                        .IsRequired();
                });

            modelBuilder.Entity("CocktailHeaven.Infrastructure.Models.Ingredient", b =>
                {
                    b.Navigation("Cocktails");
                });
#pragma warning restore 612, 618
        }
    }
}
