using GraphQLPrac.DTOs;
using GraphQLPrac.Services;
using Microsoft.EntityFrameworkCore;

namespace GraphQLPrac.Repositories
{
    public class CourseRepository
    {
        private readonly SchoolDbContext _context;

        public CourseRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<CourseDTO> AddCourse(CourseDTO courseDTO)
        {
            await _context.Courses.AddAsync(courseDTO);
            await _context.SaveChangesAsync();

            return courseDTO;
        }

        public async Task UpdateCourse(CourseDTO courseDTO)
        {
            _context.Courses.Update(courseDTO);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteCourse(Guid id)
        {
            CourseDTO courseDTO = new CourseDTO()
            {
                Id = id
            };

            _context.Courses.Remove(courseDTO);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<CourseDTO> GetCoursyById(Guid id)
        {
            return await _context.Courses
           .Where(i => i.Id == id)
           .FirstOrDefaultAsync()
           ?? throw new NullReferenceException($"Course for id: {id} not found");
        }


        public async Task<InstructorDTO> GetInstructorById(Guid id)
        {
            return await _context.Instructors
            .Where(i => i.Id == id)
            .FirstOrDefaultAsync()
            ?? throw new NullReferenceException($"Instructor for id: {id} not found");
        }

        public async Task<List<StudentDTO>> GetRandomStudents()
        {
            // Fetch all students from the database
            List<StudentDTO> students = await _context.Students.ToListAsync();

            // Randomize the list
            Random random = new Random();
            List<StudentDTO> randomizedStudents = students.OrderBy(s => random.Next()).ToList();

            // Return the first two students
            return randomizedStudents.Take(2).ToList();
        }

    }
}
