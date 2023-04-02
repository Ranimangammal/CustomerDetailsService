using Bogus;
using Microsoft.EntityFrameworkCore;

namespace CustomerDetailsService.Models.Data
{
	public class CustomerDbContext : DbContext
	{
		public CustomerDbContext() { }

		public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
			: base(options)
		{
			this.Database.EnsureCreated();
		}

		//public CustomerDbContext CreateDbContext(string[] args)
		//{
		//	//To use Sqlserver uncomment this section
		//	var optionsBuilder = new DbContextOptionsBuilder<CustomerDbContext>();
		//	optionsBuilder.UseSqlServer(AppSettings.ConnectionString);

		//	return new CustomerDbContext(optionsBuilder.Options);
		//}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseInMemoryDatabase(databaseName: "CustomersDb");

			//if (!optionsBuilder.IsConfigured)
			//{
			//	optionsBuilder.UseSqlServer(AppSettings.ConnectionString);
			//}
		}
 		public DbSet<CustomerEntity> Customers { get; set; }

		public DbSet<AddressEntity> Addresses { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
 		}
	}

}
