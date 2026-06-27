using BusinessCore.Interfaces;
using Common.DTO.Attempt;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace QuizApp.Controllers;

[Route("api/attempts")]
[ApiController]
public class AttemptsController : ControllerBase
{
    private readonly IAttemptService _attemptService;

    public AttemptsController(IAttemptService attemptService)
    {
        _attemptService = attemptService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AttemptDto>>> GetAttempts()
    {
        var attempts = await _attemptService.GetAllAttemptsAsync();
        return Ok(attempts.Select(ToDto));
    }

    [HttpGet("by-employee/{userid:int}")]
    public async Task<ActionResult<IEnumerable<AttemptDto>>> GetAttemptsByEmployee(int userid)
    {
        var attempts = await _attemptService.GetAttemptsByEmployeeAsync(userid);
        return Ok(attempts.Select(ToDto));
    }

    [HttpGet("by-quiz/{quizid:int}")]
    public async Task<ActionResult<IEnumerable<AttemptDto>>> GetAttemptsByQuiz(int quizid)
    {
        var attempts = await _attemptService.GetAttemptsByQuizAsync(quizid);
        return Ok(attempts.Select(ToDto));
    }

    [HttpGet("{attemptid:int}")]
    public async Task<ActionResult<AttemptDto>> GetAttempt(int attemptid)
    {
        var attempt = await _attemptService.GetAttemptByIdAsync(attemptid);
        if (attempt == null)
        {
            return NotFound();
        }

        return Ok(ToDto(attempt));
    }

    [HttpPost]
    public async Task<ActionResult<AttemptDto>> CreateAttempt(QuizAttempt attempt)
    {
        var created = await _attemptService.CreateAttemptAsync(attempt);
        return CreatedAtAction(nameof(GetAttempt), new { attemptid = created.AttemptId }, ToDto(created));
    }

    private static AttemptDto ToDto(QuizAttempt attempt)
    {
        return new AttemptDto
        {
            Id = attempt.AttemptId.ToString(),
            QuizId = attempt.QuizId.ToString(),
            EmployeeId = attempt.UserId.ToString(),
            Answers = attempt.AttemptDetails?.Select(detail => new AnswerDto
            {
                QuestionId = detail.QuestionId,
                SelectedOptionIndex = detail.UserAnswer,
            }).ToList() ?? new List<AnswerDto>(),
            Score = attempt.Score,
            StartedAt = attempt.CompletedDate,
            SubmittedAt = attempt.CompletedDate,
        };
    }
}
