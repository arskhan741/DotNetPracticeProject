using Dapper;
using DapperPrac.Helper;
using DapperPrac.Models;
using DapperPrac.Repository.Interfaces;
using System.Data;
using System.Data.SqlClient;


namespace DapperPrac.Repository
{
    public class DbService : IDbService
    {
        private readonly IDbConnection _dbConnection;
        private readonly string _dbConnectionString = null!;

        public DbService(IConfiguration configuration)
        {
            string? dbConnectionString = configuration.GetConnectionString("SQLConnection")
                                ?? throw new NullReferenceException("Db connection string not found");

            _dbConnectionString = dbConnectionString;
            _dbConnection = new SqlConnection(_dbConnectionString);
        }

        public IDbConnection CreateConnection() => new SqlConnection(_dbConnectionString);


        public async Task<Response> CreateAsync(string insertCommand, object? parms, ModelDetails details)
        {
            try
            {
                await _dbConnection.QueryAsync(insertCommand, parms);

                string msg = string.Format(ResponseMessages.AddedSuccessfully, details.ModelName);

                return Response.ReturnResponse(true, msg, parms);
            }
            catch (Exception ex)
            {
                return Response.ReturnResponse(false, ex.Message);
            }
        }


        public async Task<Response> DeleteAsyncById(string deleteCommand, ModelDetails details)
        {
            try
            {
                await _dbConnection.QueryAsync(deleteCommand);

                return Response.ReturnResponse(true, string.Format(ResponseMessages.DeletedSuccessfully, details.ModelName, details.ModelId));
            }
            catch (Exception ex)
            {
                return Response.ReturnResponse(false, ex.Message);
            }
        }

        public async Task<Response> GetAllAsync(string selectAllCommand, ModelDetails details)
        {
            try
            {
                var results = await _dbConnection.QueryAsync<Employee>(selectAllCommand);

                string msg = string.Format(ResponseMessages.FoundSuccessfully, details.ModelName);

                // Return a response with the list of employees
                return Response.ReturnResponse(true, msg, results.ToList());
            }
            catch (Exception ex)
            {
                return Response.ReturnResponse(false, ex.Message);
            }
        }

        public async Task<Response> GetAsync(string selectCommand, ModelDetails details)
        {
            try
            {
                var result = await _dbConnection.QueryFirstOrDefaultAsync(selectCommand);

                // Create a custom message including the model name
                string message = string.Format(ResponseMessages.FoundSuccessfully, details.ModelName);

                if(result is null)
                {
                    string msg = string.Format(ResponseMessages.ModelNotFound, details.ModelName, details.ModelId);
                    throw new(msg);
                }

                return Response.ReturnResponse(true, message, result);
            }
            catch (Exception ex)
            {
                return Response.ReturnResponse(false, ex.Message);
            }
        }

        public async Task<Response> UpdateAsync(string updateEmployees, object? parms, ModelDetails details)
        {
            try
            {
                // Execute the update query and get the number of affected rows
                var affectedRows = await _dbConnection.ExecuteAsync(updateEmployees, parms);

                // Check if no rows were affected (employee not found)
                if (affectedRows == 0)
                {
                    string msg = string.Format(ResponseMessages.ModelNotFound, details.ModelName, details.ModelId);

                    throw new Exception(msg);
                }

                string msg2 = string.Format(ResponseMessages.UpdatedSuccessfully, details.ModelName, details.ModelId);

                return Response.ReturnResponse(true, msg2, parms);
            }
            catch (Exception ex)
            {
                // Return a failure response with the exception message
                return Response.ReturnResponse(false, ex.Message);
            }
        }
    }
}


public class ModelDetails
{
    public string? ModelName { get; set; }
    public int? ModelId { get; set; }
}
