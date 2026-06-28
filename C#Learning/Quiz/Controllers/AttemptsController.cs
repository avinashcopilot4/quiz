using BusinessCore.Interfaces;
using Common.DTO.Attempt;
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
    public async Task<ActionResult<IEnumerable<AttemptDto>>> GetAttempts()
    {
        var attempts = await _attemptService.GetAllAttemptsAsync();
        return Ok(attempts);
    }

    [HttpGet("by-employee/{userid:int}")]
    public async Task<ActionResult<IEnumerable<AttemptDto>>> GetAttemptsByEmployee(int userid)
    {
        var attempts = await _attemptService.GetAttemptsByEmployeeAsync(userid);
        return Ok(attempts);
    }

    [HttpGet("by-quiz/{quizid:int}")]
    public async Task<ActionResult<IEnumerable<AttemptDto>>> GetAttemptsByQuiz(int quizid)
    {
        var attempts = await _attemptService.GetAttemptsByQuizAsync(quizid);
        return Ok(attempts);
    }

    [HttpGet("{attemptid:int}")]
    public async Task<ActionResult<AttemptDto>> GetAttempt(int attemptid)
    {
        var attempt = await _attemptService.GetAttemptByIdAsync(attemptid);
        if (attempt == null)
        {
            return NotFound();
        }

        return Ok(attempt);
    }

    [HttpPost]
    public async Task<ActionResult<AttemptDto>> CreateAttempt([FromBody] AttemptSubmissionDto request)
    {
        if (request == null)
        {
            return BadRequest();
        }

        var created = await _attemptService.CreateAttemptAsync(request);
        return CreatedAtAction(nameof(GetAttempt), new { attemptid = created.Id }, created);
    }
}
