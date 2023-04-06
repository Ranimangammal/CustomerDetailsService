using System.ComponentModel.DataAnnotations;

namespace CustomerDetailsService.Models
{
	[Serializable]
	public class UpdateCustomerRequestModel
	{
		[Required]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "First Name field must have minimum 5!")]
		public string FirstName { get; set; }

		[Required]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Last Name field must have minimum 5!")]
		public string LastName { get; set; }

		[Required]
		[Range(20, 50)]
		public int Age { get; set; }

		public AddressModel CurrentAddress { get; set; }
	}
}
