using DapperPrac.Helper;
using DapperPrac.Models;
using DapperPrac.Models.DTOs;
using DapperPrac.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DapperPrac.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<Response> AddEmployee([FromBody] CreateEmployeeDTO employee)
        {
            var result = await _employeeService.CreateEmployee(employee);

            return result;
        }

        [HttpDelete]
        public async Task<Response> DeleteEmployee(int id)
        {
            var result = await _employeeService.DeleteEmployee(id);

            return result;
        }

        [HttpGet("Get")]
        public async Task<Response> GetEmployee(int id)
        {
            var result = await _employeeService.GetEmployee(id);

            return result;
        }

        [HttpGet("Get All")]
        public async Task<Response> GetAllEmployee()
        {
            var result = await _employeeService.GetAllEmployeeAsync();

            return result;
        }

        [HttpPut]
        public async Task<Response> UpdateEmployee([FromBody] Employee employee)
        {
            var result = await _employeeService.UpdateEmployee(employee);

            return result;
        }

    }
}
