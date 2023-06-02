using System.Text.Json;
using System.Text.RegularExpressions;

//Console.WriteLine("Hello, World!");
var fileContent = await File.ReadAllTextAsync("data.json");
var students = JsonSerializer.Deserialize<List<Student>>(fileContent);
#region All students that first name start with 'A' and their grade greater than 75
//students
//    .Where(s => s.FirstName.Contains("A"))
//    .ToList()
//    .ForEach(s => Console.WriteLine($"Student : {s.FirstName} {s.LastName} with Grade : {s.Grade}"));
#endregion


#region The Max Grade
//var highGrade = students.Max(s => s.Grade);
//var studentWithMaxGrade = students.FirstOrDefault(s => s.Grade == highGrade);
//Console.WriteLine($"The High Score is ${highGrade}");
#endregion

#region Top 10 Students
// Take : used for take the First n number of elements you entered .
// Skip : the opposite of the Take , Skip n elements and take the items after that .
students
    .OrderByDescending(s => s.Grade)
    .Take(10)
    .ToList()
    .ForEach(s => Console.WriteLine($"{s.FirstName} {s.LastName} with Score : {s.Grade}"));
#endregion

#region GroupBy ===> return GroupingItem has the Key of distinct value that i have determined and the value is all the objects of Student that match with this value

/*
var numberOFGenders = students.GroupBy(s => s.Gender).ToList().Count;
students
    .GroupBy(s => s.Gender)
    .ToList()
    .ForEach(s => Console.WriteLine(s.Key));
Console.WriteLine(" ----------------------------- ");
*/
#endregion

#region Get every key with number of values that take this Key
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
#endregion

//Console.WriteLine(students.GroupBy(s => s.Gender).Count());
//Console.WriteLine(numberOFGenders);



// More advanced Query with GroupBy
var carsFile = await File.ReadAllTextAsync("cars.json");
var cars = JsonSerializer.Deserialize<List<Car>>(carsFile);

#region 1- Display the number of models per make that appeared in 2011
/*
cars
    .GroupBy(c => c.Make)
    .Select(c => new { Make = c.Key, numberOfModelsAppearedAfter = c.Count(c => c.Year == 2011) })
    .ToList()
    .ForEach(c => Console.WriteLine($"Model : {c.Make} has number of : {c.numberOfModelsAppearedAfter}"));
*/
#endregion

#region 2- Display a list of makes that have at least two models that appeared after 1995
/*
cars
    .Where(car => car.HP > 400)
    .GroupBy(car => car.Make)
    .Select(carGroup => new { Id = carGroup.Key, numberOfModels = carGroup.Count() })
    .Where(carObject => carObject.numberOfModels >= 2)
    .ToList()
    .ForEach(c => Console.WriteLine($"{c.Id} appeares : {c.numberOfModels} Times ."));
*/
#endregion

#region 3- Display every Make with its average of HP
/*
cars
    .GroupBy(car => car.Make)
    .Select(carGroup => new { Make = carGroup.Key, Avg = carGroup.Average(carGroup => carGroup.HP) })
    .ToList()
    .ForEach(car => Console.WriteLine($"The Make : {car.Make} has Average : {car.Avg} "));
*/
#endregion


#region 4- How many Makes in each field of HP as : 0..100 / 101..200 / 201..300 / 301..400 / 401..500
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
    .Select(group => new { 
        HP = group.Key,
        numberOfMakes = group.Where(car => car.Make.Length == 4).Select(car => car.Make).Distinct().Count(),
        Makes = group.Where(car => car.Make.Length == 4).Select(car => car.Make).Distinct() })
    .ToList()
    .ForEach(c =>
    {
        Console.WriteLine($"{c.HP} has {c.numberOfMakes}");
        foreach (var make in c.Makes)
        {
            Console.WriteLine($"-- {make}");
        }
    });
#endregion



#region Hints for ITI Lectures
// Fluents Expression : () . () . () .....
// the Query expression should start with from ... and end with select
// if i want to select then use where I must : 
// 1- Seperate the expressions
// 2- use into ===> mark the final result as new source , that enable me to restart the pipeline with the new source
#endregion


#region Types of Operators in LINQ :
// 1- Sequence Operators : return output sequence          : Where , Select ..

//       Where "Filteration Operator" : return the same form of the elements ,but only with less number عدد اقل بس نفس الصورة 
//       Select "Transformation Operator " : return reformatted type of the elements "updated or new type" with the same number of elements.

// 2- Element Operators "immidiate execution" not deffered : return single element عنصر واحد او صف واحد / First() , Last() , ...
// 3- Aggregate Operators "immidiate execution" not deffered : return single value قيمة واحدة من القيم الموجودة في العمود  / Max() , Min() , Count() , Avg() , Sum() 
//
// I can combine using sub-queries by using element & aggregate operators both of them together
// Aggregate ===> for finding the Max Value without any information about the element itself
// then using element operators like First() , Last() to match the value given from Aggregate .

// 4- Generator Operators : Generate Seq. of elements from nothing , Only called by using Enumerable.***
//    example : Range(0,5) / Repeat() / ...
//    

// 5- Select Many : Transform Operator like Normal Select , but it returns output seq larger than input seq in length/count
//var names = new List<string>() { "Omar Gomaa" , "Ahmed Ali" , "Mohammed Ahmed"};
// I want this output : {Omar , Gomaa , ....}
//var res = names.SelectMany(name => name.Split(" "));

//6- Quantifiers Operator : return boolean based on condition
// Any() , All() , ***.SequenceEqual(***)

// 7- ZIP operator : compine 2 datasource sequneces to one sequence result.

// /- Into / Let : Introducing new sequence to continue on it in Query Expression Only "Restart the input seq."
// Let ===> intrduce new seq as variable and save the old variable if you want to use .
// into ===> convert the seq. to new one and remove the old one , so you can't use it .

var names = new List<string>()
{
    "Omar",
    "Ali",
    "Ziad",
    "Momen",
    "Mahmoud",
    "Ahmed",
    "Abdullah"
};

// Remove all Vowels from every name and then order them 

// By using into

/*
var result = from name in names
             select Regex.Replace(name, "[aeiouAEIOU]",String.Empty) 
             into nameAfterRemoving // restart the seq. with the new names after removing vowels
             where nameAfterRemoving.Length >= 3
             select nameAfterRemoving;

// By using let : introduce seq as new variable and use the old seq if you need
result = from name in names
         let nameAfterRemovingVowels = Regex.Replace(name, "[aeiouAEIOU]", String.Empty)
         where nameAfterRemovingVowels.Length >= 3
         // note : you can use the old name 
         orderby nameAfterRemovingVowels ascending, name descending
         select nameAfterRemovingVowels;


foreach(var name in result)
{
    Console.WriteLine(name);
}
*/
#endregion

