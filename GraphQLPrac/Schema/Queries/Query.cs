using Bogus;
using GraphQLPrac.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace GraphQLPrac.Schema.Queries
{
    public class Query
    {
        private readonly Faker<InstructorType> _fakeInstructors;
        private readonly Faker<StudentType> _fakeStudents;
        private readonly Faker<CourseType> _fakeCourses;

        public Query()
        {
            // Rules for fake generation of data

            _fakeInstructors = new Faker<InstructorType>()
               .RuleFor(i => i.Id, i => Guid.NewGuid())
               .RuleFor(i => i.FirstName, f => f.Name.FirstName())
               .RuleFor(i => i.LastName, l => l.Name.LastName())
               .RuleFor(i => i.Salary, s => s.Random.Double(100, 1000));

            _fakeStudents = new Faker<StudentType>()
               .RuleFor(s => s.Id, i => Guid.NewGuid())
               .RuleFor(s => s.FirstName, f => f.Name.FirstName())
               .RuleFor(s => s.LastName, l => l.Name.LastName())
               .RuleFor(s => s.GPA, g => g.Random.Double(0, 4));

            _fakeCourses = new Faker<CourseType>()
               .RuleFor(c => c.Id, i => Guid.NewGuid())
               .RuleFor(c => c.Name, n => n.Name.JobTitle())
               .RuleFor(c => c.Subject, s => s.PickRandom<Subject>())
               .RuleFor(c => c.Instructor, i => _fakeInstructors.Generate())
               .RuleFor(c => c.Students, s => _fakeStudents.Generate(3));
        }

        public IEnumerable<CourseType> Courses() => _fakeCourses.Generate(5);

        // Get Parameters by ID

        public async Task<CourseType> GetCourseByIdAsync(Guid id)
        {
            await Task.Delay(1000);

            CourseType course = _fakeCourses.Generate();

            course.Id = id;

            return course;
        }

    }
}
