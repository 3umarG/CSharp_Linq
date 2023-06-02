using System.Text.Json.Serialization;

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