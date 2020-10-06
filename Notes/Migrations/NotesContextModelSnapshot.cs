﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Notes.Models;

namespace Notes.Migrations
{
    [DbContext(typeof(NotesContext))]
    partial class NotesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Notes.Models.Note", b =>
                {
                    b.Property<int>("IdNote")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id_note")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("text");

                    b.Property<string>("Header")
                        .HasColumnName("header")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("IdNoteCategory")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id_note_category")
                        .HasColumnType("int")
                        .HasDefaultValueSql("((1))");

                    b.Property<string>("ImagePath")
                        .HasColumnName("image_path")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<bool>("IsFavorites")
                        .HasColumnName("is_favorites")
                        .HasColumnType("bit");

                    b.HasKey("IdNote");

                    b.HasIndex("IdNoteCategory");

                    b.ToTable("note");
                });

            modelBuilder.Entity("Notes.Models.NoteCategory", b =>
                {
                    b.Property<int>("IdNoteCategory")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id_note_category")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("IdNoteCategory");

                    b.ToTable("note_category");
                });

            modelBuilder.Entity("Notes.Models.User", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id_user")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int>("IdUserType")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id_user_type")
                        .HasColumnType("int")
                        .HasDefaultValueSql("((2))");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnName("login")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("password")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("IdUser");

                    b.HasIndex("IdUserType");

                    b.ToTable("user");
                });

            modelBuilder.Entity("Notes.Models.UserHasNote", b =>
                {
                    b.Property<int>("IdUser")
                        .HasColumnName("id_user")
                        .HasColumnType("int");

                    b.Property<int>("IdNote")
                        .HasColumnName("id_note")
                        .HasColumnType("int");

                    b.HasKey("IdUser", "IdNote");

                    b.HasIndex("IdNote");

                    b.ToTable("user_has_note");
                });

            modelBuilder.Entity("Notes.Models.UserType", b =>
                {
                    b.Property<int>("IdUserType")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id_user_type")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("IdUserType");

                    b.ToTable("user_type");
                });

            modelBuilder.Entity("Notes.Models.Note", b =>
                {
                    b.HasOne("Notes.Models.NoteCategory", "IdNoteCategoryNavigation")
                        .WithMany("Note")
                        .HasForeignKey("IdNoteCategory")
                        .HasConstraintName("FK_note_note_category")
                        .IsRequired();
                });

            modelBuilder.Entity("Notes.Models.User", b =>
                {
                    b.HasOne("Notes.Models.UserType", "IdUserTypeNavigation")
                        .WithMany("User")
                        .HasForeignKey("IdUserType")
                        .HasConstraintName("FK_user_user_type")
                        .IsRequired();
                });

            modelBuilder.Entity("Notes.Models.UserHasNote", b =>
                {
                    b.HasOne("Notes.Models.Note", "IdNoteNavigation")
                        .WithMany("UserHasNote")
                        .HasForeignKey("IdNote")
                        .HasConstraintName("FK_user_has_note_note")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Notes.Models.User", "IdUserNavigation")
                        .WithMany("UserHasNote")
                        .HasForeignKey("IdUser")
                        .HasConstraintName("FK_user_has_note_user")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}