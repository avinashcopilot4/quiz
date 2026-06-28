using Common.DTO.Quiz;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessCore.Interfaces;

public interface IQuizService
{
    Task<IEnumerable<QuizDto>> GetAllQuizzesAsync();
    Task<IEnumerable<QuizDto>> GetPublishedQuizzesAsync();
    Task<QuizDto?> GetQuizByIdAsync(int quizId);
    Task<QuizDto> CreateQuizAsync(QuizDto quiz);
    Task UpdateQuizAsync(QuizDto quiz);
    Task DeleteQuizAsync(int quizId);
    Task<bool> QuizExistsAsync(int quizId);
}
