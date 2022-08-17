using System;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace PersonClasses_03
{
    public interface IPerson
    {
        
    }

    public abstract class Person : IPerson
    {
        [NameChecker(5)]
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"Name: {Name} - Age: {Age}";
        }

        protected Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public abstract string Description();
    }

    public class Worker : Person
    {
        public Worker(string name, int age) : base(name, age)
        {
        }

        public string Job { get; set; }
        public int Salary { get; set; }

        public override string Description()
        {
            return $"Working hard for the minimal wage.";
        }
    }

    public class Student : Person
    {
        public Student(string name, int age) : base(name, age)
        {
        }

        public string NeptunID { get; set; }
        public string Email { get; set; }
        public int Credits { get; set; }

        public override string Description()
        {
            return $"Always in need for credits.";
        }
    }

    public class Retired : Person
    {
        public Retired(string name, int age) : base(name, age)
        {
        }

        public int SulejmanEpisodes { get; set; }

        public override string Description()
        {
            return "Watching Szulejmán all day.";
        }
    }

    public static class ClassDetector
    {
        public static void ClassesToXML()
        {
            Assembly a = Assembly.GetExecutingAssembly();

            Type[] types = a.GetTypes().Where(x => x.IsClass && x.GetInterface("IPerson") != null).OrderBy(x => x.Name).ToArray();

            XDocument xdoc = new XDocument();
            xdoc.Add(new XElement("people", new XAttribute("count", types.Length)));
            foreach (var t in types)
            {
                xdoc.Root.Add(new XElement("class",
                    new XElement("name", t.Name),
                    new XElement("hash", t.GetHashCode())));
            }

            xdoc.Save("classes.xml");
            ;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class NameCheckerAttribute : Attribute
    {
        public int Length { get; set; }

        public NameCheckerAttribute(int length)
        {
            Length = length;
        }
    }
}
