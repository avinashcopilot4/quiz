using BusinessCore.Interfaces;
using Common.DTO.Quiz;
using Entity.Models;
using Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessCore;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _questionRepository;

    public QuestionService(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<IEnumerable<QuestionDto>> GetAllQuestionsAsync()
    {
        var questions = await _questionRepository.GetAllAsync();
        return questions.Select(MapToDto).ToList();
    }

    public async Task<IEnumerable<QuestionDto>> GetQuestionsByQuizIdAsync(int quizId)
    {
        var questions = await _questionRepository.GetByQuizIdAsync(quizId);
        return questions.Select(MapToDto).ToList();
    }

    public async Task<QuestionDto?> GetQuestionByIdAsync(int questionId)
    {
        var question = await _questionRepository.GetByIdAsync(questionId);
        return question == null ? null : MapToDto(question);
    }

    public async Task<QuestionDto> CreateQuestionAsync(Question question)
    {
        await _questionRepository.AddAsync(question);
        return MapToDto(question);
    }

    public Task UpdateQuestionAsync(Question question)
    {
        return _questionRepository.UpdateAsync(question);
    }

    public Task DeleteQuestionAsync(int questionId)
    {
        return _questionRepository.DeleteAsync(questionId);
    }

    public Task<bool> QuestionExistsAsync(int questionId)
    {
        return _questionRepository.ExistsAsync(questionId);
    }

    private static QuestionDto MapToDto(Question question)
    {
        return new QuestionDto
        {
            Id = question.QuestionId,
            Text = question.QuestionText,
            Options = new List<string> { question.Option1, question.Option2, question.Option3, question.Option4 },
            CorrectOptionIndex = question.CorrectOption,
        };
    }
}
