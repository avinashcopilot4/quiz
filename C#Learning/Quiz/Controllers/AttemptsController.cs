using BusinessCore.Interfaces;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<IEnumerable<QuizAttempt>>> GetAttempts()
    {
        return Ok(await _attemptService.GetAllAttemptsAsync());
    }

    [HttpGet("by-employee/{userid:int}")]
    public async Task<ActionResult<IEnumerable<QuizAttempt>>> GetAttemptsByEmployee(int userid)
    {
        return Ok(await _attemptService.GetAttemptsByEmployeeAsync(userid));
    }

    [HttpGet("by-quiz/{quizid:int}")]
    public async Task<ActionResult<IEnumerable<QuizAttempt>>> GetAttemptsByQuiz(int quizid)
    {
        return Ok(await _attemptService.GetAttemptsByQuizAsync(quizid));
    }

    [HttpGet("{attemptid:int}")]
    public async Task<ActionResult<QuizAttempt>> GetAttempt(int attemptid)
    {
        var attempt = await _attemptService.GetAttemptByIdAsync(attemptid);
        if (attempt == null)
        {
            return NotFound();
        }

        return Ok(attempt);
    }

    [HttpPost]
    public async Task<ActionResult<QuizAttempt>> CreateAttempt(QuizAttempt attempt)
    {
        await _attemptService.CreateAttemptAsync(attempt);
        return CreatedAtAction(nameof(GetAttempt), new { attemptid = attempt.AttemptId }, attempt);
    }
}
