namespace GraphQLPrac.Schema.Queries
{
    public class StudentType
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        [GraphQLName("gpa")]
        public double GPA { get; set; }
    }
}
