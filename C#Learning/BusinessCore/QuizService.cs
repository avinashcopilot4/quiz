using BusinessCore.Interfaces;
using Entity.Models;
using Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessCore;

public class QuizService : IQuizService
{
    private readonly IQuizRepository _quizRepository;

    public QuizService(IQuizRepository quizRepository)
    {
        _quizRepository = quizRepository;
    }

    public Task<IEnumerable<Quiz>> GetAllQuizzesAsync()
    {
        return _quizRepository.GetAllAsync();
    }

    public Task<IEnumerable<Quiz>> GetPublishedQuizzesAsync()
    {
        return _quizRepository.GetPublishedAsync();
    }

    public Task<Quiz?> GetQuizByIdAsync(int quizId)
    {
        return _quizRepository.GetByIdAsync(quizId);
    }

    public async Task<Quiz> CreateQuizAsync(Quiz quiz)
    {
        await _quizRepository.AddAsync(quiz);
        return quiz;
    }

    public Task UpdateQuizAsync(Quiz quiz)
    {
        return _quizRepository.UpdateAsync(quiz);
    }

    public Task DeleteQuizAsync(int quizId)
    {
        return _quizRepository.DeleteAsync(quizId);
    }

    public Task<bool> QuizExistsAsync(int quizId)
    {
        return _quizRepository.ExistsAsync(quizId);
    }
}
