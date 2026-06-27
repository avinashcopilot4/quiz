using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Interfaces;

public interface IAttemptRepository
{
    Task<IEnumerable<QuizAttempt>> GetAllAsync();
    Task<IEnumerable<QuizAttempt>> GetByEmployeeIdAsync(int userId);
    Task<IEnumerable<QuizAttempt>> GetByQuizIdAsync(int quizId);
    Task<QuizAttempt?> GetByIdAsync(int attemptId);
    Task AddAsync(QuizAttempt attempt);
    Task<bool> ExistsAsync(int attemptId);
}
