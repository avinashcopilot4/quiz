//// ============================================================
//// CONCEPT: Variables & Data Types
//// ============================================================
//// C# is a statically typed language — every variable must have
//// a declared type before it can be used.
//// ============================================================

//using System;

//class VariablesAndDataTypes
//{
//    static void Main()
//    {
//        //0 1 2 3 - 255
//        //|              256
//        //256
//        //    -64,768  64767    64768 -> -64768
//        // --- Integer Types ---
//        byte b = 255;           // 0 to 255
//        short s = 32000;         // -32,768 to 32,767
//        int i = 2_000_000;     // most common integer type
//        long l = 9_000_000_000L; // very large numbers

//        // --- Floating Point Types ---
//        float f = 3.14f;         // ~7 digits precision  (suffix f required)
//        double d = 3.14159265358; // ~15 digits precision (default for decimals)
//        decimal m = 19.99m;        // exact precision — use for money (suffix m required)

//        // --- Text & Character ---
//        char c = 'A';         // single character, uses single quotes
//        string name = "Avinash";   // sequence of characters, uses double quotes

//        // --- Boolean ---
//        bool isLearning = true;    // true or false only

//        // --- var keyword (type inference) ---
//        var city = "Hyderabad"; // compiler infers: string
//        var year = 2026;        // compiler infers: int
//        var pi = 3.14;        // compiler infers: double

//        // --- Nullable types (can hold null) ---
//        int? maybeAge = null;      // int? means "int or null"
//        maybeAge = 25;

//        // --- Constants (value can never change) ---
//        const double Gravity = 9.8;

//        // --- String interpolation ---
//        string str = "Hello, " + name + " ";
//        Console.WriteLine($"Hello, {""}! Year: {year}, Gravity: {Gravity}");
//        Console.Write("");
//        // --- Type conversion ---
//        //1)Implict
//        //2)Explict

//        int n1 = 1000;
//        long n2 = n1;
//        //int -> long

//        long n3 = 2000001; //20 is valid
//        int n4 = (int)n3;
//        Console.WriteLine(n4);

//        int x = 42;
//        double xAsDouble = (double)x;     // explicit cast
//        int backToInt = (int)xAsDouble; // truncates decimal part

//        string numStr = "100";
//        int parsed = int.Parse(numStr);        // throws if invalid
//        bool success = int.TryParse("abc", out int result); // safe parse

//        Console.WriteLine($"Parsed: {parsed}, TryParse succeeded: {success}");
//    }
//}



//// ============================================================
//// CONCEPT: Control Flow — if/else, switch, loops
//// ============================================================

//using System;

//class ControlFlow
//{
//    static void Main()
//    {
//        // ---- if / else if / else ----
//        int score = 85;

//        if (score >= 90)
//            Console.WriteLine("Grade: A");
//        else if (score >= 75)
//            Console.WriteLine("Grade: B");   // <-- this prints
//        else
//            Console.WriteLine("Grade: C");

//        // ---- Ternary operator (compact if/else) ----
//        string result = score >= 50 ? "Pass" : "Fail";
//        Console.WriteLine(result);  // Pass

//        // ---- switch statement ----
//        string day = "Monday";
//        switch (day)
//        {
//            case "Saturday":
//            case "Sunday":
//                Console.WriteLine("Weekend!");
//                break;
//            case "Monday":
//                Console.WriteLine("Start of work week.");  // <-- this prints
//                break;
//            default:
//                Console.WriteLine("Weekday.");
//                break;
//        }

//        // ---- switch EXPRESSION (modern C# style) ----
//        int dayNum = 3;
//        string dayName = dayNum switch
//        {
//            1 => "Monday",
//            2 => "Tuesday",
//            3 => "Wednesday",   // <-- matched
//            4 => "Thursday",
//            5 => "Friday",
//            _ => "Weekend"  // default
//        };
//        Console.WriteLine(dayName);  // Wednesday

//        // ---- for loop ----
//        for (int i = 1; i <= 5; i++)
//        {
//            Console.Write(i + " ");   // 1 2 3 4 5
//        }
//            //Console.Write(i + " ");   // 1 2 3 4 5
//        Console.WriteLine();

//        // ---- while loop ----
//        int count = 0;
//        while (count < 3)
//        {
//            Console.WriteLine($"while count: {count}");
//            count++;
//        }

