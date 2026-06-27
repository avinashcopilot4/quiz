using Entity.Data;
using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace QuizApp.Data;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<SchoolDbContext>();

        await context.Database.EnsureCreatedAsync();

        if (await context.Users.AnyAsync())
        {
            return;
        }

        var users = new List<User>
        {
            new()
            {
                UserName = "Admin User",
                Email = "admin@example.com",
                Password = "admin123",
                Gender = "Other",
                IsActive = true,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
            },
            new()
            {
                UserName = "Employee User",
                Email = "employee@example.com",
                Password = "employee123",
                Gender = "Other",
                IsActive = true,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
            }
        };

        context.Users.AddRange(users);
        await context.SaveChangesAsync();

        // Add roles for users
        var userRoles = new List<UserRole>
        {
            new() { UserId = users[0].UserId, RoleName = "admin", CreatedDate = DateTime.UtcNow },
            new() { UserId = users[0].UserId, RoleName = "employee", CreatedDate = DateTime.UtcNow },
            new() { UserId = users[1].UserId, RoleName = "employee", CreatedDate = DateTime.UtcNow }
        };

        context.UserRoles.AddRange(userRoles);
        await context.SaveChangesAsync();

        var quizzes = new List<Quiz>
        {
            CreateQuiz(
                "Basic React Quiz",
                "A short quiz to test basic React and JavaScript knowledge.",
                "published",
                "React",
                "English",
                "Beginner",
                new[] { "react", "javascript", "frontend" },
                new[]
                {
                    CreateQuestion("What is the primary purpose of React?", new[] { "To build user interfaces", "To manage database queries", "To style HTML pages", "To create backend APIs" }, 1),
                    CreateQuestion("Which hook is used to add state in a functional component?", new[] { "useEffect", "useState", "useMemo", "useRef" }, 2),
                    CreateQuestion("Which element attribute is used to render dynamic lists in React?", new[] { "key", "id", "className", "data-index" }, 1),
                    CreateQuestion("What does JSX compile to?", new[] { "HTML", "JavaScript objects", "CSS rules", "JSON" }, 2)
                }),
            CreateQuiz(
                "JavaScript Fundamentals",
                "A quiz covering core JavaScript concepts and syntax.",
                "published",
                "JavaScript",
                "English",
                "Beginner",
                new[] { "javascript", "syntax", "basics" },
                new[]
                {
                    CreateQuestion("What is the result of typeof null in JavaScript?", new[] { "object", "null", "undefined", "number" }, 1),
                    CreateQuestion("Which keyword creates a block-scoped variable?", new[] { "var", "let", "const", "define" }, 2),
                    CreateQuestion("Which method converts JSON to an object?", new[] { "JSON.stringify", "JSON.parse", "Object.fromJSON", "JSON.object" }, 2),
                    CreateQuestion("What does === compare in JavaScript?", new[] { "Value only", "Type only", "Value and type", "Reference only" }, 3)
                }),
            CreateQuiz(
                "TypeScript Basics",
                "Test your understanding of TypeScript types and tooling.",
                "published",
                "TypeScript",
                "English",
                "Intermediate",
                new[] { "typescript", "types", "static typing" },
                new[]
                {
                    CreateQuestion("What type does Array<string> represent?", new[] { "Object", "String", "Array of strings", "Tuple" }, 3),
                    CreateQuestion("Which keyword defines a contract for object shapes?", new[] { "type", "interface", "class", "namespace" }, 2),
                    CreateQuestion("How do you make a property optional?", new[] { "prop?", "optional prop", "prop|undefined", "prop!" }, 1),
                    CreateQuestion("What does unknown represent?", new[] { "Any value", "Values that are not known yet", "Safe alternative to any", "A string value" }, 3)
                }),
            CreateQuiz(
                "HTML & CSS Essentials",
                "A review of semantic HTML and stylesheet fundamentals.",
                "published",
                "Web Design",
                "English",
                "Beginner",
                new[] { "html", "css", "web" },
                new[]
                {
                    CreateQuestion("Which HTML tag is used for the largest heading?", new[] { "<h1>", "<heading>", "<title>", "<header>" }, 1),
                    CreateQuestion("What CSS property changes text color?", new[] { "font-color", "text-color", "color", "fill" }, 3),
                    CreateQuestion("Which CSS display value makes an element a block?", new[] { "inline", "block", "flex", "grid" }, 2)
                }),
            CreateQuiz(
                "React Hooks Deep Dive",
                "Questions about hooks, state, and lifecycle in React.",
                "published",
                "React",
                "English",
                "Intermediate",
                new[] { "react", "hooks", "state" },
                new[]
                {
                    CreateQuestion("Which hook is used for side effects in React?", new[] { "useMemo", "useEffect", "useCallback", "useState" }, 2),
                    CreateQuestion("What does useState return?", new[] { "Two values: state and setter", "A single state value", "A function only", "An object" }, 1),
                    CreateQuestion("Which hook memoizes a function?", new[] { "useMemo", "useCallback", "useRef", "useReducer" }, 2),
                    CreateQuestion("How do you preserve a value across renders?", new[] { "useState", "useRef", "useMemo", "useEffect" }, 2)
                }),
            CreateQuiz(
                "Node.js Backend Basics",
                "Assessment of server-side JavaScript and Node runtime concepts.",
                "published",
                "Node.js",
                "English",
                "Intermediate",
                new[] { "node", "backend", "javascript" },
                new[]
                {
                    CreateQuestion("Which module is used to create an HTTP server in Node?", new[] { "http", "server", "express", "network" }, 1),
                    CreateQuestion("What does npm stand for?", new[] { "Node Package Manager", "Node Process Manager", "New Product Model", "Network Package Manager" }, 1),
                    CreateQuestion("Which value is returned by fs.readFileSync?", new[] { "Callback", "Promise", "Buffer or string", "Array" }, 3),
                    CreateQuestion("Which file is commonly used to declare Node project dependencies?", new[] { "package.json", "tsconfig.json", ".env", "README.md" }, 1)
                }),
            CreateQuiz(
                "Database Fundamentals",
                "Questions about SQL, data modeling, and query basics.",
                "published",
                "Databases",
                "English",
                "Beginner",
                new[] { "database", "sql", "data" },
                new[]
                {
                    CreateQuestion("What does SQL stand for?", new[] { "Structured Query Language", "Simple Query Language", "Sequential Query Language", "Standard Query Link" }, 1),
                    CreateQuestion("Which clause filters rows from a query result?", new[] { "ORDER BY", "GROUP BY", "WHERE", "HAVING" }, 3),
                    CreateQuestion("What is a primary key?", new[] { "A unique row identifier", "A column to sort by", "A related table key", "A text index" }, 1)
                }),
            CreateQuiz(
                "Git & Version Control",
                "A quiz on Git commands, branches, and workflows.",
                "published",
                "DevOps",
                "English",
                "Beginner",
                new[] { "git", "version control", "workflow" },
                new[]
                {
                    CreateQuestion("Which command creates a new Git branch?", new[] { "git new branch", "git branch", "git checkout", "git init" }, 2),
                    CreateQuestion("How do you stage files for commit?", new[] { "git stage", "git add", "git commit", "git push" }, 2),
                    CreateQuestion("Which command shows the commit history?", new[] { "git status", "git log", "git history", "git show" }, 2),
                    CreateQuestion("What is a merge conflict?", new[] { "A failed push", "A branch error", "Overlapping changes to the same file", "A network issue" }, 3)
                }),
            CreateQuiz(
                "Testing JavaScript Apps",
                "A quick quiz on writing tests and verifying app behavior.",
                "draft",
                "Testing",
                "English",
                "Intermediate",
                new[] { "testing", "javascript", "qa" },
                new[]
                {
                    CreateQuestion("Which library is commonly used for unit testing React components?", new[] { "Jest", "Chai", "Enzyme", "React Test Library" }, 4),
                    CreateQuestion("What does E2E testing validate?", new[] { "Individual functions", "User workflows across the app", "CSS styles only", "Network latency" }, 2),
                    CreateQuestion("What is a mock in testing?", new[] { "A fake dependency", "A real database", "A build script", "A lint rule" }, 1)
                }),
            CreateQuiz(
                "Web Performance Optimization",
                "Questions about making web apps faster and more responsive.",
                "published",
                "Performance",
                "English",
                "Advanced",
                new[] { "performance", "web", "optimization" },
                new[]
                {
                    CreateQuestion("Which technique helps reduce initial page load time?", new[] { "Bundling all files into one large asset", "Lazy loading non-critical resources", "Using inline styles everywhere", "Disabling caching" }, 2),
                    CreateQuestion("What does the browser cache help with?", new[] { "Server-side rendering", "Slower updates", "Faster repeat loads", "Database queries" }, 3),
                    CreateQuestion("Which image format typically offers the best compression for the web?", new[] { "BMP", "PNG", "GIF", "WebP" }, 4),
                    CreateQuestion("What is one benefit of code splitting?", new[] { "More CSS files", "Larger initial download", "Smaller initial bundle", "Slower route transitions" }, 3)
                })
        };

        context.Quizzes.AddRange(quizzes);
        await context.SaveChangesAsync();
    }

    private static Quiz CreateQuiz(string title, string description, string status, string topic, string language, string difficulty, IEnumerable<string> tags, IEnumerable<Question> questions)
    {
        return new Quiz
        {
            Title = title,
            Description = description,
            Status = status,
            IsDeleted = false,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            Questions = questions.ToList()
        };
    }

    private static Question CreateQuestion(string text, IEnumerable<string> options, int correctOptionIndex)
    {
        var optionList = options.ToArray();
        return new Question
        {
            QuestionText = text,
            Option1 = optionList[0],
            Option2 = optionList[1],
            Option3 = optionList[2],
            Option4 = optionList[3],
            CorrectOption = correctOptionIndex,
            DisplayOrder = 1,
            IsActive = true,
            CreatedDate = DateTime.UtcNow,
        };
    }
}
