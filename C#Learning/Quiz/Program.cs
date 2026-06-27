using BusinessCore;
using BusinessCore.Interfaces;
using Common.DTO.User;
using Entity.Data;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Interfaces;

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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();