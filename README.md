# CustomerDetailsService 

** CustomerDetailsService allows users to add, read, store, and update customer details ** 

# Table of Contents 

Prerequisites 

Technologies 

Instructions 


# Prerequisites 

You will need the following tools: 

Visual Studio Code or Visual Studio 2022 (version 17.0.0 Preview 7.0 or later) 

.NET Core SDK 6.0 

# Technologies 

ASP.NET Core 6 

Entity Framework Core 7 

InMemoryDb 

AutoMapper 

XUnit, FluentAssertions, Moq & AutoFixture 

SWAGGER 

 

# Instructions 

Install the latest .NET Core 6 SDK. 

Clone this repository using link - https://github.com/Ranimangammal/CustomerDetailsService.git.

Build the solution using Visual Studio, or on the command line with dotnet build. 

Run the project. The API will start up on http://localhost:59203 with dotnet run. 

Use an HTTP client like Postman or Fiddler to GET http://localhost:59203. 

 

** Database Configuration 

The template is configured to use an in-memory database by default. This ensures that all users will be able to run the solution without needing to set up additional infrastructure (e.g. SQL Server). 

If you would like to use SQL Server, you will need to update WebApi/appsettings.json as follows: 

 "DbProvider": SqlServer 

dB Provider could be SQL Server by default, which could be extended to more database providers that EF Core supports. 

Verify that the DefaultConn connection string within appsettings.json points to a valid SQL Server instance. 

When you run the application, the database will be automatically created (if necessary) and the latest migrations will be applied. 

To setup the SQL Server database following the instructions below: 

Review the connection string in appsettings.Local.json and update the database name. 

Run dotnet ef migrations add Initial --context <ProjectName>DbContext to add migation with EF Core 

Run dotnet ef database update Initial to create application database. 

  ** Swagger UI

 ![image](https://user-images.githubusercontent.com/48356037/230333750-55efed57-5db7-4534-9761-8892e4d9b647.png)
 
  
** GET all Customers Api
 ![image](https://user-images.githubusercontent.com/48356037/229352473-f1b71f18-8ba1-4cae-919a-a936d6857cac.png)
 
  
** Get Customer By Id
  ![image](https://user-images.githubusercontent.com/48356037/230332875-3e6c4ab3-40be-4749-b903-75dc314173fa.png)
  
  
** Add new Customer
  ![image](https://user-images.githubusercontent.com/48356037/229352582-94586ee1-5dea-4627-a905-58c88129f9b2.png)
  
  
** Update Customer
  ![image](https://user-images.githubusercontent.com/48356037/229352624-d608ef3a-2390-40db-9268-dcb47e8e925f.png)
  
  
** Delete customer by id
  ![image](https://user-images.githubusercontent.com/48356037/229352684-5a4bcb21-50a0-4263-a94c-938054a95ae8.png)
  

** Search Customer by first name
![image](https://user-images.githubusercontent.com/48356037/230333127-2b93371c-70dd-4274-8a5e-a2f4a654c136.png)


** Search customer by last name
![image](https://user-images.githubusercontent.com/48356037/230333306-df58a659-d020-4272-bbad-2a5afede98e7.png)







