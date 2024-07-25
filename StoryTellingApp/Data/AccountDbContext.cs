using Microsoft.EntityFrameworkCore;
using StoryTellingApp.Data.Entity;

namespace StoryTellingApp.Data
{
    public class AccountDbContext : DbContext
    {
        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior =
            QueryTrackingBehavior.NoTracking;
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Users> Users { get; set; }
    }
}
