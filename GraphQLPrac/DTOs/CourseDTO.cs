
using GraphQLPrac.Models;

namespace GraphQLPrac.DTOs
{
    public class CourseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Subject Subject { get; set; }

        public InstructorDTO Instructor { get; set; } = null!;

        public IEnumerable<StudentDTO>? Students { get; set; }

    }
}

