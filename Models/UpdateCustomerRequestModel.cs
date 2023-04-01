using System.ComponentModel.DataAnnotations;

namespace CustomerDetailsService.Models
{
	[Serializable]
	public class UpdateCustomerRequestModel
	{
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }

		[Required]
		public int Age { get; set; }

		public AddressModel CurrentAddress { get; set; }
	}
}
