using Microsoft.EntityFrameworkCore;
using ReservationSystemMVC.Domain.Entities;
using ReservationSystemMVC.Domain.Enums;

namespace ReservationSystemMVC.Domain.Contexts;

    public sealed class ReservationSystemDbContext : DbContext
    {
        public ReservationSystemDbContext(DbContextOptions<ReservationSystemDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<ChiefEntity> Chiefs { get; set; } = null!;

        public DbSet<ClassroomEntity> Classrooms { get; set; } = null!;

        public DbSet<ReservationEntity> Reservations { get; set; } = null!;

        public DbSet<ReservationRequestEntity> ReservationRequests { get; set; } = null!;

        public DbSet<StudentEntity> Students { get; set; } = null!;

        public DbSet<ChiefClassroomEntity> ChiefClassrooms { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiefEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Email).IsRequired();
            });

            modelBuilder.Entity<ClassroomEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.RoomNumber).IsRequired();
                entity.Property(e => e.Capacity).IsRequired();
                entity.Property(e => e.Location).IsRequired();
            });

            modelBuilder.Entity<ReservationEntity>(entity => { entity.HasKey(e => e.StudentId); });

            modelBuilder.Entity<ReservationRequestEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Status).HasDefaultValue(ReservationStatus.Pending);
            });

            modelBuilder.Entity<StudentEntity>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).IsRequired();
                entity.Property(x => x.Email).IsRequired();
            });

            modelBuilder.Entity<ChiefClassroomEntity>(entity => { entity.HasKey(e => e.Id); });

            // Student-ReservationRequest One-To-Many

            modelBuilder.Entity<ReservationRequestEntity>()
                .HasOne<StudentEntity>(s => s.Student)
                .WithMany(r => r.ReservationRequestList)
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Classroom-ReservationRequest One-To-Many

            modelBuilder.Entity<ClassroomEntity>()
                .HasMany(c => c.ReservationRequestList)
                .WithOne(r => r.Classroom)
                .HasForeignKey(r => r.ClassroomId)
                .OnDelete(DeleteBehavior.Cascade);

            // Student-Reservation One-To-Many

            modelBuilder.Entity<ReservationEntity>()
                .HasOne(s => s.Student)
                .WithMany(r => r.ReservationList)
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Classroom-Reservation One-To-Many

            modelBuilder.Entity<ClassroomEntity>()
                .HasMany(c => c.ReservationList)
                .WithOne(r => r.Classroom)
                .HasForeignKey(r => r.ClassroomId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ChiefEntity>()
                .HasMany(x => x.ChiefClassrooms)
                .WithOne(x => x.Chief)
                .HasForeignKey(x => x.ChiefId);

            modelBuilder.Entity<ClassroomEntity>()
                .HasMany(x => x.ChiefClassrooms)
                .WithOne(x => x.Classroom)
                .HasForeignKey(x => x.ClassroomId);
        }
    }