namespace GraphQLPrac.Schema.Queries
{
    public class CourseType
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Subject Subject { get; set; }
        [GraphQLNonNullType]
        public InstructorType Instructor { get; set; } = null!;
        public IEnumerable<StudentType>? Students { get; set; }

        public string Description() => $"{Name} , I am a Course good to meet u";
    }

    public enum Subject
    {
        Mathematics,
        Science,
        History
    }
}
