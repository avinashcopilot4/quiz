using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Common.DTO.Quiz;

public class QuizDto
{
    [JsonPropertyName("id")]
    public int QuizId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string Topic { get; set; } = null!;

    public string Language { get; set; } = null!;

    public string Difficulty { get; set; } = null!;

    public List<string> Tags { get; set; } = new();

    public string CreatedBy { get; set; } = null!;

    public string Author { get; set; } = null!;

    public int QuestionCount { get; set; }

    public int? TimeLimit { get; set; }

    public int? PassingScore { get; set; }

    public string Status { get; set; } = null!;

    [JsonPropertyName("createdAt")]
    public DateTime? CreatedDate { get; set; }

    [JsonPropertyName("updatedAt")]
    public DateTime? UpdatedDate { get; set; }

    public List<QuestionDto> Questions { get; set; } = new();
}
