using System.ComponentModel.DataAnnotations;

namespace CustomerDetailsService.Models
{
	[Serializable]
	public class AddCustomerRequestModel
	{
		public AddCustomerRequestModel(
			string firstName,
			string lastName,
			string email,
			int age,
			AddressModel currentAddress)
		{
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			Age = age;
			CurrentAddress = currentAddress;
		}

		[Required]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "First Name field must have minimum 5!")]
		public string FirstName { get; }

		[Required]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Last Name field must have minimum 5!")]
		public string LastName { get; }

 		[EmailAddress]
		public string Email { get; }

		[Required]
		[Range(20, 50)]
		public int Age { get; }

		public AddressModel CurrentAddress { get; }
	}
}