//        // ---- do-while (runs at least once) ----
//        int n = 0;
//        do
//        {
//            Console.WriteLine($"do-while n: {n}");
//            n++;
//        } while (n < 2);

//        // ---- foreach loop (best for collections) ----
//        string[] fruits = { "Apple", "Banana", "Cherry" };
//        foreach (string fruit in fruits)
//            Console.WriteLine(fruit);

//        // ---- break and continue ----
//        for (int i = 0; i < 10; i++)
//        {
//            if (i == 3) continue;  // skip 3
//            if (i == 6) break;     // stop at 6
//            Console.Write(i + " "); // 0 1 2 4 5
//        }
//        Console.WriteLine();
//    }
//}

//// ============================================================
//// CONCEPT: Methods (Functions)
//// ============================================================
//// Methods are reusable blocks of code. In C# every method
//// belongs to a class.
//// ============================================================

//using System;

//class Methods
//{
//    static void Main()
//    {
//        // --- Calling methods ---
//        SayHello();
//        Greet("Avinash");

//        int sum = Add(10, 20);
//        Console.WriteLine($"Sum: {sum}");

//        // --- Optional parameters ---
//        PrintInfo("Alice");           // uses default age
//        PrintInfo("Bob", 30);         // overrides default

//        // --- Named arguments (order doesn't matter) ---
//        PrintInfo(age: 25, name: "Charlie");

//        // --- out parameter (return multiple values) ---
//        int quotient = 0;
//        Divide(quotient, quotient, out quotient, out int remainder);
//        Console.WriteLine($"10 / 3 = {quotient} remainder {remainder}");

//        // --- Method overloading (same name, different params) ---
//        Console.WriteLine(Multiply(3, 4));         // int version
//        Console.WriteLine(Multiply(3.0, 4.0));    // double version

//        // --- Expression-bodied method (compact syntax) ---
//        Console.WriteLine(Square(7));

//        // --- Local function (method inside a method) ---
//        int LocalAdd(int a, int b) => a + b;
//        Console.WriteLine(LocalAdd(5, 6));

//        // --- Params array (variable number of arguments) ---
//        Console.WriteLine(Sum(1, 2, 3, 4, 5));
//    }

//    // void = returns nothing
//    static void SayHello()
//    {
//        Console.WriteLine("Hello, C#!");
//    }

//    // string parameter
//    static void Greet(string name)
//    {
//        Console.WriteLine($"Hello, {name}!");
//    }

//    // returns an int

//    static int Add(int a, int b)
//    {
//        return a + b;
//    }

//    // optional parameter (has a default value)
//    static void PrintInfo(string name, int age = 18)
//    {
//        Console.WriteLine($"{name} is {age} years old.");
//    }
//    //printInfo("Tanvi",23);
//    //printInfo("Avinash);

//    // out parameters let a method return multiple values


//    static void Divide(int dividend, int divisor, out int quotient, out int remainder)
//    {
//        dividend = 15;
//        quotient = dividend / divisor;

//        remainder = dividend % divisor;
//    }

//    //Polymorphism - Many forms
//    // 1) OverLoading differing in means of 
//    // a) No of Parameters
//    // b) Order of Parameters
//    // c) Type of Parameters

//    // 2) Overriding


//    // overload 1: multiplies two ints
//    static int Multiply(int a, int b) =>  a* b;
//    static int Multiply(int a, int b, int c) => a * b * c;

//    static string Repeat(string str, int b) => string.Concat(Enumerable.Repeat(str, b));
//    static string Repeat(int b, string str) => string.Concat(Enumerable.Repeat(str+" ", b));

//    // overload 2: multiplies two doubles
//    static double Multiply(double a, double b) => a * b;

//    // expression-bodied method (single expression after =>)
//    static int Square(int n) => n * n;

//    // params: accepts any number of ints
//    static int Sum(params int[] numbers)
//    {
//        int total = 0;
//        foreach (int n in numbers) total += n;
//        return total;
//    }
//}


//// ============================================================
//// CONCEPT: Arrays & Strings
//// ============================================================

//using System;

//class ArraysAndStrings
//{
//    static void Main()
//    {
//        // ==============================
//        // ARRAYS
//        // ==============================

//        // Declare and initialize
//        int[] numbers = { 10, 20, 30, 40, 50 };

//        // Access by index (zero-based)
//        Console.WriteLine(numbers[0]);   // 10
//        Console.WriteLine(numbers[4]);   // 50

