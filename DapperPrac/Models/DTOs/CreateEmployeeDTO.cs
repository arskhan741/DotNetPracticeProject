namespace DapperPrac.Models.DTOs
{
    public class CreateEmployeeDTO
    {
        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        public string Address { get; set; } = string.Empty;

        public string MobileNumber { get; set; } = string.Empty;
    }
}
