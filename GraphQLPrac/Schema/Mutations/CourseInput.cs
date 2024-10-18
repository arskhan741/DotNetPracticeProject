using GraphQLPrac.Models;
using GraphQLPrac.Schema.Queries;

namespace GraphQLPrac.Schema.Mutations
{
    public class CourseInput
    {
        public string Name { get; set; } = null!;
        public Subject Subject { get; set; }
        public Guid InstructorId { get; set; }
    }
}