//        // Length
//        Console.WriteLine(numbers.Length); // 5

//        // Modify an element
//        numbers[2] = 99;

//        // Loop through
//        foreach (int n in numbers)
//            Console.Write(n + " ");
//        Console.WriteLine();

//        // Array methods (System.Array)
//        Array.Sort(numbers);
//        Array.Reverse(numbers);
//        int idx = Array.IndexOf(numbers, 20); // find position

//        // Multi-dimensional array (2D grid)
//        int[,] matrix = {
//            { 1, 2, 3 },
//            { 4, 5, 6 }
//        };
//        Console.WriteLine(matrix[1, 2]); // 6

//        // Jagged array (array of arrays, different lengths)
//        int[][] jagged = new int[3][];
//        jagged[0] = new int[] { 1, 2 };
//        jagged[1] = new int[] { 3, 4, 5 };
//        jagged[2] = new int[] { 6 };

//        // ==============================
//        // STRINGS
//        // ==============================

//        string s = "Hello, Avinash!";

//        // Length
//        Console.WriteLine(s.Length);          // 15

//        // Access character
//        Console.WriteLine(s[0]);              // H

//        // Common methods
//        Console.WriteLine(s.ToUpper());
//        Console.WriteLine(s.ToLower());
//        Console.WriteLine(s.Contains("Avinash")); // true
//        Console.WriteLine(s.StartsWith("Hello")); // true
//        Console.WriteLine(s.Replace("Avinash", "World"));
//        Console.WriteLine(s.Trim());              // removes whitespace from ends
//        Console.WriteLine(s.Substring(7, 7));     // "Avinash" (start, length)
//        Console.WriteLine(s.IndexOf("Avinash"));  // 7

//        // Split and Join
//        string csv = "apple,banana,cherry";
//        string[] fruits = csv.Split(',');
//        Console.WriteLine(fruits[1]);             // banana

//        string joined = string.Join(" | ", fruits);
//        Console.WriteLine(joined);                // apple | banana | cherry

//        // String interpolation (modern, preferred)
//       string name = "Avinash \\";  //Avinash \
//        int age = 25;
//        Console.WriteLine($"Name: {name}, Age: {age}");

//        // Verbatim string (ignores escape sequences)
//        string path = @"C:\Users\Avinash\Documents";
//        Console.WriteLine(path);

//        // String comparison (case-insensitive)
//        bool same = string.Equals("hello", "HELLO", StringComparison.OrdinalIgnoreCase);
//        Console.WriteLine(same); // true

//        // Check empty / null
//        string empty = "";
//        Console.WriteLine(string.IsNullOrEmpty(empty));      // true
//        Console.WriteLine(string.IsNullOrWhiteSpace("   ")); // true

//        empty = empty + "Tanvi";

//        // StringBuilder (efficient when concatenating in loops)
//        var sb = new System.Text.StringBuilder();
//        for (int i = 0; i < 5; i++)
//            sb.Append($"item{i} ");
//        Console.WriteLine(sb.ToString());
//    }
//}

//// ============================================================
//// CONCEPT: Classes & Objects (OOP Foundation)
//// ============================================================
//// A class is a blueprint. An object is an instance of that blueprint.
//// ============================================================

//using System;

//// ----- The Blueprint -----
//class Person
//{
//    // --- Fields (private data, convention: camelCase with _) ---
//    private string _name;
//    private int _age;

//    // --- Properties (public access to fields) ---
//    public string Name
//    {
//        get { return _name; }
//        set
//        {
//            if (string.IsNullOrWhiteSpace(value))
//                throw new ArgumentException("Name cannot be empty.");
//            _name = value;
//        }
//    }

//    public int Age
//    {
//        get { return _age; }
//        set
//        {
//            if (value < 0) throw new ArgumentOutOfRangeException("Age cannot be negative.");
//            _age = value;
//        }
//    }

//    // Auto-property (compiler generates the backing field automatically)
//    public string City { get; set; }

//    // Read-only auto-property (can only be set in constructor)
//    public DateTime DateOfBirth { get; }

//    // --- Constructors ---
//    // Default constructor
//    public Person()
//    {
//        _name = "Unknown";
//        _age = 0;
//        City = "Unknown";
//    }

