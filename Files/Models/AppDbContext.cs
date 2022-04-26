using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Files.Models
{
    public class AppDbContext : DbContext
    {
        private IConfiguration _configuration;

        public DbSet<Archive> Archive { get; set; }
        public DbSet<FilesUsers> FilesUsers { get; set; }
        public DbSet<Folder> Folder { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public AppDbContext()
        {

        }

        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            string stringConnection;

            stringConnection = configuration.GetConnectionString("DefaultConnection");
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Administrador", DateRegistration = DateTime.Now, IsActive = true},
                new Role { Id = 2, Name = "Colaborador", DateRegistration = DateTime.Now, IsActive = true}
            );
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    DateRegistration = System.DateTime.Now,
                    IsActive = true,
                    LastName = "",
                    Name = "Administrador",
                    Password = "123456",
                    UserName = "administrador",
                    RoleId = 1
                },
                new User
                {
                    Id = 2,
                    DateRegistration = System.DateTime.Now,
                    IsActive = true,
                    LastName = "",
                    Name = "Colaborador",
                    Password = "123456",
                    UserName = "colaborador",
                    RoleId = 2
                }
            );
        }
    }
}
