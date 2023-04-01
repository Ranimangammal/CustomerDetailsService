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
		public string FirstName { get; }
		[Required]
		public string LastName { get; }

		[Required]
		[EmailAddress]
		public string Email { get; }

		[Range(1, 100)]
		public int Age { get; }

		public AddressModel CurrentAddress { get; }
	}
}
