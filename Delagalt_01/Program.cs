using System;

namespace Delagalt_01
{
    delegate void Muvelet(int a, int b);

    class Szamok
    {
        public event Muvelet muvelet;

        public int A { get; set; }
        public int B { get; set; }

        public Szamok()
        {
            Random rnd = new Random();
            A = rnd.Next(1, 10);
            B = rnd.Next(1, 10);
        }

        public void Szamol(Muvelet m)
        {
            m?.Invoke(A, B);
        }

        public void Szamol()
        {
            muvelet?.Invoke(A, B);
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Muvelet m = new Muvelet(Osszeadas);
            m += Kivonas;
            m += Szorzas;
            m += Osztas;

            //Console.WriteLine("Eredmény: " + m(5, 2)); Console.WriteLine();


            //foreach (var item in m.GetInvocationList())
            //{
            //    Console.WriteLine((item as Muvelet)?.Invoke(5, 2));
            //}


            Szamok sz = new Szamok();
            Console.WriteLine($"A: {sz.A}\nB: {sz.B}");

            Console.WriteLine("Delegált:");
            sz.Szamol(m);


            Console.WriteLine("\nEsemény:");
            sz.muvelet += m;
            sz.Szamol();

            //m -= Osszeadas;
            //m -= Kivonas;
            //m -= Szorzas;
            //m -= Osztas;



            //Készítsünk egy Action delegáltat.
            //Anonym függvénnyel adjuk hozzá a maradékos oszást.
            Console.WriteLine("\nAction");
            Action<int, int> mod = delegate (int x, int y)
            {
                Console.WriteLine($"Mod: {x % y}");
            };

            mod?.Invoke(sz.A, sz.B);




            //Készítsünk egy Function delegáltat.
            //Anonym függvénnyel adjuk vissza a számok átlagát
            //Majd írassuk ki a konzolra az eredményt
            Console.WriteLine("\nFunction");
            Func<int, int, double> avg = delegate (int x, int y)
            {
                return (x + y) / 2;
            };

            double? average = avg?.Invoke(sz.A, sz.B);
            Console.WriteLine($"Average: {average}");

            //Készítsünk egy Predicate delegáltat.
            //Anonym függvény segítségével adjon vissza igazat ha az átlag páros
            //Majd írassuk ki a konzolra az eredményt
            Console.WriteLine("\nPredicate");
            Predicate<double?> paros = delegate (double? x)
            {
                return x % 2 == 0;
            };

            
            Console.WriteLine(paros(average) ? "Páros" : "Páratlan");

            //Ugyanezek Lambdával
            //Action<int, int> modL = (x,y) =>
            //{
            //    Console.WriteLine($"Mod: {x % y}");
            //}

            ;

        }

        static void Osszeadas(int x, int y)
        {
            Console.WriteLine($"Összeadás: {x + y}");
        }

        static void Kivonas(int x, int y)
        {
            Console.WriteLine($"Kivonás: {x - y}");
        }

        static void Szorzas(int x, int y)
        {
            Console.WriteLine($"Szorzás: {x * y}");
        }

        static void Osztas(int x, int y)
        {
            Console.WriteLine($"Osztás: {x / y}");
        }
    }
}
