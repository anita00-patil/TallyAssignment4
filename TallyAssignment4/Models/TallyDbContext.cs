using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TallyAssignment4.Models;

public partial class TallyDbContext : DbContext
{
   

    public TallyDbContext(DbContextOptions<TallyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__32C52B99D5511852");

            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubId).HasName("PK__Subjects__4D9BB84A29E22546");

            entity.Property(e => e.SubjectName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Student).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__Subjects__Studen__267ABA7A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
