using BusinessCore.Interfaces;
using Common.DTO.Quiz;
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
    public async Task<ActionResult<IEnumerable<QuestionDto>>> GetQuestions()
    {
        var questions = await _questionService.GetAllQuestionsAsync();
        return Ok(questions);
    }

    [HttpGet("by-quiz/{quizid:int}")]
    public async Task<ActionResult<IEnumerable<QuestionDto>>> GetQuestionsByQuiz(int quizid)
    {
        var questions = await _questionService.GetQuestionsByQuizIdAsync(quizid);
        return Ok(questions);
    }

    [HttpGet("{questionid:int}")]
    public async Task<ActionResult<QuestionDto>> GetQuestion(int questionid)
    {
        var question = await _questionService.GetQuestionByIdAsync(questionid);
        if (question == null)
        {
            return NotFound();
        }

        return Ok(question);
    }

    [HttpPost]
    public async Task<ActionResult<QuestionDto>> CreateQuestion(Question question)
    {
        var created = await _questionService.CreateQuestionAsync(question);
        return CreatedAtAction(nameof(GetQuestion), new { questionid = created.Id }, created);
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
