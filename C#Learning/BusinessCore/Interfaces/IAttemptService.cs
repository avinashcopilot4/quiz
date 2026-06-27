using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessCore.Interfaces;

public interface IAttemptService
{
    Task<IEnumerable<QuizAttempt>> GetAllAttemptsAsync();
    Task<IEnumerable<QuizAttempt>> GetAttemptsByEmployeeAsync(int userId);
    Task<IEnumerable<QuizAttempt>> GetAttemptsByQuizAsync(int quizId);
    Task<QuizAttempt?> GetAttemptByIdAsync(int attemptId);
    Task<QuizAttempt> CreateAttemptAsync(QuizAttempt attempt);
    Task<bool> AttemptExistsAsync(int attemptId);
}
