﻿namespace GraphQLPrac.DTOs
{
    public class InstructorDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public double Salary { get; set; }
        public IEnumerable<CourseDTO>? Courses { get; set; }

    }
}
