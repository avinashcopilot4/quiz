using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class AttemptDetail
{
    public int DetailId { get; set; }

    public int AttemptId { get; set; }

    public int QuestionId { get; set; }

    public int UserAnswer { get; set; }

    public int CorrectAnswer { get; set; }

    public bool IsCorrect { get; set; }

    public int? TimeTaken { get; set; }

    public virtual QuizAttempt Attempt { get; set; } = null!;

    public virtual Question Question { get; set; } = null!;
}