//    // Parameterized constructor
//    public Person(string name, int age, string city)
//    {
//        Name = name;   // uses the property setter (with validation)
//        Age = age;
//        City = city;
//    }

//    // Constructor chaining with : this(...)
//    public Person(string name) : this(name, 18, "Unknown") { }

//    // --- Methods ---
//    public void Introduce()
//    {
//        Console.WriteLine($"Hi! I'm {Name}, {Age} years old, from {City}.");
//    }

//    public bool IsAdult() => Age >= 18;

//    // --- Static method (belongs to the class, not an instance) ---
//    public static Person CreateDefault() => new Person("Default", 0, "Unknown");

//    // --- ToString override (called when you print the object) ---
//    public override string ToString() => $"Person({Name}, {Age})";
//}


//// ----- Another class to show composition -----
//class BankAccount
//{
//    public string Owner { get; }
//    public decimal Balance { get; private set; }  // only this class can set it

//    public BankAccount(string owner, decimal initialBalance = 0)
//    {
//        Owner = owner;
//        Balance = initialBalance;
//    }

//    public void Deposit(decimal amount)
//    {
//        if (amount <= 0) throw new ArgumentException("Deposit must be positive.");
//        Balance += amount;
//        Console.WriteLine($"Deposited {amount:C}. New balance: {Balance:C}");
//    }

//    public void Withdraw(decimal amount)
//    {
//        if (amount > Balance) throw new InvalidOperationException("Insufficient funds.");
//        Balance -= amount;
//        Console.WriteLine($"Withdrew {amount:C}. New balance: {Balance:C}");
//    }
//}


//class ClassesAndObjects
//{
//    static void Main()
//    {
//        // Creating objects (instances)
//        Person p1 = new Person("Avinash", 25, "Hyderabad");
//        Person p2 = new Person("Priya");          // uses the (name) constructor
//        Person p3 = Person.CreateDefault();       // static factory method

//        p1.Introduce();
//        p2.Introduce();

//        Console.WriteLine(p1.IsAdult());   // true
//        Console.WriteLine(p1);            // calls ToString() -> Person(Avinash, 25)

//        // Modifying properties
//        p1.City = "Bangalore";
//        p1.Introduce();

//        // BankAccount demo
//        var account = new BankAccount("Avinash", 1000m);
//        account.Deposit(500m);
//        account.Withdraw(200m);
//        Console.WriteLine($"Final balance: {account.Balance:C}");

//        // Object initializer syntax (set properties inline)
//        var p4 = new Person("Rahul", 22, "Chennai") { City = "Mumbai" };
//        p4.Introduce();
//    }
//}

//// ============================================================
//// CONCEPT: Inheritance & Polymorphism
//// ============================================================
//// Inheritance: a child class reuses code from a parent class.
//// Polymorphism: the same method name behaves differently
////               depending on the object type.
//// ============================================================

//using System;

//// ----- Base class (parent) -----
//class Animal
//{
//    public string Name { get; set; }

//    public Animal(string name)
//    {
//        Name = name;
//    }

//    // virtual = can be overridden by subclasses
//    public virtual void Speak()
//    {
//        Console.WriteLine($"{Name} makes a sound.");
//    }

//    public virtual string Describe() => $"I am an animal named {Name}.";

//    // Non-virtual method — cannot be overridden
//    public void Breathe() => Console.WriteLine($"{Name} is breathing.");
//}

//// ----- Derived class (child) -----
//class Dog : Animal   // Dog inherits from Animal
//{
//    public string Breed { get; set; }

//    // : base(name) calls the parent constructor
//    public Dog(string name, string breed) : base(name)
//    {
//        Breed = breed;
//    }

//    // override: replaces the parent's implementation
//    public override void Speak()
//    {
//        Console.WriteLine($"{Name} says: Woof!");
//    }

//    public override string Describe() => $"I am {Name}, a {Breed} dog.";

//    public void Fetch() => Console.WriteLine($"{Name} fetches the ball!");
//}

//class Cat : Animal
//{
//    public Cat(string name) : base(name) { }

//    public override void Speak()
//    {
//        Console.WriteLine($"{Name} says: Meow!");
//    }
//}

//// ----- Abstract class (cannot be instantiated directly) -----
//abstract class Shape
//{
//    public string Color { get; set; } = "White";

//    // abstract method: subclass MUST implement it
//    public abstract double Area();

