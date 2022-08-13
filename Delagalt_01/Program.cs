using System;

namespace Delagalt_01
{
    delegate int Muveletek(int x, int y);

    class Program
    {
        static void Main(string[] args)
        {
            Muveletek m = new Muveletek(Osszeadas);
            m += Kivonas;
            m += Szorzas;
            m += Osztas;


            
        }

        static int Osszeadas(int x, int y)
        {
            return x + y;
        }

        static int Kivonas(int x, int y)
        {
            return x - y;
        }

        static int Szorzas(int x, int y)
        {
            return x * y;
        }

        static int Osztas(int x, int y)
        {
            return x / y;
        }
    }
}
