﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TSC.Presistance;

namespace TSC.Migrations
{
    [DbContext(typeof(TSCDbContext))]
    partial class TSCDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TSC.Core.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("TSC.Core.Models.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("ContactEmail")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("ContactName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("TSC.Core.Models.TicketAssign", b =>
                {
                    b.Property<int>("TicketId");

                    b.Property<int>("GroupId");

                    b.Property<DateTime>("AssignDate");

                    b.Property<bool>("Status");

                    b.HasKey("TicketId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("TicketsAssign");
                });

            modelBuilder.Entity("TSC.Core.Models.UserGroup", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("GroupId");

                    b.HasKey("UserId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("UserGroups");
                });

            modelBuilder.Entity("TSC.Core.Models.TicketAssign", b =>
                {
                    b.HasOne("TSC.Core.Models.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TSC.Core.Models.Ticket", "Ticket")
                        .WithMany()
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TSC.Core.Models.UserGroup", b =>
                {
                    b.HasOne("TSC.Core.Models.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
