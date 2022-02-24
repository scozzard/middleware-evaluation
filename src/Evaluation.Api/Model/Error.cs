using System.Text.Json.Serialization;

namespace Evaluation.Api.Model;

public class Error
{
    [JsonPropertyName("error")]
    public string Name { get; set; }

    [JsonPropertyName("error_description")]
    public string Description { get; set; }

    public Error(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
