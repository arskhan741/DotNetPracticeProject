using DapperPrac.Models;
using Microsoft.EntityFrameworkCore;


namespace DapperPrac.Data
{
    public class ApplicationDBContext : DbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Products> Products { get; set; }

    }


}
