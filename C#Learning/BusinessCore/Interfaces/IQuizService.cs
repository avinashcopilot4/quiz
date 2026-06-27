using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessCore.Interfaces;

public interface IQuizService
{
    Task<IEnumerable<Quiz>> GetAllQuizzesAsync();
    Task<IEnumerable<Quiz>> GetPublishedQuizzesAsync();
    Task<Quiz?> GetQuizByIdAsync(int quizId);
    Task<Quiz> CreateQuizAsync(Common.DTO.Quiz.QuizDto quiz);
    Task UpdateQuizAsync(Common.DTO.Quiz.QuizDto quiz);
    Task DeleteQuizAsync(int quizId);
    Task<bool> QuizExistsAsync(int quizId);
}
