using System.Text.Json;
using System.Text.Json.Serialization;

//Console.WriteLine("Hello, World!");
var fileContent = await File.ReadAllTextAsync("data.json");
var students = JsonSerializer.Deserialize<List<Student>>(fileContent);
/*
// All students that first name start with 'A' and their grade greater than 75
students
    .Where(s => s.FirstName.Contains("A"))
    .ToList()
    .ForEach(s => Console.WriteLine($"Student : {s.FirstName} {s.LastName} with Grade : {s.Grade}"));

// the Max Grade
var highGrade = students.Max(s => s.Grade);
Console.WriteLine($"The High Score is ${highGrade}");

// Top 10 Students
// Take : used for take the First n number of elements you entered .
// Skip : the opposite of the Take , Skip n elements and take the items after that .
students
    .OrderByDescending(s => s.Grade)
    .Take(10)
    .ToList()
    .ForEach(s => Console.WriteLine($"{s.FirstName} {s.LastName} with Score : {s.Grade}"));
*/

var numberOFGenders = students.GroupBy(s => s.Gender).ToList().Count;
// GroupBy ===> return GroupingItem has the Key of distinct value that i have determined
//              and the value is all the objects of Student that match with this value

/*
students
    .GroupBy(s => s.Gender)
    .ToList()
    .ForEach(s => Console.WriteLine(s.Key));
Console.WriteLine(" ----------------------------- ");
*/

// Get every key with number of values that take this Key
// Key : IEnumerable of Students 
// (IEnumerable of Students).Count
/*
students
    .GroupBy(student => student.Gender)
    .Select(
    groupOfStudentsMarkedWithGender => 
    new { key = groupOfStudentsMarkedWithGender.Key,
        numberOfStudents = 
        (groupOfStudentsMarkedWithGender.Where(student => student.FirstName.Contains("A"))).Count() 
    })
    // Select : return new IEnumerable of type Anonymous Object that has only two att.,key and numebr of Items , based on Filteration Process 
    // With that way , I Group All Genders , then Filter all values(Students) with every Group
    // Group then Filter
    .ToList()
    .ForEach(g => Console.WriteLine(g.key + " : " + g.numberOfStudents));

Console.WriteLine(" ----------------------------- ");
*/
/*
Console.WriteLine(students.GroupBy(s => s.Gender).Count());
Console.WriteLine(numberOFGenders);
*/


// More advanced Query with GroupBy
var carsFile = await File.ReadAllTextAsync("cars.json");
var cars = JsonSerializer.Deserialize<List<Car>>(carsFile);

// 1- Display the number of models per make that appeared in 2011
/*
cars
    .GroupBy(c => c.Make)
    .Select(c => new { Make = c.Key, numberOfModelsAppearedAfter = c.Count(c => c.Year == 2011) })
    .ToList()
    .ForEach(c => Console.WriteLine($"Model : {c.Make} has number of : {c.numberOfModelsAppearedAfter}"));
*/

// 2- Display a list of makes that have at least two models that appeared after 1995
/*
cars
    .Where(car => car.HP > 400)
    .GroupBy(car => car.Make)
    .Select(carGroup => new { Id = carGroup.Key, numberOfModels = carGroup.Count() })
    .Where(carObject => carObject.numberOfModels >= 2)
    .ToList()
    .ForEach(c => Console.WriteLine($"{c.Id} appeares : {c.numberOfModels} Times ."));
*/

// 3- Display every Make with its average of HP
/*
cars
    .GroupBy(car => car.Make)
    .Select(carGroup => new { Make = carGroup.Key, Avg = carGroup.Average(carGroup => carGroup.HP) })
    .ToList()
    .ForEach(car => Console.WriteLine($"The Make : {car.Make} has Average : {car.Avg} "));
*/


// 4- How many Makes in each field of HP as : 0..100 / 101..200 / 201..300 / 301..400 / 401..500
// The output ===> 0..100 : 55
//                 101 ..200 : 5
// Distinct make as GroupBy but the difference that it only returns IEnumerable of items not IGrouping .
cars
    .GroupBy(car => car.HP switch
    {
        <= 100 => "0 .. 100",
        <= 200 => "101 .. 200",
        <= 300 => "201 .. 300",
        <= 400 => "301 .. 400",
        _ => "401 .. 500"
    })
    .Select(carG => new {HP = carG.Key , numberOfMakes = carG.Select(c => c.Make).Distinct().Count()})
    .ToList()
    .ForEach(c => Console.WriteLine($"{c.HP} has {c.numberOfMakes}"));

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


class Car
{
    [JsonPropertyName("id")]
    public int ID { get; set; }

    [JsonPropertyName("car_make")]
    public string Make { get; set; }

    [JsonPropertyName("car_model")]
    public string Model { get; set; }

    [JsonPropertyName("car_year")]
    public int Year { get; set; }

    [JsonPropertyName("number_of_doors")]
    public int Doors { get; set; }

    [JsonPropertyName("hp")]
    public int HP { get; set; }

}