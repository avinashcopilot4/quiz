using BusinessCore;
using BusinessCore.Interfaces;
using Entity.Data;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Interfaces;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<SchoolDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"))
                .EnableSensitiveDataLogging()   
                .LogTo(Console.WriteLine, LogLevel.Information)); 

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IQuizRepository, QuizRepository>();
builder.Services.AddScoped<IQuizService, QuizService>();

builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IQuestionService, QuestionService>();

builder.Services.AddScoped<IAttemptRepository, AttemptRepository>();
builder.Services.AddScoped<IAttemptService, AttemptService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVite", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "https://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SchoolDbContext>();
    db.Database.EnsureCreated();

    if (!db.Users.Any())
    {
        db.Users.AddRange(
            new User { UserName = "Admin User", Email = "admin@example.com", Password = "admin123", Role = "admin", IsActive = true, Gender = "Other" },
            new User { UserName = "Employee User", Email = "employee@example.com", Password = "employee123", Role = "employee", IsActive = true, Gender = "Other" }
        );
    }

    if (!db.Quizzes.Any())
    {
        db.Quizzes.Add(new Quiz
        {
            Title = "Sample Quiz",
            Description = "A starter quiz for the integrated app.",
            Status = "Published",
            Questions = new List<Question>
            {
                new Question { QuestionText = "What is 2 + 2?", Option1 = "3", Option2 = "4", Option3 = "5", Option4 = "6", CorrectOption = 2, DisplayOrder = 1, IsActive = true },
                new Question { QuestionText = "Which planet is known as the Red Planet?", Option1 = "Venus", Option2 = "Mars", Option3 = "Jupiter", Option4 = "Mercury", CorrectOption = 2, DisplayOrder = 2, IsActive = true }
            }
        });
    }

    db.SaveChanges();
}

app.UseCors("AllowVite");

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.Run();