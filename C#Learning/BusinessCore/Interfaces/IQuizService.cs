using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessCore.Interfaces;

public interface IQuizService
{
    Task<IEnumerable<Quiz>> GetAllQuizzesAsync();
    Task<IEnumerable<Quiz>> GetPublishedQuizzesAsync();
    Task<Quiz?> GetQuizByIdAsync(int quizId);
    Task<Quiz> CreateQuizAsync(Quiz quiz);
    Task UpdateQuizAsync(Quiz quiz);
    Task DeleteQuizAsync(int quizId);
    Task<bool> QuizExistsAsync(int quizId);
}
