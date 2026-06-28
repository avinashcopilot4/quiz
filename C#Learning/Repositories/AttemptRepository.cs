using Entity.Data;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories;

public class AttemptRepository : IAttemptRepository
{
    private readonly SchoolDbContext _context;

    public AttemptRepository(SchoolDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<QuizAttempt>> GetAllAsync()
    {
        return await _context.QuizAttempts
            .Include(a => a.Quiz)
            .Include(a => a.User)
            .Include(a => a.AttemptDetails)
            .ToListAsync();
    }

    public async Task<IEnumerable<QuizAttempt>> GetByEmployeeIdAsync(int userId)
    {
        return await _context.QuizAttempts
            .Include(a => a.Quiz)
            .Include(a => a.AttemptDetails)
            .Where(a => a.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<QuizAttempt>> GetByQuizIdAsync(int quizId)
    {
        return await _context.QuizAttempts
            .Include(a => a.User)
            .Include(a => a.AttemptDetails)
            .Where(a => a.QuizId == quizId)
            .ToListAsync();
    }

    public async Task<QuizAttempt?> GetByIdAsync(int attemptId)
    {
        return await _context.QuizAttempts
            .Include(a => a.Quiz)
            .Include(a => a.User)
            .Include(a => a.AttemptDetails)
            .FirstOrDefaultAsync(a => a.AttemptId == attemptId);
    }

    public async Task AddAsync(QuizAttempt attempt)
    {
        _context.QuizAttempts.Add(attempt);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int attemptId)
    {
        return await _context.QuizAttempts.AnyAsync(a => a.AttemptId == attemptId);
    }
}
