using Entity.Data;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories;

public class QuizRepository : IQuizRepository
{
    private readonly SchoolDbContext _context;

    public QuizRepository(SchoolDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Quiz>> GetAllAsync()
    {
        return await _context.Quizzes.Include(q => q.Questions).ToListAsync();
    }

    public async Task<IEnumerable<Quiz>> GetPublishedAsync()
    {
        return await _context.Quizzes
            .Include(q => q.Questions)
            .Where(q => q.Status != null && q.Status.ToLower() == "published")
            .ToListAsync();
    }

    public async Task<Quiz?> GetByIdAsync(int quizId)
    {
        return await _context.Quizzes
            .Include(q => q.Questions)
            .FirstOrDefaultAsync(q => q.QuizId == quizId);
    }

    public async Task AddAsync(Quiz quiz)
    {
        _context.Quizzes.Add(quiz);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Quiz quiz)
    {
        _context.Quizzes.Update(quiz);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int quizId)
    {
        var quiz = await _context.Quizzes.FindAsync(quizId);
        if (quiz != null)
        {
            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int quizId)
    {
        return await _context.Quizzes.AnyAsync(q => q.QuizId == quizId);
    }
}
