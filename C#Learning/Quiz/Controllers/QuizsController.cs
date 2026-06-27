using BusinessCore.Interfaces;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<IEnumerable<Quiz>>> GetQuizzes([FromQuery] string? status = null)
    {
        if (string.Equals(status, "published", StringComparison.OrdinalIgnoreCase))
        {
            return Ok(await _quizService.GetPublishedQuizzesAsync());
        }

        return Ok(await _quizService.GetAllQuizzesAsync());
    }

    [HttpGet("{quizid:int}")]
    public async Task<ActionResult<Quiz>> GetQuiz(int quizid)
    {
        var quiz = await _quizService.GetQuizByIdAsync(quizid);
        if (quiz == null)
        {
            return NotFound();
        }

        return Ok(quiz);
    }

    [HttpPost]
    public async Task<ActionResult<Quiz>> CreateQuiz(Quiz quiz)
    {
        await _quizService.CreateQuizAsync(quiz);
        return CreatedAtAction(nameof(GetQuiz), new { quizid = quiz.QuizId }, quiz);
    }

    [HttpPut("{quizid:int}")]
    public async Task<IActionResult> UpdateQuiz(int quizid, Quiz quiz)
    {
        if (quizid != quiz.QuizId)
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
