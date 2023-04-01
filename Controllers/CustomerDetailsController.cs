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

		// GET: api/<CustomerDetailsController>
		[HttpGet]
		public async Task<CustomersModel> Get()
		{
			return await _customerService.GetAllCustomers();
		}

		// GET api/<CustomerDetailsController>/5
		[HttpGet("{id}")]
		public async Task<CustomerModel> Get(int id)
		{
			if (id == default)
			{
				_logger.LogError("Provide value for customer id");
				return new CustomerModel();
			}

			var response = await _customerService.GetCustomerById(id);

			return response;
		}

		// POST api/<CustomerDetailsController>
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] AddCustomerRequestModel input)
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
		public async Task<IActionResult> Put(int id, [FromBody] UpdateCustomerRequestModel input)
		{

			if (id == default)
			{
				_logger.LogError("Provide value for customer id");
			}
			if(!ModelState.IsValid ) return BadRequest(ModelState);

			var result = await _customerService.UpdateCustomer(id, input);
			if (result)
			{
				return Ok();
			}

			return BadRequest();
		}

		// DELETE api/<CustomerDetailsController>/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
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
