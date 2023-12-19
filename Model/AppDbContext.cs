using Microsoft.EntityFrameworkCore;

namespace EVA1_BackEnd.Model
{
    public class AppDbContext:DbContext
    {
        public DbSet<Categoria> TblCategorias { get; set; }
        public DbSet<Producto> TblProductos { get; set; }
        public DbSet<User> TblUsers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Eva1BackEnd;Integrated Security=True;");
        }
    }
}
