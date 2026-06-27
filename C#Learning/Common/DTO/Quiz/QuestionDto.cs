using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Common.DTO.Quiz;

public class QuestionDto
{
    [JsonPropertyName("id")]
    public int QuestionId { get; set; }

    public string Text { get; set; } = null!;

    public List<string> Options { get; set; } = new();

    [JsonPropertyName("correctOptionIndex")]
    public int CorrectOptionIndex { get; set; }
}
