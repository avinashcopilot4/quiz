using Common.DTO.Attempt;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessCore.Interfaces;

public interface IAttemptService
{
    Task<IEnumerable<AttemptDto>> GetAllAttemptsAsync();
    Task<IEnumerable<AttemptDto>> GetAttemptsByEmployeeAsync(int userId);
    Task<IEnumerable<AttemptDto>> GetAttemptsByQuizAsync(int quizId);
    Task<AttemptDto?> GetAttemptByIdAsync(int attemptId);
    Task<AttemptDto> CreateAttemptAsync(AttemptSubmissionDto request);
    Task<bool> AttemptExistsAsync(int attemptId);
}
