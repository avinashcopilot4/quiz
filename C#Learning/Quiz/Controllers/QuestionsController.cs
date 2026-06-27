using BusinessCore.Interfaces;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;

namespace QuizApp.Controllers;

[Route("api/questions")]
[ApiController]
public class QuestionsController : ControllerBase
{
    private readonly IQuestionService _questionService;

    public QuestionsController(IQuestionService questionService)
    {
        _questionService = questionService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
    {
        return Ok(await _questionService.GetAllQuestionsAsync());
    }

    [HttpGet("by-quiz/{quizid:int}")]
    public async Task<ActionResult<IEnumerable<Question>>> GetQuestionsByQuiz(int quizid)
    {
        return Ok(await _questionService.GetQuestionsByQuizIdAsync(quizid));
    }

    [HttpGet("{questionid:int}")]
    public async Task<ActionResult<Question>> GetQuestion(int questionid)
    {
        var question = await _questionService.GetQuestionByIdAsync(questionid);
        if (question == null)
        {
            return NotFound();
        }

        return Ok(question);
    }

    [HttpPost]
    public async Task<ActionResult<Question>> CreateQuestion(Question question)
    {
        await _questionService.CreateQuestionAsync(question);
        return CreatedAtAction(nameof(GetQuestion), new { questionid = question.QuestionId }, question);
    }

    [HttpPut("{questionid:int}")]
    public async Task<IActionResult> UpdateQuestion(int questionid, Question question)
    {
        if (questionid != question.QuestionId)
        {
            return BadRequest();
        }

        if (!await _questionService.QuestionExistsAsync(questionid))
        {
            return NotFound();
        }

        await _questionService.UpdateQuestionAsync(question);
        return NoContent();
    }

    [HttpDelete("{questionid:int}")]
    public async Task<IActionResult> DeleteQuestion(int questionid)
    {
        if (!await _questionService.QuestionExistsAsync(questionid))
        {
            return NotFound();
        }

        await _questionService.DeleteQuestionAsync(questionid);
        return NoContent();
    }
}
