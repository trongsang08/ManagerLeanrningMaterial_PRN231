using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObjects.Models;

public partial class Prn231ProjectContext : DbContext
{
    public Prn231ProjectContext()
    {
    }

    public Prn231ProjectContext(DbContextOptions<Prn231ProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SubmitAssignment> SubmitAssignments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var builder = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("PRN231_Project"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("PK__Assignme__32499E777F60ED59");

            entity.Property(e => e.AssignmentName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Path).IsUnicode(false);
            entity.Property(e => e.RequiredDate).HasColumnType("datetime");

            entity.HasOne(d => d.Course).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__Assignmen__Cours__182C9B23");

            entity.HasOne(d => d.Uploader).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.UploaderId)
                .HasConstraintName("FK__Assignmen__Uploa__1920BF5C");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Courses__C92D71A703317E3D");

            entity.Property(e => e.CourseCode)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.CourseName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.MaterialId).HasName("PK__Material__C50610F707020F21");

            entity.Property(e => e.MaterialName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Path).IsUnicode(false);

            entity.HasOne(d => d.Course).WithMany(p => p.Materials)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__Materials__Cours__1A14E395");

            entity.HasOne(d => d.Uploader).WithMany(p => p.Materials)
                .HasForeignKey(d => d.UploaderId)
                .HasConstraintName("FK__Materials__Uploa__1B0907CE");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A0AD2A005");

            entity.Property(e => e.RoleName)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SubmitAssignment>(entity =>
        {
            entity.HasKey(e => e.SubmitAssignmentId).HasName("PK__SubmitAs__E17008450EA330E9");

            entity.ToTable("SubmitAssignment");

            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Path).IsUnicode(false);
            entity.Property(e => e.SubmitAssignmentName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Assignment).WithMany(p => p.SubmitAssignments)
                .HasForeignKey(d => d.AssignmentId)
                .HasConstraintName("FK__SubmitAss__Assig__1BFD2C07");

            entity.HasOne(d => d.Uploader).WithMany(p => p.SubmitAssignments)
                .HasForeignKey(d => d.UploaderId)
                .HasConstraintName("FK__SubmitAss__Uploa__1CF15040");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C164452B1");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Fullname)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password).IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__RoleId__1FCDBCEB");

            entity.HasMany(d => d.Courses).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserCourse",
                    r => r.HasOne<Course>().WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserCours__Cours__1DE57479"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserCours__UserI__1ED998B2"),
                    j =>
                    {
                        j.HasKey("UserId", "CourseId").HasName("PK__UserCour__7B1A1B561273C1CD");
                        j.ToTable("UserCourse");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
