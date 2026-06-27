using C_Learning.OOps;

Console.WriteLine("Hello, World!");
// Contract: defines what operations are available
{ 
    Program p = new Program();
    IAnimal dog = new Dog();
    p.LooseCouple(dog);
}
public partial class Program
{
    public void Main()
    {
        Dog dog1 = new Dog();

        Dog dog = new Dog();
        Cat cat = new Cat();
        Lion lion = new Lion();

        dog.Speak();
        dog.Eat();
        dog.Speak2();
        dog.Sleep();

        cat.Speak();
        cat.Eat();
        cat.Speak2();
        cat.Sleep();

        lion.Sleep();
    }
    public void LooseCouple(IAnimal dog)
    {
        dog.Eat();
    }
}