//    // concrete method (shared behavior)
//    public void Describe()
//    {
//        Console.WriteLine($"Shape color: {Color}, Area: {Area():F2}");
//    }
//}

//class Circle : Shape
//{
//    public double Radius { get; set; }
//    public Circle(double radius) { Radius = radius; }

//    public override double Area() => Math.PI * Radius * Radius;
//}

//class Rectangle : Shape
//{
//    public double Width { get; set; }
//    public double Height { get; set; }
//    public Rectangle(double w, double h) { Width = w; Height = h; }

//    public override double Area() => Width * Height;
//}


//class InheritanceAndPolymorphism
//{
//    static void Main()
//    {
//        // --- Basic inheritance ---
//        Dog dog = new Dog("Rex", "Labrador");
//        dog.Speak();     // overridden: "Rex says: Woof!"
//        dog.Breathe();   // inherited from Animal
//        dog.Fetch();     // Dog-specific method

//        // --- Polymorphism: treat different objects as the same base type ---
//        Animal[] animals = {
//            new Dog("Buddy", "Poodle"),
//            new Cat("Whiskers"),
//            new Dog("Max", "Husky")
//        };

//        foreach (Animal a in animals)
//        {
//            a.Speak();   // correct Speak() is called for each type at runtime
//        }

//        // --- Type checking ---
//        Animal a1 = new Dog("Coco", "Beagle");
//        if (a1 is Dog d)
//        {
//            Console.WriteLine($"It's a dog! Breed: {d.Breed}");
//        }

//        // --- Abstract class usage ---
//        Shape[] shapes = {
//            new Circle(5) { Color = "Red" },
//            new Rectangle(4, 6) { Color = "Blue" }
//        };

//        foreach (Shape s in shapes)
//            s.Describe();

//        // --- sealed class: cannot be inherited further ---
//        // sealed class FinalClass { }

//        // --- base keyword: call parent's method ---
//        // (demonstrated via: base.Speak() inside a child override)
//    }
//}


//// ============================================================
//// CONCEPT: Interfaces
//// ============================================================
//// An interface defines a CONTRACT — a set of methods/properties
//// that a class MUST implement. No implementation inside the interface.
//// A class can implement MULTIPLE interfaces (unlike inheritance).
//// ============================================================

//using System;
//using System.Collections.Generic;

//// ----- Define interfaces -----

//interface IMovable
//{
//    void Move(int x, int y);
//    int X { get; }
//    int Y { get; }
//}

//interface IDrawable
//{
//    void Draw();
//    string Color { get; set; }
//}

//interface IResizable
//{
//    void Resize(double factor);
//}

//// ----- Implement multiple interfaces -----

//class GameSprite : IMovable, IDrawable, IResizable
//{
//    public int X { get; private set; }
//    public int Y { get; private set; }
//    public string Color { get; set; } = "Green";
//    public double Size { get; private set; } = 1.0;
//    public string Name { get; }

//    public GameSprite(string name, int x, int y)
//    {
//        Name = name;
//        X = x;
//        Y = y;
//    }

//    public void Move(int x, int y)
//    {
//        X = x;
//        Y = y;
//        Console.WriteLine($"{Name} moved to ({X}, {Y})");
//    }

//    public void Draw()
//    {
//        Console.WriteLine($"Drawing {Name} [{Color}] at ({X},{Y}) size {Size}x");
//    }

//    public void Resize(double factor)
//    {
//        Size *= factor;
//        Console.WriteLine($"{Name} resized to {Size}x");
//    }
//}

//// ----- Practical interface example: IComparable -----

//class Product : IComparable<Product>
//{
//    public string Name { get; set; }
//    public decimal Price { get; set; }

//    public Product(string name, decimal price)
//    {
//        Name = name;
//        Price = price;
//    }

//    // IComparable: enables sorting
//    public int CompareTo(Product other)
//    {
//        return Price.CompareTo(other.Price); // sort by price ascending
//    }

//    public override string ToString() => $"{Name} ({Price:C})";
//}

//// ----- Interface with default implementation (C# 8+) -----

//interface ILogger
//{
//    void Log(string message);

//    // Default implementation (optional to override)
//    void LogError(string message) => Log($"[ERROR] {message}");
//}

//class ConsoleLogger : ILogger
//{
//    public void Log(string message) => Console.WriteLine($"[LOG] {message}");
//    // LogError is inherited from the interface default
//}

//// ----- Program -----

