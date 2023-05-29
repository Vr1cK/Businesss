using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Business;

public partial class BusinessContext : DbContext
{
    public BusinessContext()
    {
    }

    public BusinessContext(DbContextOptions<BusinessContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

    public virtual DbSet<WorkingHour> WorkingHours { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost;Database=Business;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__POSTS__3214EC07BD5E7DDA");

            entity.ToTable("POSTS");

            entity.Property(e => e.PostName).HasMaxLength(50);
            entity.Property(e => e.RatePerHour).HasColumnType("numeric(18, 0)");
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Workers__3214EC07BDD51172");

            entity.Property(e => e.Email)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Post).WithMany(p => p.Workers)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK__Workers__PostId__398D8EEE");
        });

        modelBuilder.Entity<WorkingHour>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__WorkingH__3214EC07A3979C8B");

            entity.Property(e => e.EndOfWork).HasColumnType("smalldatetime");
            entity.Property(e => e.StartOfWork).HasColumnType("smalldatetime");

            entity.HasOne(d => d.Worker).WithMany(p => p.WorkingHours)
                .HasForeignKey(d => d.WorkerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WorkingHo__Worke__3C69FB99");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
