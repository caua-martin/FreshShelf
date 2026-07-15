using FreshShelf.Models;
using Microsoft.EntityFrameworkCore;

namespace FreshShelf.Data; 

public class ProductContext : DbContext
{
	public ProductContext(DbContextOptions<ProductContext> opts)
		: base(opts)
	{
					  
	}

    public DbSet<Product> Products { get; set; } 
}
