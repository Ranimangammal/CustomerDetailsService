using Microsoft.EntityFrameworkCore;

namespace CustomerDetailsService.Models.Data
{
	public class CustomerDbContext : DbContext
	{

		public DbSet<CustomerEntity> Customers { get; set; }

		public DbSet<AddressEntity> Addresses { get; set; }
	}
}