//class InterfacesDemo
//{
//    // Method accepts any IDrawable — doesn't care about the concrete type
//    static void DrawAll(IEnumerable<IDrawable> drawables)
//    {
//        foreach (var d in drawables)
//            d.Draw();
//    }

//    static void Main()
//    {
//        var sprite1 = new GameSprite("Hero", 0, 0);
//        var sprite2 = new GameSprite("Enemy", 10, 5) { Color = "Red" };

//        sprite1.Move(3, 7);
//        sprite1.Resize(2.0);
//        sprite1.Draw();

//        sprite2.Draw();

//        Console.WriteLine("\n--- Draw all via interface ---");
//        DrawAll(new IDrawable[] { sprite1, sprite2 });

//        Console.WriteLine("\n--- IComparable sort ---");
//        var products = new List<Product>
//        {
//            new Product("Laptop",  75000m),
//            new Product("Mouse",    500m),
//            new Product("Monitor", 20000m),
//        };
//        products.Sort();
//        products.ForEach(Console.WriteLine);

//        Console.WriteLine("\n--- ILogger default method ---");
//        ILogger logger = new ConsoleLogger();
//        logger.Log("Application started.");
//        logger.LogError("Something went wrong!");
//    }
//}

//// ============================================================
//// CONCEPT: Exception Handling
//// ============================================================
//// Exceptions are runtime errors. C# lets you catch and handle
//// them gracefully instead of crashing the program.
//// ============================================================

//using System;
//using System.IO;

//class ExceptionHandling
//{
//    static void Main()
//    {
//        // ---- Basic try / catch / finally ----
//        try
//        {
//            int[] arr = { 1, 2, 3 };
//            Console.WriteLine(arr[10]);   // IndexOutOfRangeException
//        }
//        catch (IndexOutOfRangeException ex)
//        {
//            Console.WriteLine($"Array error: {ex.Message}");
//        }
//        finally
//        {
//            // finally ALWAYS runs — good for cleanup (close files, DB connections)
//            Console.WriteLine("finally block ran.");
//        }

//        // ---- Multiple catch blocks ----
//        try
//        {
//            string s = null;
//            Console.WriteLine(s.Length);  // NullReferenceException
//        }
//        catch (NullReferenceException ex)
//        {
//            Console.WriteLine($"Null error: {ex.Message}");
//        }
//        catch (Exception ex)
//        {
//            // catch-all — always put this LAST
//            Console.WriteLine($"General error: {ex.Message}");
//        }

//        // ---- Custom exception ----
//        try
//        {
//            Withdraw(100m, 200m);
//        }
//        catch (InsufficientFundsException ex)
//        {
//            Console.WriteLine($"Custom exception: {ex.Message}");
//            Console.WriteLine($"Shortfall: {ex.Shortfall:C}");
//        }

//        // ---- Re-throwing exceptions ----
//        try
//        {
//            RiskyOperation();
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Caught and re-thrown: {ex.Message}");
//        }

//        // ---- when filter: catch only specific conditions ----
//        for (int i = 0; i < 3; i++)
//        {
//            try
//            {
//                ThrowWithCode(i);
//            }
//            catch (Exception ex) when (ex.Message.Contains("CRITICAL"))
//            {
//                Console.WriteLine($"Only caught CRITICAL: {ex.Message}");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Regular catch: {ex.Message}");
//            }
//        }
//    }

//    static void Withdraw(decimal balance, decimal amount)
//    {
//        if (amount > balance)
//            throw new InsufficientFundsException(balance, amount);

//        Console.WriteLine($"Withdrew {amount:C}");
//    }

//    static void RiskyOperation()
//    {
//        try
//        {
//            throw new InvalidOperationException("Something broke inside.");
//        }
//        catch (Exception)
//        {
//            // throw; preserves original stack trace (preferred)
//            throw;
//        }
//    }

//    static void ThrowWithCode(int code)
//    {
//        throw new Exception(code == 1 ? "CRITICAL failure" : $"Minor error {code}");
//    }
//}

//// ---- Custom Exception class ----
//class InsufficientFundsException : Exception
//{
//    public decimal Shortfall { get; }

//    public InsufficientFundsException(decimal balance, decimal amount)
//        : base($"Cannot withdraw {amount:C}. Balance is only {balance:C}.")
//    {
//        Shortfall = amount - balance;
//    }
//}