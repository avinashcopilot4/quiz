using BusinessCore.Interfaces;
using Common.DTO.Attempt;
using Entity.Models;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessCore;

public class AttemptService : IAttemptService
{
    private readonly IAttemptRepository _attemptRepository;

    public AttemptService(IAttemptRepository attemptRepository)
    {
        _attemptRepository = attemptRepository;
    }

    public async Task<IEnumerable<AttemptDto>> GetAllAttemptsAsync()
    {
        var attempts = await _attemptRepository.GetAllAsync();
        return attempts.Select(MapToDto).ToList();
    }

    public async Task<IEnumerable<AttemptDto>> GetAttemptsByEmployeeAsync(int userId)
    {
        var attempts = await _attemptRepository.GetByEmployeeIdAsync(userId);
        return attempts.Select(MapToDto).ToList();
    }

    public async Task<IEnumerable<AttemptDto>> GetAttemptsByQuizAsync(int quizId)
    {
        var attempts = await _attemptRepository.GetByQuizIdAsync(quizId);
        return attempts.Select(MapToDto).ToList();
    }

    public async Task<AttemptDto?> GetAttemptByIdAsync(int attemptId)
    {
        var attempt = await _attemptRepository.GetByIdAsync(attemptId);
        return attempt == null ? null : MapToDto(attempt);
    }

    public async Task<AttemptDto> CreateAttemptAsync(AttemptSubmissionDto request)
    {
        var attempt = new QuizAttempt
        {
            UserId = request.UserId,
            QuizId = request.QuizId,
            Score = request.Score,
            MaxPossibleScore = Math.Max(request.MaxPossibleScore, Math.Max(request.AttemptDetails.Count, request.Score)),
            AttemptedQuestions = request.AttemptedQuestions > 0 ? request.AttemptedQuestions : request.AttemptDetails.Count,
            CompletedDate = request.CompletedDate ?? DateTime.UtcNow,
            AttemptDetails = request.AttemptDetails.Select(detail => new AttemptDetail
            {
                QuestionId = detail.QuestionId,
                UserAnswer = detail.UserAnswer,
                CorrectAnswer = detail.CorrectAnswer,
                IsCorrect = detail.IsCorrect,
            }).ToList(),
        };

        await _attemptRepository.AddAsync(attempt);
        return MapToDto(attempt);
    }

    public Task<bool> AttemptExistsAsync(int attemptId)
    {
        return _attemptRepository.ExistsAsync(attemptId);
    }

    private static AttemptDto MapToDto(QuizAttempt attempt)
    {
        return new AttemptDto
        {
            Id = attempt.AttemptId.ToString(),
            QuizId = attempt.QuizId.ToString(),
            EmployeeId = attempt.UserId.ToString(),
            Answers = attempt.AttemptDetails?.Select(detail => new AnswerDto
            {
                QuestionId = detail.QuestionId,
                SelectedOptionIndex = detail.UserAnswer,
            }).ToList() ?? new List<AnswerDto>(),
            Score = attempt.Score,
            StartedAt = attempt.CompletedDate,
            SubmittedAt = attempt.CompletedDate,
        };
    }
}
