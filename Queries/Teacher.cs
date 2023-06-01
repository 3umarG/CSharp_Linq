using System.Text.Json.Serialization;

internal partial class Program
{
    class Teacher
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("teacher_name")]
        public string Name { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }
    }
}