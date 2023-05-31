using System.Text.Json;
using System.Text.Json.Serialization;

//Console.WriteLine("Hello, World!");
var fileContent = await File.ReadAllTextAsync("data.json");
var students = JsonSerializer.Deserialize<List<Student>>(fileContent);

// All students that first name start with 'A' and their grade greater than 75
students
    .Where(s => s.FirstName.Contains("A") && s.Grade > 30)
    .ToList()
    .ForEach(s => Console.WriteLine($"Student : {s.FirstName} {s.LastName} with Grade : {s.Grade}"));

// the Max Grade
var highGrade = students.Max(s => s.Grade);
Console.WriteLine($"The High Score is ${highGrade}");

// Top 10 Students
students
    .OrderByDescending(s => s.Grade)
    .Take(10)
    .ToList()
    .ForEach(s => Console.WriteLine($"{s.FirstName} {s.LastName} with Score : {s.Grade}"));

class Student
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string LastName { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("gender")]
    public string Gender { get; set; }

    [JsonPropertyName("grade")]
    public int Grade { get; set; }

}
