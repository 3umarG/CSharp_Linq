using System.Text.Json;

internal partial class Program
{
    public static async Task Main(string[] args)
    {
        var studentsFile = await File.ReadAllTextAsync("students.json");
        var teachersFile = await File.ReadAllTextAsync("teachers.json");


        var students = JsonSerializer.Deserialize<List<Student>>(studentsFile);
        var teachers = JsonSerializer.Deserialize<List<Teacher>>(teachersFile);
        /*
        students.ForEach(s => Console.WriteLine(s.City));
        teachers.ForEach(s => Console.WriteLine(s.City));
        */

        // One to One :  Make a list that match and join the students with their teachers with same id
        /*
         var studentsWithTheirTeachers = from s in students
                                         join t in teachers! on s.ID equals t.ID
                                         select new { Student = s.Name, Teacher = t.Name, t.ID };
         foreach (var s in studentsWithTheirTeachers.ToList())
         {
             await
                 Console.Out.WriteLineAsync(
                     $"Student : {s.Student} has match with Teacher : {s.Teacher} in : {s.ID}");
         }
        */


        // Group Join -One to Many - : Group all males and females students
        /*
         var malesAndFemals = from g in new List<string> { "Male", "Female" }
                             join student in students! on g equals student.Gender into studentsGroupedByGender
                             select new { Gender = g, Students = studentsGroupedByGender.ToList().Where(s => s.Grade >= 75) };

        malesAndFemals
            .ToList()
            .ForEach(
            gender => Console.WriteLine($"Gender {gender.Gender} has {gender.Students.Count()}")
            );
        */


        // Left Outer Join : use DefaultIfEmpty()
        // Male : .....
        // Femal: .....
        // Undefined : "Any Empty Data" بحيث تكون بديلة لل null values as the Table 
        var malesAndFemales = from g in new List<string> { "Male", "Female", "Not Defined" }
                             join student in students! on g equals student.Gender into studentsGroupedByGender
                             // the addition part for check there is no match in the right side :
                             from matchedStudent in studentsGroupedByGender.DefaultIfEmpty(new Student { Name = "Undefined" })
                             where matchedStudent.Grade >= 75
                             select new { Gender = g, Student = matchedStudent };
        malesAndFemales
            .ToList()
            .ForEach(
            gender => Console.WriteLine($"{gender.Gender} has Student : {gender.Student.Name} with Score : {gender.Student.Grade}")
            );

        Console.ReadLine();
    }
}