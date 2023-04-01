using AutoMapper;
using CustomerDetailsService.Models;
using CustomerDetailsService.Models.Data;

namespace CustomerService
{
	public class MappingConfiguration : Profile
	{
		public MappingConfiguration()
		{
			CreateMap<CustomerEntity, AddCustomerRequestModel>().ReverseMap();
			CreateMap<CustomerEntity, UpdateCustomerRequestModel>().ReverseMap();
			CreateMap<CustomerEntity, CustomerModel>().ReverseMap();
			CreateMap<AddressEntity, AddressModel>().ReverseMap();
		}
	}
}