using Entity.Data;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly SchoolDbContext _context;

    public QuestionRepository(SchoolDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Question>> GetAllAsync()
    {
        return await _context.Questions.ToListAsync();
    }

    public async Task<IEnumerable<Question>> GetByQuizIdAsync(int quizId)
    {
        return await _context.Questions.Where(q => q.QuizId == quizId).ToListAsync();
    }

    public async Task<Question?> GetByIdAsync(int questionId)
    {
        return await _context.Questions.FindAsync(questionId);
    }

    public async Task AddAsync(Question question)
    {
        _context.Questions.Add(question);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Question question)
    {
        _context.Questions.Update(question);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int questionId)
    {
        var question = await _context.Questions.FindAsync(questionId);
        if (question != null)
        {
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int questionId)
    {
        return await _context.Questions.AnyAsync(q => q.QuestionId == questionId);
    }
}
