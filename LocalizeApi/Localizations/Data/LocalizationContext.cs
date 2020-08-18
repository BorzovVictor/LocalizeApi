using LocalizeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LocalizeApi.Data
{
    public class LocalizationContext : DbContext
    {
        public LocalizationContext(DbContextOptions<LocalizationContext> options) 
            : base(options)
        {
        }
        public DbSet<Culture> Cultures { get; set; }
        public DbSet<Resource> Resources { get; set; }
    }
}