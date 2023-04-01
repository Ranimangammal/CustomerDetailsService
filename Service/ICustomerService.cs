using CustomerDetailsService.Models;

namespace CustomerDetailsService.Service
{
	public interface ICustomerService
	{
		Task<bool> AddCustomer(AddCustomerRequestModel input);

		Task<CustomerModel> GetCustomerById(int id);

		Task<CustomersModel> GetAllCustomers();

		Task<bool> UpdateCustomer(int id, UpdateCustomerRequestModel input);

		Task<bool> DeleteCustomer(int id);
	}
}
