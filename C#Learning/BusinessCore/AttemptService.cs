using BusinessCore.Interfaces;
using Entity.Models;
using Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessCore;

public class AttemptService : IAttemptService
{
    private readonly IAttemptRepository _attemptRepository;

    public AttemptService(IAttemptRepository attemptRepository)
    {
        _attemptRepository = attemptRepository;
    }

    public Task<IEnumerable<QuizAttempt>> GetAllAttemptsAsync()
    {
        return _attemptRepository.GetAllAsync();
    }

    public Task<IEnumerable<QuizAttempt>> GetAttemptsByEmployeeAsync(int userId)
    {
        return _attemptRepository.GetByEmployeeIdAsync(userId);
    }

    public Task<IEnumerable<QuizAttempt>> GetAttemptsByQuizAsync(int quizId)
    {
        return _attemptRepository.GetByQuizIdAsync(quizId);
    }

    public Task<QuizAttempt?> GetAttemptByIdAsync(int attemptId)
    {
        return _attemptRepository.GetByIdAsync(attemptId);
    }

    public async Task<QuizAttempt> CreateAttemptAsync(QuizAttempt attempt)
    {
        await _attemptRepository.AddAsync(attempt);
        return attempt;
    }

    public Task<bool> AttemptExistsAsync(int attemptId)
    {
        return _attemptRepository.ExistsAsync(attemptId);
    }
}
