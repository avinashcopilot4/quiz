using System;
using System.Collections.Generic;
using System.Text;

namespace C_Learning.OOps;

public interface IAnimal
{
    void Speak();
    void Eat();
}



public class Dog : Animal,IAnimal
{
    public void Speak()
    {
        Console.WriteLine("Woof! Woof!");
    }

    public void Eat()
    {
        Console.WriteLine("Dog is eating bones.");
    }

    public override void Speak2()
    {
        Console.WriteLine("Dog is speaking ");
    }

}

public class Cat : Animal,IAnimal
{
    public void Speak()
    {
        Console.WriteLine("Meow! Meow!");
    }

    public void Eat()
    {
        Console.WriteLine("Cat is eating fish.");
    }
    public override void Speak2()
    {
        Console.WriteLine("Cat is speaking ");
    }
}

public class Lion : Animal, IAnimal
{
    public void Speak()
    {
        Console.WriteLine("Meow! Meow!");
    }

    public void Eat()
    {
        Console.WriteLine("Cat is eating fish.");
    }
    public override void Speak2()
    {
        Console.WriteLine("Cat is speaking ");
    }

    public override void Sleep()
    {
        Console.WriteLine("Be careful its a lion");
        base.Sleep();
    }
}


// Abstract class: defines common behavior but leaves details to child classes
public abstract class Animal 
{
    // Concrete method (same for all animals)
    public virtual void Sleep()
    {
        Console.WriteLine("Animal is sleeping...");
    }

    // Abstract method (must be implemented by child classes)
    public abstract void Speak2();
}


// polymorphism can be achieved by 2 ways
//1) method overloading 
//2) method overriding