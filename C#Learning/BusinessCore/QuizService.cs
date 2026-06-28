using BusinessCore.Interfaces;
using Common.DTO.Quiz;
using Entity.Models;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessCore;

public class QuizService : IQuizService
{
    private readonly IQuizRepository _quizRepository;

    public QuizService(IQuizRepository quizRepository)
    {
        _quizRepository = quizRepository;
    }

    public async Task<IEnumerable<QuizDto>> GetAllQuizzesAsync()
    {
        var quizzes = await _quizRepository.GetAllAsync();
        return quizzes.Select(MapToDto).ToList();
    }

    public async Task<IEnumerable<QuizDto>> GetPublishedQuizzesAsync()
    {
        var quizzes = await _quizRepository.GetPublishedAsync();
        return quizzes.Select(MapToDto).ToList();
    }

    public async Task<QuizDto?> GetQuizByIdAsync(int quizId)
    {
        var quiz = await _quizRepository.GetByIdAsync(quizId);
        return quiz == null ? null : MapToDto(quiz);
    }

    public async Task<QuizDto> CreateQuizAsync(QuizDto quiz)
    {
        var entity = new Quiz
        {
            Title = quiz.Title,
            Description = quiz.Description,
            Status = string.IsNullOrWhiteSpace(quiz.Status) ? "Draft" : quiz.Status,
            IsDeleted = false,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            Questions = quiz.Questions?.Select(q => new Question
            {
                QuestionText = q.Text,
                Option1 = q.Options.ElementAtOrDefault(0) ?? string.Empty,
                Option2 = q.Options.ElementAtOrDefault(1) ?? string.Empty,
                Option3 = q.Options.ElementAtOrDefault(2) ?? string.Empty,
                Option4 = q.Options.ElementAtOrDefault(3) ?? string.Empty,
                CorrectOption = q.CorrectOptionIndex,
                DisplayOrder = 1,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            }).ToList() ?? new List<Question>()
        };

        await _quizRepository.AddAsync(entity);
        return MapToDto(entity);
    }

    public async Task UpdateQuizAsync(QuizDto quiz)
    {
        var existing = await _quizRepository.GetByIdAsync(int.Parse(quiz.Id));
        if (existing == null)
        {
            throw new KeyNotFoundException("Quiz not found");
        }

        existing.Title = quiz.Title;
        existing.Description = quiz.Description;
        existing.Status = string.IsNullOrWhiteSpace(quiz.Status) ? existing.Status : quiz.Status;
        existing.UpdatedDate = DateTime.UtcNow;

        // Sync questions: update existing, add new, remove missing
        var incoming = quiz.Questions ?? new List<QuestionDto>();
        var incomingIds = incoming.Where(q => q.Id > 0).Select(q => q.Id).ToHashSet();

        // Update or add
        foreach (var qDto in incoming)
        {
            if (qDto.Id > 0)
            {
                var found = existing.Questions.FirstOrDefault(x => x.QuestionId == qDto.Id);
                if (found != null)
                {
                    found.QuestionText = qDto.Text;
                    found.Option1 = qDto.Options.ElementAtOrDefault(0) ?? string.Empty;
                    found.Option2 = qDto.Options.ElementAtOrDefault(1) ?? string.Empty;
                    found.Option3 = qDto.Options.ElementAtOrDefault(2) ?? string.Empty;
                    found.Option4 = qDto.Options.ElementAtOrDefault(3) ?? string.Empty;
                    found.CorrectOption = qDto.CorrectOptionIndex;
                    found.IsActive = true;
                }
            }
            else
            {
                existing.Questions.Add(new Question
                {
                    QuestionText = qDto.Text,
                    Option1 = qDto.Options.ElementAtOrDefault(0) ?? string.Empty,
                    Option2 = qDto.Options.ElementAtOrDefault(1) ?? string.Empty,
                    Option3 = qDto.Options.ElementAtOrDefault(2) ?? string.Empty,
                    Option4 = qDto.Options.ElementAtOrDefault(3) ?? string.Empty,
                    CorrectOption = qDto.CorrectOptionIndex,
                    DisplayOrder = 1,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow
                });
            }
        }

        // Remove questions not present in incoming list
        var toRemove = existing.Questions.Where(q => q.QuestionId > 0 && !incomingIds.Contains(q.QuestionId)).ToList();
        foreach (var r in toRemove)
        {
            existing.Questions.Remove(r);
        }

        await _quizRepository.UpdateAsync(existing);
    }

    public Task DeleteQuizAsync(int quizId)
    {
        return _quizRepository.DeleteAsync(quizId);
    }

    public Task<bool> QuizExistsAsync(int quizId)
    {
        return _quizRepository.ExistsAsync(quizId);
    }

    private static QuizDto MapToDto(Quiz quiz)
    {
        return new QuizDto
        {
            Id = quiz.QuizId.ToString(),
            Title = quiz.Title,
            Description = quiz.Description,
            Topic = "General",
            Language = "English",
            Difficulty = "Intermediate",
            Tags = new List<string>(),
            CreatedBy = "admin",
            Author = "Admin",
            QuestionCount = quiz.Questions?.Count ?? 0,
            TimeLimit = 10,
            PassingScore = 70,
            Status = quiz.Status ?? "draft",
            CreatedAt = quiz.CreatedDate,
            UpdatedAt = quiz.UpdatedDate,
            Questions = quiz.Questions?.Select(q => new QuestionDto
            {
                Id = q.QuestionId,
                Text = q.QuestionText,
                Options = new List<string> { q.Option1, q.Option2, q.Option3, q.Option4 },
                CorrectOptionIndex = q.CorrectOption,
            }).ToList() ?? new List<QuestionDto>(),
        };
    }
}
