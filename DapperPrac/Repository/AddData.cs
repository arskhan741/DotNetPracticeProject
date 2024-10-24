using Bogus;
using DapperPrac.Models.DTOs;
using DapperPrac.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace DapperPrac.Repository
{
    /// <summary>
    /// Class for adding Fake Data with Bogus
    /// </summary>
    public class AddData : IAddData
    {
        private readonly ILogger<AddData> _logger;
        private readonly IEmployeeService _employeeService;

        public AddData(ILogger<AddData> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        public async Task AddFakeEmployee()
        {
            // Create a Faker instance for the CreateEmployeeDTO
            var employeeFaker = new Faker<CreateEmployeeDTO>()
                .RuleFor(e => e.Name, f => f.Name.FullName())
                .RuleFor(e => e.Age, f => f.Random.Int(18, 65)) // Age between 18 and 65
                .RuleFor(e => e.Address, f => f.Address.FullAddress())
                .RuleFor(e => e.MobileNumber, f => f.Phone.PhoneNumber());

            for (int i = 0; i < 10; i++)
            {
                // Generate a fake employee
                var fakeEmployee = employeeFaker.Generate();

                // Log the generated employee data
                _logger.LogInformation($"Generated employee {i + 1}: Name = {fakeEmployee.Name}, Age = {fakeEmployee.Age}, Mobile = {fakeEmployee.MobileNumber}");

                // Pass the DTO to the employee service
                await _employeeService.CreateEmployee(fakeEmployee);
            }

            _logger.LogInformation("Finished adding fake employees.");
        }

    }
}
