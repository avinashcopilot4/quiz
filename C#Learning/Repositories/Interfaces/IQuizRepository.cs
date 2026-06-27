using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Interfaces;

public interface IQuizRepository
{
    Task<IEnumerable<Quiz>> GetAllAsync();
    Task<IEnumerable<Quiz>> GetPublishedAsync();
    Task<Quiz?> GetByIdAsync(int quizId);
    Task AddAsync(Quiz quiz);
    Task UpdateAsync(Quiz quiz);
    Task DeleteAsync(int quizId);
    Task<bool> ExistsAsync(int quizId);
}
