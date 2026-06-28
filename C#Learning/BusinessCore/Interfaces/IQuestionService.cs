using Common.DTO.Quiz;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessCore.Interfaces;

public interface IQuestionService
{
    Task<IEnumerable<QuestionDto>> GetAllQuestionsAsync();
    Task<IEnumerable<QuestionDto>> GetQuestionsByQuizIdAsync(int quizId);
    Task<QuestionDto?> GetQuestionByIdAsync(int questionId);
    Task<QuestionDto> CreateQuestionAsync(Question question);
    Task UpdateQuestionAsync(Question question);
    Task DeleteQuestionAsync(int questionId);
    Task<bool> QuestionExistsAsync(int questionId);
}
