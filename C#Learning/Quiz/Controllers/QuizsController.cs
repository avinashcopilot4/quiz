using BusinessCore.Interfaces;
using Common.DTO.Quiz;
using Microsoft.AspNetCore.Mvc;
using System;

[Route("api/quizzes")]
[ApiController]
public class QuizzesController : ControllerBase
{
    private readonly IQuizService _quizService;

    public QuizzesController(IQuizService quizService)
    {
        _quizService = quizService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<QuizDto>>> GetQuizzes([FromQuery] string? status = null)
    {
        var quizzes = string.Equals(status, "published", StringComparison.OrdinalIgnoreCase)
            ? await _quizService.GetPublishedQuizzesAsync()
            : await _quizService.GetAllQuizzesAsync();

        return Ok(quizzes);
    }

    [HttpGet("{quizid:int}")]
    public async Task<ActionResult<QuizDto>> GetQuiz(int quizid)
    {
        var quiz = await _quizService.GetQuizByIdAsync(quizid);
        if (quiz == null)
        {
            return NotFound();
        }

        return Ok(quiz);
    }

    [HttpPost]
    public async Task<ActionResult<QuizDto>> CreateQuiz([FromBody] QuizDto quiz)
    {
        var created = await _quizService.CreateQuizAsync(quiz);
        return CreatedAtAction(nameof(GetQuiz), new { quizid = created.Id }, created);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateQuiz([FromQuery] int quizid, [FromBody] QuizDto quiz)
    {
        if (quizid.ToString() != quiz.Id)
        {
            return BadRequest();
        }

        if (!await _quizService.QuizExistsAsync(quizid))
        {
            return NotFound();
        }

        await _quizService.UpdateQuizAsync(quiz);
        return NoContent();
    }

    [HttpDelete("{quizid:int}")]
    public async Task<IActionResult> DeleteQuiz(int quizid)
    {
        if (!await _quizService.QuizExistsAsync(quizid))
        {
            return NotFound();
        }

        await _quizService.DeleteQuizAsync(quizid);
        return NoContent();
    }
}
