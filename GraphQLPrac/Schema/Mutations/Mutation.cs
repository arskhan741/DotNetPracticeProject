using HotChocolate.Subscriptions;
using GraphQLPrac.Schema.Subscription;

namespace GraphQLPrac.Schema.Mutations
{
    public class Mutation
    {
        private readonly List<CourseResult> _courses;

        public Mutation()
        {
            _courses = new List<CourseResult>();
        }

        public async Task<CourseResult> CreateCourse(CourseInput courseInput, [Service] ITopicEventSender topicEventSender)
        {
            CourseResult course = new CourseResult()
            {
                Id = Guid.NewGuid(),
                Name = courseInput.Name,
                Subject = courseInput.Subject,
                InstructorId = courseInput.InstructorId
            };

            _courses.Add(course);

            // Call an subscribed event
            await topicEventSender.SendAsync(nameof(Subscription.Subscription.CourseCreated), course);

            return course;
        }

        public async Task<CourseResult> UpdateCourse(Guid id, CourseInput courseInput, [Service] ITopicEventSender topicEventSender)
        {
            CourseResult? course = _courses.FirstOrDefault(c => (c.Id == id));

            if (course is null)
            {
                throw new GraphQLException(new Error($"No course found for Id : {id}", "COURSE_NOT_FOUND"));
                //throw new NullReferenceException("Course not found");
            }

            course.Name = courseInput.Name;
            course.Subject = courseInput.Subject;
            course.InstructorId = courseInput.InstructorId;

            string updatedCourseTopic = $"{id}_CourseUpdated";
            await topicEventSender.SendAsync(updatedCourseTopic, id);

            return course;
        }

        public bool DeleteCourse(Guid id) => _courses.RemoveAll(c => c.Id == id) >= 1;

    }
}
