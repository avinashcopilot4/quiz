using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entity.Models;

public partial class User
{

    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;
    public string Gender { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }


    public virtual ICollection<QuizAttempt> QuizAttempts { get; set; } = new List<QuizAttempt>();
}
