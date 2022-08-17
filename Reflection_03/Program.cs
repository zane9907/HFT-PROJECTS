using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PersonClasses_03;

namespace Reflection_03
{ 
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();

            List<IPerson> people = new List<IPerson>();
            Func<Type> typeRandomizer = () =>
            {
                int num = rnd.Next(3);
                switch (num)
                {
                    case 0: return typeof(Worker);
                    case 1: return typeof(Student);
                    default: return typeof(Retired);
                }
            };



            for (int i = 0; i < 5; i++)
            {
                string rName = "";
                for (int j = 0; j < rnd.Next(3, 11); j++)
                    rName += ((char)rnd.Next('A', 'Z')).ToString();

                int rAge = rnd.Next(1, 101);


                IPerson p = (IPerson)Activator.CreateInstance(typeRandomizer(), new object[] { rName, rAge });
                people.Add(p);
                Console.WriteLine($"Added new {p.GetType().Name}: {p}");
            }
            Console.WriteLine();

            foreach (var item in people)
            {
                Type t = item.GetType();

                Console.WriteLine("\n");
                Console.WriteLine("TYPE: " + t.Name);

                Console.WriteLine("FIELDS: ");
                foreach (var f in t.GetFields())
                {
                    Console.WriteLine("-> " + f.Name);
                }

                Console.WriteLine("PROPERTIES: ");
                foreach (var p in t.GetProperties())
                {
                    Console.WriteLine("-> " + p.Name);
                }

                Console.WriteLine("METHODS: ");
                foreach (var m in t.GetMethods())
                {
                    Console.WriteLine("-> " + m.Name);
                }
            }

            Console.WriteLine("\n\n");
            foreach (var p in people)
            {
                Type t = p.GetType();

                PropertyInfo info = t.GetProperty("Name");
                MethodInfo m = t.GetMethod("Description");


                string result = info.GetValue(p) + ": " + (string)m?.Invoke(p, null);
                Console.WriteLine(result);
            }

            Console.WriteLine("\n\n");

            foreach (var p in people)
            {
                NameValidator(p);
            }


            ClassDetector.ClassesToXML();

            ;
        }



        static void NameValidator(object o)
        {
            PropertyInfo info = o.GetType().GetProperty("Name");
            NameCheckerAttribute attr = (NameCheckerAttribute)info.GetCustomAttribute(typeof(NameCheckerAttribute));

            if (attr != null)
            {
                if (attr.Length <= info.GetValue(o).ToString().Length)
                {
                    Console.WriteLine($"Name accpeted. ({info.GetValue(o)})");
                }
                else
                {
                    Console.WriteLine($"Name is too short. ({info.GetValue(o)})");
                }
            }
        }
    }
}
