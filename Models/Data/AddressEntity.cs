using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerDetailsService.Models.Data
{
	public class AddressEntity
	{
		[Key]
		public int Id { get; set; }

		[MaxLength(256)]
		[Column(TypeName = "varchar(256)")]
		public string StreetName { get; set; }

		[MaxLength(64)]
		[Column(TypeName = "varchar(64)")]
		public string HouseNumber { get; set; }

		[MaxLength(512)]
		[Column(TypeName = "varchar(512)")]
		public string City { get; set; }

		[MaxLength(64)]
		[Column(TypeName = "varchar(64)")]
		public string PostalCode { get; set; }

		[MaxLength(512)]
		[Column(TypeName = "varchar(512)")]
		public string Country { get; set; }
	}
}
