using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Files.Models
{
    public class AppDbContext : DbContext
    {
        private IConfiguration _configuration;

        public DbSet<Archive> Archive { get; set; }
        public DbSet<Folder> Folder { get; set; }
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
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    DateRegistration = System.DateTime.Now,
                    IsActive = true,
                    LastName = "",
                    Name = "Usuario",
                    Password = "123456",
                    UserName = "usuario"

                }
            );
        }
    }
}
