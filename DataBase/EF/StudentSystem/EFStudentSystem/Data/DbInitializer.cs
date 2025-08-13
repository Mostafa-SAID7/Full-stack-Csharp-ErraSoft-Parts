// Data/DbInitializer.cs
using EFStudentSystem.Data;
using EFStudentSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFStudentSystem.Data
{
    public static class DbInitializer
    {
        public static void Seed(StudentSystemContext context)
        {
            if (context.Students.Any()) return; // Prevent duplicate seeding

            var students = new List<Student>
            {
                new Student { Name = "Ali Hassan", RegisteredOn = DateTime.Now.AddDays(-30), Birthday = new DateTime(2000, 5, 20), PhoneNumber = "0123456789" },
                new Student { Name = "Sara Youssef", RegisteredOn = DateTime.Now.AddDays(-20), Birthday = new DateTime(2001, 8, 15) },
                new Student { Name = "Omar Adel", RegisteredOn = DateTime.Now.AddDays(-10), PhoneNumber = "0987654321" }
            };

            var courses = new List<Course>
            {
                new Course { Name = "C# Fundamentals", StartDate = DateTime.Now.AddDays(-40), EndDate = DateTime.Now.AddDays(40), Price = 199.99m },
                new Course { Name = "EF Core Deep Dive", StartDate = DateTime.Now.AddDays(-20), EndDate = DateTime.Now.AddDays(60), Price = 249.99m }
            };

            var resources = new List<Resource>
            {
                new Resource { Name = "Intro Video", Url = "http://video.com/intro", ResourceType = ResourceType.Video, Course = courses[0] },
                new Resource { Name = "EF Slides", Url = "http://docs.com/efslides", ResourceType = ResourceType.Presentation, Course = courses[1] }
            };

            var homeworks = new List<Homework>
            {
                new Homework { Content = "http://files.com/hw1.zip", ContentType = ContentType.Zip, SubmissionTime = DateTime.Now, Student = students[0], Course = courses[0] },
                new Homework { Content = "http://files.com/hw2.pdf", ContentType = ContentType.Pdf, SubmissionTime = DateTime.Now, Student = students[1], Course = courses[1] }
            };

            var studentCourses = new List<StudentCourse>
            {
                new StudentCourse { Student = students[0], Course = courses[0] },
                new StudentCourse { Student = students[1], Course = courses[1] },
                new StudentCourse { Student = students[2], Course = courses[0] }
            };

            context.Students.AddRange(students);
            context.Courses.AddRange(courses);
            context.Resources.AddRange(resources);
            context.Homeworks.AddRange(homeworks);
            context.StudentCourses.AddRange(studentCourses);

            context.SaveChanges();
        }
    }
}
