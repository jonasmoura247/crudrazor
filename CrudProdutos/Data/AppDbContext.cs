// Data/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using CrudProdutos.Models;

namespace CrudProdutos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
    }
}