namespace GraphQLPrac.Schema.Mutations
{
    public class Mutation
    {
        private readonly List<CourseResult> _courses;

        public Mutation()
        {
            _courses = new List<CourseResult>();
        }

        public CourseResult CreateCourse(CourseInput courseInput)
        {
            CourseResult courseResult = new CourseResult()
            {
                Id = Guid.NewGuid(),
                Name = courseInput.Name,
                Subject = courseInput.Subject,
                InstructorId = courseInput.InstructorId
            };

            _courses.Add(courseResult);

            return courseResult;
        }

        public CourseResult UpdateCourse(Guid id, CourseInput courseInput)
        {
            CourseResult? courseResult = _courses.FirstOrDefault(c => (c.Id == id));

            if (courseResult is null)
            {
                throw new GraphQLException(new Error($"No course found for Id : {id}", "COURSE_NOT_FOUND"));
                //throw new NullReferenceException("Course not found");
            }

            courseResult.Name = courseInput.Name;
            courseResult.Subject = courseInput.Subject;
            courseResult.InstructorId = courseInput.InstructorId;

            return courseResult;
        }

        public bool DeleteCourse(Guid id) => _courses.RemoveAll(c => c.Id == id) >= 1;





    }
}
