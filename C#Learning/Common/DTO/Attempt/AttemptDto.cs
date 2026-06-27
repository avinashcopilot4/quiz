using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Common.DTO.Attempt;

namespace Common.DTO.Attempt;

public class AttemptDto
{
    [JsonPropertyName("id")]
    public int AttemptId { get; set; }

    [JsonPropertyName("quizId")]
    public int QuizId { get; set; }

    [JsonPropertyName("employeeId")]
    public int EmployeeId { get; set; }

    public List<AnswerDto> Answers { get; set; } = new();

    public int Score { get; set; }

    public int MaxPossibleScore { get; set; }

    public decimal? PercentageScore { get; set; }

    public int AttemptedQuestions { get; set; }

    public int? TimeTaken { get; set; }

    [JsonPropertyName("startedAt")]
    public DateTime? StartedAt { get; set; }

    [JsonPropertyName("submittedAt")]
    public DateTime? SubmittedAt { get; set; }
}
