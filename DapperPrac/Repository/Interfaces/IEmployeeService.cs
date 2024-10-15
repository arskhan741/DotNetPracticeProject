using DapperPrac.Helper;
using DapperPrac.Models;
using DapperPrac.Models.DTOs;

namespace DapperPrac.Repository.Interfaces
{
    public interface IEmployeeService
    {
        Task<Response> CreateEmployee(CreateEmployeeDTO employee);
        Task<Response> GetAllEmployeeAsync();
        Task<Response> UpdateEmployee(Employee employee);
        Task<Response> DeleteEmployee(int id);
        Task<Response> GetEmployee(int id);
    }
}
