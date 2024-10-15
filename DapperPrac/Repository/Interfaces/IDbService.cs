using DapperPrac.Helper;
using System.Data;

namespace DapperPrac.Repository.Interfaces
{
    public interface IDbService
    {
        Task<Response> GetAsync(string selectCommand, ModelDetails details);
        Task<Response> GetAllAsync(string selectAllCommand, ModelDetails details);
        Task<Response> DeleteAsyncById(string deleteCommand, ModelDetails details);
        Task<Response> CreateAsync(string insertCommand, object? parms, ModelDetails details);
        Task<Response> UpdateAsync(string deleteCommand, object? parms, ModelDetails details);
        IDbConnection CreateConnection();
    }

}
