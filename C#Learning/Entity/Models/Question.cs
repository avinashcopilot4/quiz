using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class Question
{
    public int QuestionId { get; set; }

    public int QuizId { get; set; }

    public string QuestionText { get; set; } = null!;

    public string Option1 { get; set; } = null!;

    public string Option2 { get; set; } = null!;

    public string Option3 { get; set; } = null!;

    public string Option4 { get; set; } = null!;

    public int CorrectOption { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<AttemptDetail> AttemptDetails { get; set; } = new List<AttemptDetail>();

    public virtual Quiz Quiz { get; set; } = null!;
}
