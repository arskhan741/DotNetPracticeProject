﻿using GraphQLPrac.Schema.Queries;

namespace GraphQLPrac.Schema.Mutations
{
    public class CourseResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Subject Subject { get; set; }
        public Guid InstructorId { get; set; }

    }
}