namespace CustomerDetailsService.Models
{
	[Serializable]
	public class CustomersModel
	{
		public CustomersModel()
		{
			Customers = Array.Empty<CustomerModel>();
		}
		public CustomerModel[] Customers { get; set; }
	}
}
