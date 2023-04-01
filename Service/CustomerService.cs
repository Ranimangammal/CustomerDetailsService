using AutoMapper;
using CustomerDetailsService.Models;
using CustomerDetailsService.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace CustomerDetailsService.Service
{
	public class CustomerService : ICustomerService
	{
		private readonly IMapper _mapper;
		private readonly ILogger<CustomerService> _logger;


		public CustomerService(
			IMapper mapper,
			ILogger<CustomerService> logger)
		{
			_mapper = mapper;
			_logger = logger;
		}

		public async Task<bool> AddCustomer(AddCustomerRequestModel input)
		{
			try
			{
				await using var context = new CustomerDbContext();
				var customerData = _mapper.Map<CustomerEntity>(input);
				var customerAddress = _mapper.Map<AddressEntity>(input.CurrentAddress);

				customerData.CreateDate = DateTime.UtcNow;
				customerData.UpdateDate = DateTime.UtcNow;
				customerData.CurrentAddress = customerAddress;
				await context.Customers.AddAsync(customerData);
				await context.SaveChangesAsync();

				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError("Unable to add Customer. exception" + ex);
				return false;
			}
		}

		public async Task<bool> DeleteCustomer(int id)
		{
			try
			{
				await using var context = new CustomerDbContext();
				var customer = await context.Customers.FirstOrDefaultAsync(c => c.Id == id);
				if (customer == null)
				{
					_logger.LogError("Unable to find Customer with Id:", id);
					return false;
				}

				context.Customers.Remove(customer);

				await context.SaveChangesAsync();
 
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError("Unable to delete Customer. exception" + ex);
				return false;
			}
		}

		public async Task<CustomersModel> GetAllCustomers()
		{
			await using var context = new CustomerDbContext();

			var customers = await context.Customers
			.AsNoTracking()
			.Include(c => c.CurrentAddress)
			.ToListAsync();

			var customerModels = _mapper.Map<IEnumerable<CustomerModel>>(customers);

			return new CustomersModel()
			{
				Customers = customerModels.ToArray()
			};
		}

		public async Task<CustomerModel> GetCustomerById(int id)
		{
			await using var context = new CustomerDbContext();
			var customer = await context.Customers
			.AsNoTracking()
			.Include(c => c.CurrentAddress)
			.FirstOrDefaultAsync(c => c.Id == id);
			if (customer == null)
			{
				_logger.LogError("Unable to find Customer with Id:", id);

				return new CustomerModel();
			}

			var customerDto = _mapper.Map<CustomerModel>(customer);

			return customerDto;
		}

		public async Task<bool> UpdateCustomer(int id, UpdateCustomerRequestModel input)
		{
			try
			{
				await using var context = new CustomerDbContext();
				var customer = await context.Customers
					.Include(c=>c.CurrentAddress)
					.FirstOrDefaultAsync(c => c.Id == id);
				if (customer == null)
				{
					_logger.LogError("Unable to find Customer with Id:", id);
					return false;
				}

				customer.FirstName = input.FirstName;
				customer.LastName = input.LastName;
				customer.Age = input.Age;
				customer.UpdateDate = DateTime.UtcNow;
				customer.CurrentAddress.StreetName = input.CurrentAddress?.StreetName;
				customer.CurrentAddress.HouseNumber = input.CurrentAddress?.HouseNumber;
				customer.CurrentAddress.City = input.CurrentAddress?.City;
				customer.CurrentAddress.Country = input.CurrentAddress?.Country;
				customer.CurrentAddress.PostalCode = input.CurrentAddress?.PostalCode;

				await context.SaveChangesAsync();
 
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError("Unable to update Customer. exception" + ex);
				return false;
			}
		}
	}
}
