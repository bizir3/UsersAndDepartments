using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UsersAndDepartments.Models;

namespace UsersAndDepartments.Data
{
    public class DBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.UserId).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.DepId).IsUnique(false);
            modelBuilder.Entity<Department>().HasIndex(u => u.DepartmentId).IsUnique();

            modelBuilder.Entity<User>()
                .HasOne(u => u.Department)
                .WithMany(u => u.Users)
                .HasPrincipalKey(u => u.DepartmentId)
                .HasForeignKey(u => u.DepId);
            
            modelBuilder.Entity<Department>()
                .HasMany(u => u.Users)
                .WithOne(u => u.Department)
                .HasPrincipalKey(u => u.DepartmentId)
                .HasForeignKey(u => u.DepId);
        }
    }
}