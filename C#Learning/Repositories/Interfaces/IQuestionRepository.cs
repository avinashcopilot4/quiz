using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Interfaces;

public interface IQuestionRepository
{
    Task<IEnumerable<Question>> GetAllAsync();
    Task<IEnumerable<Question>> GetByQuizIdAsync(int quizId);
    Task<Question?> GetByIdAsync(int questionId);
    Task AddAsync(Question question);
    Task UpdateAsync(Question question);
    Task DeleteAsync(int questionId);
    Task<bool> ExistsAsync(int questionId);
}
