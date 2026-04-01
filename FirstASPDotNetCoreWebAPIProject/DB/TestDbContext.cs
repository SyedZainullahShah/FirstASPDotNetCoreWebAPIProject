using FirstASPDotNetCoreWebAPIProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstASPDotNetCoreWebAPIProject.DB
{
    public class TestDbContext: DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options): base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
