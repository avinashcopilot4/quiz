using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Common.DTO.Attempt;

namespace Common.DTO.Attempt;

public class AttemptSubmissionDto
{
    [JsonPropertyName("quizId")]
    public int QuizId { get; set; }

    [JsonPropertyName("employeeId")]
    public int EmployeeId { get; set; }

    public List<AnswerDto> Answers { get; set; } = new();

    [JsonPropertyName("startedAt")]
    public DateTime StartedAt { get; set; }

    [JsonPropertyName("submittedAt")]
    public DateTime SubmittedAt { get; set; }
}
