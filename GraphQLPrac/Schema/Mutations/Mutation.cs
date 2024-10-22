using HotChocolate.Subscriptions;
using GraphQLPrac.Schema.Subscription;
using GraphQLPrac.Services;
using GraphQLPrac.Repositories;
using GraphQLPrac.DTOs;

namespace GraphQLPrac.Schema.Mutations
{
    public class Mutation
    {
        private readonly CourseRepository _courseRepository;

        public Mutation(CourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<CourseResult> CreateCourse(CourseInput courseInput, [Service] ITopicEventSender topicEventSender)
        {
            CourseResult courseResult = new CourseResult()
            {
                Id = Guid.NewGuid(),
                Name = courseInput.Name,
                Subject = courseInput.Subject,
                InstructorId = courseInput.InstructorId
            };

            await _courseRepository.AddCourse(new CourseDTO()
            {
                Id = courseResult.Id,
                Name = courseInput.Name,
                Subject = courseInput.Subject,
                Instructor = await _courseRepository.GetInstructorById(courseResult.InstructorId),
                Students = await _courseRepository.GetRandomStudents()
            });

            // Call an subscribed event
            await topicEventSender.SendAsync(nameof(Subscription.Subscription.CourseCreated), courseResult);

            return courseResult;
        }

        public async Task<CourseResult> UpdateCourse(Guid id, CourseInput courseInput, [Service] ITopicEventSender topicEventSender)
        {
            CourseDTO courseDTO = await _courseRepository.GetCoursyById(id);
            await _courseRepository.UpdateCourse(courseDTO);

            CourseResult? course = new CourseResult()
            {
                Id = courseDTO.Id,
                Name = courseDTO.Name,
                Subject = courseDTO.Subject,
                InstructorId = courseInput.InstructorId
            };

            if (course is null)
            {
                throw new GraphQLException(new Error($"No course found for Id : {id}", "COURSE_NOT_FOUND"));
                //throw new NullReferenceException("Course not found");
            }

            course.Name = courseInput.Name;
            course.Subject = courseInput.Subject;
            course.InstructorId = courseInput.InstructorId;

            //string updatedCourseTopic = $"{id}_CourseUpdated";
            //await topicEventSender.SendAsync(updatedCourseTopic, id);

            await Task.Delay(1);

            return course;
        }

        //public bool DeleteCourse(Guid id) => 

    }
}

