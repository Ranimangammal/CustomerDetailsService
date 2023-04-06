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

				var existingCustomer = await
					context.Customers
					.Include(c => c.CurrentAddress)
					.FirstOrDefaultAsync(
						i => string.Equals(i.FirstName, input.FirstName, StringComparison.CurrentCultureIgnoreCase) &&
							string.Equals(i.LastName, input.LastName, StringComparison.CurrentCultureIgnoreCase));

				if (existingCustomer != default)
				{
					_logger.LogInformation("Customer already exists");

					existingCustomer.FirstName = (!string.IsNullOrEmpty(input?.FirstName) ? input?.FirstName : existingCustomer.FirstName) ?? string.Empty;
					existingCustomer.LastName = (!string.IsNullOrEmpty(input?.LastName) ? input?.LastName : existingCustomer.LastName) ?? string.Empty;
					existingCustomer.Age = (input?.Age > 0? input?.Age : existingCustomer.Age) ?? default;
					existingCustomer.UpdateDate = DateTime.UtcNow;
					existingCustomer.CurrentAddress.StreetName = (!string.IsNullOrEmpty(input.CurrentAddress?.StreetName) ? input.CurrentAddress?.StreetName : existingCustomer.CurrentAddress?.StreetName) ?? string.Empty;
					if (existingCustomer.CurrentAddress != null)
					{
						existingCustomer.CurrentAddress.HouseNumber =
							(!string.IsNullOrEmpty(input.CurrentAddress?.HouseNumber)
								? input.CurrentAddress?.HouseNumber
								: existingCustomer.CurrentAddress?.HouseNumber)
						?? string.Empty;
						existingCustomer.CurrentAddress.City =
							(!string.IsNullOrEmpty(input.CurrentAddress?.City)
								? input.CurrentAddress?.City
								: existingCustomer.CurrentAddress?.City)
						?? string.Empty;
						existingCustomer.CurrentAddress.Country = (!string.IsNullOrEmpty(input.CurrentAddress?.Country)
								? input.CurrentAddress?.Country
								: existingCustomer.CurrentAddress?.Country)
						?? string.Empty;
						existingCustomer.CurrentAddress.PostalCode =
							(!string.IsNullOrEmpty(input.CurrentAddress?.PostalCode)
								? input.CurrentAddress?.PostalCode
								: existingCustomer.CurrentAddress?.PostalCode)
						?? string.Empty;
					}

					await context.SaveChangesAsync();

					return true;
				}

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

		public async Task<CustomerModel> SearchCustomerByFirstName(string firstName)
		{
			await using var context = new CustomerDbContext();
			var customer = await context.Customers
				.AsNoTracking()
				.Include(c => c.CurrentAddress)
				.FirstOrDefaultAsync(c => String.Equals(c.FirstName, firstName, StringComparison.CurrentCultureIgnoreCase));
			if (customer == null)
			{
				_logger.LogError("Unable to find Customer with first name:", firstName);

				return new CustomerModel();
			}

			var customerDto = _mapper.Map<CustomerModel>(customer);

			return customerDto;
		}

		public async Task<CustomerModel> SearchCustomerByLastName(string lastName)
		{
			await using var context = new CustomerDbContext();
			var customer = await context.Customers
				.AsNoTracking()
				.Include(c => c.CurrentAddress)
				.FirstOrDefaultAsync(c => string.Equals(c.LastName, lastName, StringComparison.CurrentCultureIgnoreCase));
			if (customer == null)
			{
				_logger.LogError("Unable to find Customer with last name:", lastName);

				return new CustomerModel();
			}

			var customerDto = _mapper.Map<CustomerModel>(customer);

			return customerDto;
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
				var existingCustomer = await context.Customers
				.Include(c => c.CurrentAddress)
				.FirstOrDefaultAsync(c => c.Id == id);
				if (existingCustomer == null)
				{
					_logger.LogError("Unable to find Customer with Id:", id);
					return false;
				}

				existingCustomer.FirstName =
					(!string.IsNullOrEmpty(input?.FirstName) ? input?.FirstName : existingCustomer.FirstName)
				?? string.Empty;
				existingCustomer.LastName =
					(!string.IsNullOrEmpty(input?.LastName) ? input?.LastName : existingCustomer.LastName)
				?? string.Empty;
				existingCustomer.Age = (input?.Age > 0 ? input?.Age : existingCustomer.Age) ?? default;
				existingCustomer.UpdateDate = DateTime.UtcNow;
				existingCustomer.CurrentAddress.StreetName = (!string.IsNullOrEmpty(input.CurrentAddress?.StreetName)
						? input.CurrentAddress?.StreetName
						: existingCustomer.CurrentAddress?.StreetName)
				?? string.Empty;
 
					existingCustomer.CurrentAddress.HouseNumber =
						(!string.IsNullOrEmpty(input.CurrentAddress?.HouseNumber)
							? input.CurrentAddress?.HouseNumber
							: existingCustomer.CurrentAddress?.HouseNumber)
					?? string.Empty;
					existingCustomer.CurrentAddress.City =
						(!string.IsNullOrEmpty(input.CurrentAddress?.City)
							? input.CurrentAddress?.City
							: existingCustomer.CurrentAddress?.City)
					?? string.Empty;
					existingCustomer.CurrentAddress.Country = (!string.IsNullOrEmpty(input.CurrentAddress?.Country)
							? input.CurrentAddress?.Country
							: existingCustomer.CurrentAddress?.Country)
					?? string.Empty;
					existingCustomer.CurrentAddress.PostalCode =
						(!string.IsNullOrEmpty(input.CurrentAddress?.PostalCode)
							? input.CurrentAddress?.PostalCode
							: existingCustomer.CurrentAddress?.PostalCode)
					?? string.Empty;

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
