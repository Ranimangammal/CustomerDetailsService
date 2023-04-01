using Microsoft.EntityFrameworkCore;

namespace CustomerDetailsService.Models.Data
{
	public class CustomerDbContext : DbContext
	{
		public CustomerDbContext(){}
		public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
			: base(options)
		{ }
		protected override void OnConfiguring
			(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseInMemoryDatabase(databaseName: "CustomersDb");
		}
		public DbSet<CustomerEntity> Customers { get; set; }

		public DbSet<AddressEntity> Addresses { get; set; }
	}
}
