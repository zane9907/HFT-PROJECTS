using System;

namespace PersonClasses_03
{
    public interface IPerson
    {
        
    }

    public abstract class Person : IPerson
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"Name: {Name} - Age: {Age}";
        }

        public abstract void Do();
    }

    public class Worker : Person
    {
        public string Job { get; set; }
        public int Salary { get; set; }

        public override void Do()
        {
            Console.WriteLine($"Working as {Job} for {Salary}$/hour");
        }
    }

    public class Student : Person
    {
        public string NeptunID { get; set; }
        public string Email { get; set; }
        public int Credits { get; set; }

        public override void Do()
        {
            Console.WriteLine($"[{NeptunID}] Need more credits.");
        }
    }

    public class Retired : Person
    {
        public int SulejmanEpisodes { get; set; }

        public override void Do()
        {
            Console.WriteLine("Watching Szulejmán all day.");
        }
    }
}
