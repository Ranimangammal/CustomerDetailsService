namespace CustomerDetailsService.Models
{
	[Serializable]
	public class AddressModel
	{
		public string StreetName { get; set; }

		public string HouseNumber { get; set; }

		public string City { get; set; }

		public string PostalCode { get; set; }

		public string Country { get; set; }
	}
}
