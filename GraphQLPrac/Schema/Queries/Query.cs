using GraphQLPrac.Services;
using Microsoft.EntityFrameworkCore;

namespace GraphQLPrac.Schema.Queries
{
    public class Query
    {
        #region Generate Fake data to enter in DB
        //private readonly Faker<InstructorType> _fakeInstructors;
        //private readonly Faker<StudentType> _fakeStudents;
        //private readonly Faker<CourseType> _fakeCourses; 
        #endregion

        private readonly SchoolDbContext _context;

        public Query(SchoolDbContext context)
        {
            #region Generate Fake data to enter in DB
            // Rules for fake generation of data

            //_fakeInstructors = new Faker<InstructorType>()
            //      .RuleFor(i => i.Id, i => Guid.NewGuid())
            //      .RuleFor(i => i.FirstName, f => f.Name.FirstName())
            //      .RuleFor(i => i.LastName, l => l.Name.LastName())
            //      .RuleFor(i => i.Salary, s => s.Random.Double(100, 1000));

            //_fakeStudents = new Faker<StudentType>()
            //   .RuleFor(s => s.Id, i => Guid.NewGuid())
            //   .RuleFor(s => s.FirstName, f => f.Name.FirstName())
            //   .RuleFor(s => s.LastName, l => l.Name.LastName())
            //   .RuleFor(s => s.GPA, g => g.Random.Double(0, 4));

            //_fakeCourses = new Faker<CourseType>()
            //   .RuleFor(c => c.Id, i => Guid.NewGuid())
            //   .RuleFor(c => c.Name, n => n.Name.JobTitle())
            //   .RuleFor(c => c.Subject, s => s.PickRandom<Subject>())
            //   .RuleFor(c => c.Instructor, i => _fakeInstructors.Generate())
            //   .RuleFor(c => c.Students, s => _fakeStudents.Generate(3)); 
            #endregion

            _context = context;
        }

        #region Generate Fake data to enter in DB
        //public IEnumerable<CourseType> Courses([Service] SchoolDbContext schoolDbContext)
        //{
        //    // Generate the fake courses
        //    List<CourseType> newCourses = _fakeCourses.Generate(5);

        //    // Add each generated course to the DbContext
        //    foreach (var course in newCourses)
        //    {
        //        // Create CourseDTO from the generated CourseType
        //        CourseDTO courseDTO = new CourseDTO()
        //        {
        //            Id = course.Id,
        //            Name = course.Name,
        //            Subject = course.Subject,
        //            Instructor = new InstructorDTO()
        //            {
        //                Id = course.Instructor.Id,
        //                FirstName = course.Instructor.FirstName,
        //                LastName = course.Instructor.LastName,
        //                Salary = course.Instructor.Salary
        //            },
        //            Students = course?.Students?.Select(student => new StudentDTO()
        //            {
        //                Id = student.Id,
        //                FirstName = student.FirstName,
        //                LastName = student.LastName,
        //                GPA = student.GPA
        //            }).ToList()
        //        };

        //        // Add the CourseDTO to the DbContext
        //        schoolDbContext.Courses.Add(courseDTO);
        //    } 

        // Save the changes to the database
        //schoolDbContext.SaveChanges();

        // Return the list of newly added courses
        //  return newCourses;
        // }
        #endregion

        // Fetching courses from the database
        public async Task<IEnumerable<CourseType>> Courses()
        {
            // Just for debugging: Check if _context is disposed
            if (_context == null) throw new Exception("DbContext is null");

            // Fetch all courses from the database
            var coursesFromDb = await _context.Courses
                .Include(c => c.Instructor)  // Include related instructor data
                .Include(c => c.Students)     // Include related students data
                .ToListAsync();

            // Map CourseDTOs to CourseTypes
            var courseTypes = coursesFromDb.Select(course => new CourseType
            {
                Id = course.Id,
                Name = course.Name,
                Subject = course.Subject,
                Instructor = new InstructorType
                {
                    Id = course.Instructor.Id,
                    FirstName = course.Instructor.FirstName,
                    LastName = course.Instructor.LastName,
                    Salary = course.Instructor.Salary
                },
                Students = course?.Students?.Select(student => new StudentType
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    GPA = student.GPA
                }).ToList()
            });

            return courseTypes;
        }

        // Get Course by ID
        public async Task<CourseType> GetCourseByIdAsync(Guid id)
        {
            var courseDTO = await _context.Courses
                .Include(c => c.Instructor)
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (courseDTO == null)
            {
                throw new GraphQLException(new Error($"Course not found for Id :{id}", "COURSE_NOT_FOUND")); // Or throw an exception based on your needs
            }
           
            return new CourseType
            {
                Id = courseDTO.Id,
                Name = courseDTO.Name,
                Subject = courseDTO.Subject,
                Instructor = new InstructorType
                {
                    Id = courseDTO.Instructor.Id,
                    FirstName = courseDTO.Instructor.FirstName,
                    LastName = courseDTO.Instructor.LastName,
                    Salary = courseDTO.Instructor.Salary
                },
                Students = courseDTO?.Students?.Select(student => new StudentType
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    GPA = student.GPA
                }).ToList()
            };
        }
    }

}

