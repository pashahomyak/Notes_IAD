using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
//Scaffold-DbContext "Server=COMPUTER\SQLEXPRESS;Database=Notes;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -DataAnnotations -Force
namespace Notes.Models
{
    public partial class NotesContext : DbContext
    {
        public NotesContext()
        {
        }

        public NotesContext(DbContextOptions<NotesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Note> Note { get; set; }
        public virtual DbSet<NoteCategory> NoteCategory { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserHasNote> UserHasNote { get; set; }
        public virtual DbSet<UserHasNoteCategory> UserHasNoteCategory { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>(entity =>
            {
                entity.Property(e => e.IdNoteCategory).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdNoteCategoryNavigation)
                    .WithMany(p => p.Note)
                    .HasForeignKey(d => d.IdNoteCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_note_note_category");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.IdUserType).HasDefaultValueSql("((2))");

                entity.HasOne(d => d.IdUserTypeNavigation)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.IdUserType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_user_type");
            });

            modelBuilder.Entity<UserHasNote>(entity =>
            {
                entity.HasKey(e => new { e.IdUser, e.IdNote });

                entity.HasOne(d => d.IdNoteNavigation)
                    .WithMany(p => p.UserHasNote)
                    .HasForeignKey(d => d.IdNote)
                    .HasConstraintName("FK_user_has_note_note");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.UserHasNote)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_user_has_note_user");
            });

            modelBuilder.Entity<UserHasNoteCategory>(entity =>
            {
                entity.HasKey(e => new { e.IdUser, e.IdNoteCategory });

                entity.HasOne(d => d.IdNoteCategoryNavigation)
                    .WithMany(p => p.UserHasNoteCategory)
                    .HasForeignKey(d => d.IdNoteCategory)
                    .HasConstraintName("FK_user_has_note_category_note_category");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.UserHasNoteCategory)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_user_has_note_category_user");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
