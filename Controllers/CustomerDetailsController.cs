using CustomerDetailsService.Models;
using CustomerDetailsService.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerDetailsService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomerDetailsController : ControllerBase
	{
		private readonly ICustomerService _customerService;
		private readonly ILogger<CustomerDetailsController> _logger;

		public CustomerDetailsController(
			ICustomerService customerService
			, ILogger<CustomerDetailsController> logger)
		{
			_customerService = customerService;
			_logger = logger;
		}

		// GET: api/<CustomerDetailsController>/GetAllCustomers
		[HttpGet]
		public async Task<CustomersModel> GetAllCustomers()
		{
			return await _customerService.GetAllCustomers();
		}

		// GET api/<CustomerDetailsController>/GetCustomerById/5
		[HttpGet("GetCustomerById/{id}", Name = "GetCustomerById")]
		public async Task<CustomerModel> GetCustomerById(int id)
		{
			if (id == default)
			{
				_logger.LogError("Provide value for customer id");
				return new CustomerModel();
			}

			var response = await _customerService.GetCustomerById(id);

			return response;
		}

		[HttpGet("SearchCustomerByFirstName/{firstName}", Name = "SearchCustomerByFirstName")]
		public async Task<CustomerModel> SearchCustomerByFirstName(string firstName)
		{
			if (string.IsNullOrEmpty(firstName))
			{
				_logger.LogError("Provide value for first name");
				return new CustomerModel();
			}

			var response = await _customerService.SearchCustomerByFirstName(firstName);

			return response;
		}

		[HttpGet("SearchCustomerByLastName/{LastName}", Name = "SearchCustomerByLastName")]
		public async Task<CustomerModel> SearchCustomerByLastName(string lastName)
		{
			if (string.IsNullOrEmpty(lastName))
			{
				_logger.LogError("Provide value for last name");
				return new CustomerModel();
			}

			var response = await _customerService.SearchCustomerByLastName(lastName);

			return response;
		}

		// POST api/<CustomerDetailsController>
		[HttpPost]
		public async Task<IActionResult> AddNewCustomer([FromBody] AddCustomerRequestModel input)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var result = await _customerService.AddCustomer(input);
			if (result)
			{
				return Ok();
			}

			return BadRequest();
		}

		// PUT api/<CustomerDetailsController>/5
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCustomer(int id, [FromBody] UpdateCustomerRequestModel input)
		{

			if (id == default)
			{
				_logger.LogError("Provide value for customer id");
				return BadRequest(ModelState);
			}
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var result = await _customerService.UpdateCustomer(id, input);
			if (result)
			{
				return Ok();
			}

			return BadRequest();
		}

		// DELETE api/<CustomerDetailsController>/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCustomer(int id)
		{

			if (id == default)
			{
				_logger.LogError("Provide value for customer id");
				return new NotFoundResult();
			}

			var result = await _customerService.DeleteCustomer(id);
			if (result)
			{
				return Ok();
			}
			return BadRequest();
		}
	}
}
