using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Common.DTO.Quiz;

public class QuestionDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    [JsonPropertyName("options")]
    public List<string> Options { get; set; } = new();

    [JsonPropertyName("correctOptionIndex")]
    public int CorrectOptionIndex { get; set; }
}
