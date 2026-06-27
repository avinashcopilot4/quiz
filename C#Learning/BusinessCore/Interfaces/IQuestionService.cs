using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessCore.Interfaces;

public interface IQuestionService
{
    Task<IEnumerable<Question>> GetAllQuestionsAsync();
    Task<IEnumerable<Question>> GetQuestionsByQuizIdAsync(int quizId);
    Task<Question?> GetQuestionByIdAsync(int questionId);
    Task<Question> CreateQuestionAsync(Question question);
    Task UpdateQuestionAsync(Question question);
    Task DeleteQuestionAsync(int questionId);
    Task<bool> QuestionExistsAsync(int questionId);
}
