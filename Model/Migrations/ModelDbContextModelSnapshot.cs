﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Model.Configuration;

#nullable disable

namespace Model.Migrations
{
    [DbContext(typeof(ModelDbContext))]
    partial class ModelDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Model.Entities.Authentication.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ROLE_ID");

                    b.Property<string>("Description")
                        .HasColumnType("longtext")
                        .HasColumnName("DESCRIPTION");

                    b.Property<string>("Identifier")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("IDENTIFIER");

                    b.HasKey("Id");

                    b.HasIndex("Identifier")
                        .IsUnique();

                    b.ToTable("ROLES");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Administrator",
                            Identifier = "Admin"
                        });
                });

            modelBuilder.Entity("Model.Entities.Authentication.RoleClaim", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("USER_ID");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("ROLE_ID");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("USER_HAS_ROLES_JT");
                });

            modelBuilder.Entity("Model.Entities.Authentication.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("USER_ID");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("EMAIL");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("PASSWORD_HASH");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("USERNAME");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("USERS");
                });

            modelBuilder.Entity("Model.Entities.Authentication.RoleClaim", b =>
                {
                    b.HasOne("Model.Entities.Authentication.Role", "Role")
                        .WithMany("RoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Entities.Authentication.User", "User")
                        .WithMany("RoleClaims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Model.Entities.Authentication.Role", b =>
                {
                    b.Navigation("RoleClaims");
                });

            modelBuilder.Entity("Model.Entities.Authentication.User", b =>
                {
                    b.Navigation("RoleClaims");
                });
#pragma warning restore 612, 618
        }
    }
}