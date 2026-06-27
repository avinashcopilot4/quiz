using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Common.DTO.Attempt;

public class AttemptDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("quizId")]
    public string QuizId { get; set; } = string.Empty;

    [JsonPropertyName("employeeId")]
    public string EmployeeId { get; set; } = string.Empty;

    [JsonPropertyName("answers")]
    public List<AnswerDto> Answers { get; set; } = new();

    [JsonPropertyName("score")]
    public int Score { get; set; }

    [JsonPropertyName("startedAt")]
    public DateTime? StartedAt { get; set; }

    [JsonPropertyName("submittedAt")]
    public DateTime? SubmittedAt { get; set; }
}
