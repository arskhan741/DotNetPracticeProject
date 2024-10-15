using DapperPrac.Helper;
using DapperPrac.Models;
using DapperPrac.Models.DTOs;
using DapperPrac.Repository.Interfaces;

namespace DapperPrac.Repository
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IDbService _dbService;
        private ModelDetails _modelDetails;

        public EmployeeService(IDbService dbService)
        {
            _dbService = dbService;

            _modelDetails = new ModelDetails()
            {
                ModelName = "Employee",
                ModelId = 0
            };
        }

        public async Task<Response> CreateEmployee(CreateEmployeeDTO employeeDto)
        {
            Response response = await _dbService.CreateAsync(StoredProcedures.InsertEmployee, employeeDto, _modelDetails);
            return response;
        }

        public async Task<Response> DeleteEmployee(int id)
        {
            _modelDetails.ModelId = id;

            Response response = await _dbService.DeleteAsyncById(String.Format(StoredProcedures.DeleteEmployeeById, id), _modelDetails);
            return response;
        }

        public async Task<Response> GetEmployee(int id)
        {
            _modelDetails.ModelId = id;

            Response response = await _dbService.GetAsync(string.Format(StoredProcedures.SelectEmployeeById, id), _modelDetails);
            return response;
        }

        public async Task<Response> GetAllEmployeeAsync()
        {
            var response = await _dbService.GetAllAsync(StoredProcedures.SelectAllEmployees, _modelDetails);
            return response;
        }

        public async Task<Response> UpdateEmployee(Employee employee)
        {
            _modelDetails.ModelId = employee.Id;

            Response response = await _dbService.UpdateAsync(StoredProcedures.UpdateEmployees, employee, _modelDetails);
            return response;
        }
    }
}
