using AutoMapper;
using CustomerDetailsService;
using CustomerDetailsService.Models.Data;
using CustomerDetailsService.Service;
using CustomerService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// use Inmemorydb
builder.Services.AddDbContext<CustomerDbContext>(c => c.UseInMemoryDatabase("CustomersDb"));

////To Use Sqlserver
//var connectionstring = builder.Configuration.GetConnectionString("DefaultConn");
//if (!string.IsNullOrEmpty(connectionstring))
//{
//	AppSettings.ConnectionString = connectionstring;
//}
//builder.Services.AddDbContext<CustomerDbContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConn")));
// register service
builder.Services.AddScoped<ICustomerService,CustomerDetailsService.Service.CustomerService>();

//register IMapper
var mapperConfig = new MapperConfiguration(mc =>
{
	mc.AddProfile(new MappingConfiguration());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
