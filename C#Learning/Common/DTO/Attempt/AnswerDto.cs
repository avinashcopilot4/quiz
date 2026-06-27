using System.Text.Json.Serialization;

namespace Common.DTO.Attempt;

public class AnswerDto
{
    [JsonPropertyName("questionId")]
    public int QuestionId { get; set; }

    [JsonPropertyName("selectedOptionIndex")]
    public int SelectedOptionIndex { get; set; }
}
