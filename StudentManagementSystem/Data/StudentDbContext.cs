using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;
using StudentManagementSystem.Models.Domain;
using System.Reflection.Emit;

namespace StudentManagementSystem.Data
{
    public class StudentDbContext : IdentityDbContext
    {
        public StudentDbContext(DbContextOptions options): base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Facuilty> Facuilty { get; set; }
        public DbSet<Courses> courses { get; set; }
        public DbSet<Teachers> Teachers { get; set; }
        public DbSet<Relationship> Relation { get; set; }
        public DbSet<StudentCourses> Studentcourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .Property(s => s.phoneno)
                .HasColumnType("bigint");

            modelBuilder.Entity<Relationship>()
                .HasKey(r => new { r.TeacherId, r.CourseId });

            modelBuilder.Entity<StudentCourses>()
             .HasOne(sc => sc.Student)
             .WithMany(s => s.studentcourses)
             .HasForeignKey(sc => sc.StudentId);


            modelBuilder.Entity<StudentCourses>()
                .HasOne(sc => sc.Courses)
                .WithMany(c => c.studentcourses)
                .HasForeignKey(sc => sc.CourseId);

            base.OnModelCreating(modelBuilder);

   

        }
    }
}
