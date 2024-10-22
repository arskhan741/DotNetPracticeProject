namespace GraphQLPrac.DTOs
{
    public class StudentDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public double GPA { get; set; }
        public IEnumerable<CourseDTO>? Courses { get; set; }
    }
}
