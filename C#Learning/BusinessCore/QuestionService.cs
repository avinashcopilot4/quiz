using BusinessCore.Interfaces;
using Entity.Models;
using Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessCore;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _questionRepository;

    public QuestionService(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public Task<IEnumerable<Question>> GetAllQuestionsAsync()
    {
        return _questionRepository.GetAllAsync();
    }

    public Task<IEnumerable<Question>> GetQuestionsByQuizIdAsync(int quizId)
    {
        return _questionRepository.GetByQuizIdAsync(quizId);
    }

    public Task<Question?> GetQuestionByIdAsync(int questionId)
    {
        return _questionRepository.GetByIdAsync(questionId);
    }

    public async Task<Question> CreateQuestionAsync(Question question)
    {
        await _questionRepository.AddAsync(question);
        return question;
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
}
