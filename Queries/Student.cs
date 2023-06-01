using System.Text.Json.Serialization;

internal partial class Program
{
    class Student
    {

        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("student_name")]
        public string Name { get; set; }


        [JsonPropertyName("email")]
        public string Email { get; set; }


        [JsonPropertyName("grade")]
        public int Grade { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }


    }
}