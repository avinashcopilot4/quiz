using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Common.DTO.Quiz;

public class QuizDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("topic")]
    public string Topic { get; set; } = "General";

    [JsonPropertyName("language")]
    public string Language { get; set; } = "English";

    [JsonPropertyName("difficulty")]
    public string Difficulty { get; set; } = "Intermediate";

    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; } = new();

    [JsonPropertyName("createdBy")]
    public string CreatedBy { get; set; } = "admin";

    [JsonPropertyName("author")]
    public string Author { get; set; } = "Admin";

    [JsonPropertyName("createdAt")]
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")]
    public DateTime? UpdatedAt { get; set; }

    [JsonPropertyName("questionCount")]
    public int QuestionCount { get; set; }

    [JsonPropertyName("timeLimit")]
    public int? TimeLimit { get; set; }

    [JsonPropertyName("passingScore")]
    public int? PassingScore { get; set; }

    [JsonPropertyName("questions")]
    public List<QuestionDto> Questions { get; set; } = new();

    [JsonPropertyName("status")]
    public string Status { get; set; } = "draft";
}
