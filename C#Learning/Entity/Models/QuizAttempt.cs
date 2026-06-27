using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class QuizAttempt
{
    public int AttemptId { get; set; }

    public int UserId { get; set; }

    public int QuizId { get; set; }

    public int Score { get; set; }

    public int MaxPossibleScore { get; set; }

    public decimal? PercentageScore { get; set; }

    public int AttemptedQuestions { get; set; }

    public int? TimeTaken { get; set; }

    public DateTime? CompletedDate { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<AttemptDetail> AttemptDetails { get; set; } = new List<AttemptDetail>();

    public virtual Quiz Quiz { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
