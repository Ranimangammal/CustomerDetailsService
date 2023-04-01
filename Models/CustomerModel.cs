namespace CustomerDetailsService.Models
{
	[Serializable]
	public class CustomerModel
	{
		public int Id { get; set; }

		public string Email { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public int Age { get; set; }

		public long StatusId { get; set; }

		public AddressModel CurrentAddress { get; set; }

		public DateTime CreateDate { get; set; }

		public DateTime UpdateDateTime { get; set; }
	}
}
