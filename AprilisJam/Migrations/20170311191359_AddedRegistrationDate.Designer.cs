using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using AprilisJam.Data;

namespace AprilisJam.Migrations
{
    [DbContext(typeof(AprilisJamRegistrationContext))]
    [Migration("20170311191359_AddedRegistrationDate")]
    partial class AddedRegistrationDate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AprilisJam.Models.RegistrationForm", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdditionalNotes");

                    b.Property<string>("AprilisQuestion");

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Phone")
                        .IsRequired();

                    b.Property<DateTime>("RegistrationDate");

                    b.Property<string>("School");

                    b.Property<string>("Surname")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("RegistrationForms");
                });
        }
    }
}
