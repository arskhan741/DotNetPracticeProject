using GraphQLPrac.Schema.Mutations;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;

namespace GraphQLPrac.Schema.Subscription
{
    public class Subscription
    {
        [Subscribe]
        public CourseResult CourseCreated([EventMessage] CourseResult course) => course;

        [Subscribe]
        [Topic]
        public ValueTask<ISourceStream<CourseResult>> CourseUpdated(Guid courseId, [Service] ITopicEventReceiver topicEventReceiver)
        {
            string topicName = $"{courseId}_CourseUpdated";
            
            return topicEventReceiver.SubscribeAsync<CourseResult>(topicName);
        }

    }
}
