using BusinessCore.Interfaces;
using Common.DTO.Quiz;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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

        return Ok(quizzes.Select(ToDto));
    }

    [HttpGet("{quizid:int}")]
    public async Task<ActionResult<QuizDto>> GetQuiz(int quizid)
    {
        var quiz = await _quizService.GetQuizByIdAsync(quizid);
        if (quiz == null)
        {
            return NotFound();
        }

        return Ok(ToDto(quiz));
    }

    [HttpPost]
    public async Task<ActionResult<QuizDto>> CreateQuiz([FromBody]QuizDto quiz)
    {
        var created = await _quizService.CreateQuizAsync(quiz);
        return CreatedAtAction(nameof(GetQuiz), new { quizid = created.QuizId }, ToDto(created));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateQuiz([FromQuery] int quizid, [FromBody]QuizDto quiz)
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

    private static QuizDto ToDto(Quiz quiz)
    {
        return new QuizDto
        {
            Id = quiz.QuizId.ToString(),
            Title = quiz.Title,
            Description = quiz.Description,
            Topic = "General",
            Language = "English",
            Difficulty = "Intermediate",
            Tags = new List<string>(),
            CreatedBy = "admin",
            Author = "Admin",
            QuestionCount = quiz.Questions?.Count ?? 0,
            TimeLimit = 10,
            PassingScore = 70,
            Status = quiz.Status ?? "draft",
            CreatedAt = quiz.CreatedDate,
            UpdatedAt = quiz.UpdatedDate,
            Questions = quiz.Questions?.Select(q => new QuestionDto
            {
                Id = q.QuestionId,
                Text = q.QuestionText,
                Options = new List<string> { q.Option1, q.Option2, q.Option3, q.Option4 },
                CorrectOptionIndex = q.CorrectOption,
            }).ToList() ?? new List<QuestionDto>(),
        };
    }
}
