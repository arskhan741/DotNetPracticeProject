namespace DapperPrac.Helper
{
    public static class StoredProcedures
    {
        public static string InsertEmployee =
            "INSERT INTO dbo.Employees(name, age, address, MobileNumber) " +
            "VALUES(@Name, @Age, @Address, @MobileNumber)";

        public static string DeleteEmployeeById = "DELETE FROM dbo.Employees WHERE Id = {0}";

        public static string SelectEmployeeById = "SELECT * FROM dbo.Employees WHERE Id = {0}";

        public static string SelectEmployeeByName = "SELECT * FROM dbo.Employees WHERE Name = '{0}'";

        public static string SelectAllEmployees = "SELECT * FROM dbo.Employees";

        public static string UpdateEmployees =
           "UPDATE dbo.Employees " +
            "SET Name = @Name, " +
            "Age = @Age, " +
            "Address = @Address, " +
            "MobileNumber = @MobileNumber " +
            "WHERE Id = @Id";

    }
}
