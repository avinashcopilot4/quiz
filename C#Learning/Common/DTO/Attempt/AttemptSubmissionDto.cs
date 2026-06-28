using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Common.DTO.Attempt;

public class AttemptSubmissionDto
{
    [JsonPropertyName("userId")]
    public int UserId { get; set; }

    [JsonPropertyName("quizId")]
    public int QuizId { get; set; }

    [JsonPropertyName("score")]
    public int Score { get; set; }

    [JsonPropertyName("maxPossibleScore")]
    public int MaxPossibleScore { get; set; }

    [JsonPropertyName("attemptedQuestions")]
    public int AttemptedQuestions { get; set; }

    [JsonPropertyName("completedDate")]
    public DateTime? CompletedDate { get; set; }

    [JsonPropertyName("attemptDetails")]
    public List<AttemptDetailSubmissionDto> AttemptDetails { get; set; } = new();
}

public class AttemptDetailSubmissionDto
{
    [JsonPropertyName("questionId")]
    public int QuestionId { get; set; }

    [JsonPropertyName("userAnswer")]
    public int UserAnswer { get; set; }

    [JsonPropertyName("correctAnswer")]
    public int CorrectAnswer { get; set; }

    [JsonPropertyName("isCorrect")]
    public bool IsCorrect { get; set; }
}
