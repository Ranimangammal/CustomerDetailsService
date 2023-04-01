using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CustomerDetailsService.Models.Data;

namespace CustomerDetailsService.Models
{
	public class CustomerEntity
	{
		[Key]
		public int Id { get; set; }

		public string Email { get; set; }

		[StringLength(512)]
		public string FirstName { get; set; }

		[StringLength(512)]
		public string LastName { get; set; }

		public int Age { get; set; }

		public AddressEntity CurrentAddress { get; set; }

		public DateTime CreateDate { get; set; }

		public DateTime UpdateDate { get; set; }
	}
}
