using Microsoft.EntityFrameworkCore;
using VulnerableAPIProject.Entities.Base;

namespace VulnerableAPIProject.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Account> Account { get; set; }
        public DbSet<Profile> Profile { get; set; }


    }
}
