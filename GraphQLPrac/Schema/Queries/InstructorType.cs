namespace GraphQLPrac.Schema.Queries
{
    public class InstructorType
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public double Salary { get; set; }
    }
}
