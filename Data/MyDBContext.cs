﻿using Microsoft.EntityFrameworkCore;
using Web_DonNghiPhep.Models;

namespace Web_DonNghiPhep.Data
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserID);

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserID, ur.RoleID }); 

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleID);


            modelBuilder.Entity<User>()
                .HasOne(u => u.EmployeeUs)
                .WithOne(e => e.User)
                .HasForeignKey<User>(u => u.Employee_ID)
                .OnDelete(DeleteBehavior.Cascade);  // Xóa User nếu Employee bị xóa


            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.Department_id)
                .OnDelete(DeleteBehavior.SetNull);
        
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Title)
                .WithMany(t => t.Employees)
                .HasForeignKey(e => e.Title_id)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Log>()
                .HasOne(l => l.User)
                .WithMany(u => u.Logs)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<LeaveBalance>()
                .HasOne(lb => lb.Employee)
                .WithMany(e => e.LeaveBalances)
                .HasForeignKey(lb => lb.Employee_id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LeaveRequest>()
                .HasOne(lr => lr.Employee)
                .WithMany(e => e.LeaveRequests)
                .HasForeignKey(lr => lr.Employee_id)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<User> User { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Roles> Role { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Titles> Title { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<LeaveBalance> LeaveBalance { get; set; }
        public DbSet<LeaveRequest> LeaveRequest { get; set; }
        public DbSet<Log> Log { get; set; }
    }
}
