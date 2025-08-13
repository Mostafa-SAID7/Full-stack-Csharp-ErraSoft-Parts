using System;
using System.Linq;
using EFStudentSystem.Data;
using EFStudentSystem.Models;

namespace EFStudentSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            using var context = new StudentSystemContext();
            DbSeeder.Seed(context); // Optional: Seed only once

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Student System Menu ===");
                Console.WriteLine("1. List All Students");
                Console.WriteLine("2. Add New Course");
                Console.WriteLine("3. View Homework Submissions");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ListStudents(context);
                        break;
                    case "2":
                        AddCourse(context);
                        break;
                    case "3":
                        ViewHomework(context);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press Enter to continue...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void ListStudents(StudentSystemContext context)
        {
            Console.Clear();
            Console.WriteLine("Registered Students:");

            var students = context.Students.ToList();

            foreach (var student in students)
            {
                Console.WriteLine($"- {student.Name} (ID: {student.StudentId}) | Registered: {student.RegisteredOn:d}");
            }

            Console.WriteLine("\nPress Enter to return to the menu.");
            Console.ReadLine();
        }

        static void AddCourse(StudentSystemContext context)
        {
            Console.Clear();
            Console.Write("Course Name: ");
            string name = Console.ReadLine();

            Console.Write("Description (optional): ");
            string desc = Console.ReadLine();

            Console.Write("Start Date (yyyy-mm-dd): ");
            DateTime start = DateTime.Parse(Console.ReadLine());

            Console.Write("End Date (yyyy-mm-dd): ");
            DateTime end = DateTime.Parse(Console.ReadLine());

            Console.Write("Price: ");
            decimal price = decimal.Parse(Console.ReadLine());

            var course = new Course
            {
                Name = name,
                Description = string.IsNullOrWhiteSpace(desc) ? null : desc,
                StartDate = start,
                EndDate = end,
                Price = price
            };

            context.Courses.Add(course);
            context.SaveChanges();

            Console.WriteLine("Course added successfully. Press Enter to return to the menu.");
            Console.ReadLine();
        }

        static void ViewHomework(StudentSystemContext context)
        {
            Console.Clear();
            Console.WriteLine("Homework Submissions:");

            var homeworks = context.HomeworkSubmissions
                .Select(h => new
                {
                    h.Content,
                    h.ContentType,
                    h.SubmissionTime,
                    Student = h.Student.Name,
                    Course = h.Course.Name
                })
                .ToList();

            foreach (var hw in homeworks)
            {
                Console.WriteLine($"{hw.SubmissionTime:g} | {hw.Student} submitted '{hw.Content}' ({hw.ContentType}) for {hw.Course}");
            }

            Console.WriteLine("\nPress Enter to return to the menu.");
            Console.ReadLine();
        }
    }
}
