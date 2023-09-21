using Microsoft.EntityFrameworkCore;
using One_To_Many_RawSqL.Models;

namespace One_To_Many_RawSqL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Products> Products { get; set; }
        public DbSet<Sizes> Sizes { get; set; }

    }
}
