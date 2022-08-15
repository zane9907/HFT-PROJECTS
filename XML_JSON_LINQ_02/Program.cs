using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace XML_JSON_LINQ_02
{
    class CD
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Country { get; set; }
        public string Company { get; set; }
        public double Price { get; set; }
        public int Year { get; set; }

        public CD(string title, string artist, string country, string company, double price, int year)
        {
            Title = title;
            Artist = artist;
            Country = country;
            Company = company;
            Price = price;
            Year = year;
        }

        public CD()
        {

        }

        public override string ToString()
        {
            return $"\"{Title}\" - {Artist} [{Year}] {Price}$";
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            //var catalog = DeserializeXML("cd_catalog.xml");
            var catalog = DeserializeJSON("cd_catalog.json");

            

            //Lekérdezések

            //1. Gyűjtsük ki a CD-k címeit egy külön listába
            var titleMethod = catalog.Select(x => x.Title).ToList();


            var titleQuery = (from x in catalog
                                    select x.Title).ToList();



            //2. Gyűjtsük ki a CD-k címeit és évszámait, évszám szerint növekvő sorrendben
            var titleYearIncMethod = catalog.Select(x => new
            {
                Title = x.Title,
                Year = x.Year
            }).OrderBy(y => y.Year).ToList();       
            

            var titleYearIncQuery = (from x in catalog
                                     orderby x.Year
                                     select new
                                     {
                                         Title = x.Title,
                                         Year = x.Year
                                     }).ToList();



            //3. Gyűjtsük ki csak a 10$-nál nagyobb árú CD-ket
            var moreThan10Method = catalog.Where(x => x.Price >= 10).ToList();

            var moreThan10Query = (from x in catalog
                                  where x.Price >= 10
                                  select x).ToList();



            //4. Gyűjtsük ki azokat az elemeket, amelyek évszáma legalább 1995 és az EU-ban készültek
            //   Írassuk ki a CD-k nevét, szerzőjét és árát
            var eu1995Method = catalog.Where(x => x.Year >= 1995 && x.Country == "EU").Select(x => new
            {
                TITLE = x.Title,
                ARTIST = x.Artist,
                PRICE = x.Price
            }).ToList();

            var eu1995Query = (from x in catalog
                               where x.Year >= 1995 && x.Country == "EU"
                               select new
                               {
                                   TITLE = x.Title,
                                   ARTIST = x.Artist,
                                   PRICE = x.Price
                               }).ToList();



            //5. Számoljuk meg, hogy hány olyan CD van ami az USA-ban készült
            var madeInUSAMethod = catalog.Count(x => x.Country == "USA");

            var madeInUSAQuery = (from x in catalog
                                  where x.Country == "USA"
                                  select x).Count();



            //6. Csoportosítsuk az elemeket ország szerint, valamint számoljuk meg hogy országonként hány CD-t adtak ki
            var groupCountryMethod = catalog.GroupBy(x => x.Country).Select(y => new
            {
                COUNTRY = y.Key,
                COUNT = y.Count()
            }).ToList();

            var groupCountryQuery = (from x in catalog
                                     group x by x.Country into g
                                     select new
                                     {
                                         COUNTRY = g.Key,
                                         COUNT = g.Count()
                                     }).ToList();



            //7. Csoportosítsuk a CD-ket évszám szerint és írjuk ki mellé az évenkénti átlagárat kettő tizedesjegyig
            var groupYearMethod = catalog.GroupBy(x => x.Year).Select(y => new
            {
                YEAR = y.Key,
                AVG = Math.Round(y.Average(x => x.Price),2)
            }).ToList();


            var groupYearQuery = (from x in catalog
                                  group x by x.Year into g
                                  select new
                                  {
                                      YEAR = g.Key,
                                      AVG = Math.Round(g.Average(x => x.Price),2)
                                  }).ToList();

            ;







            //string choice = "";
            //do
            //{
            //    catalog.Add(CreateCD());
            //    Console.WriteLine("Do you want to add more? y/n");
            //    choice = Console.ReadLine();


            //} while (choice == "y");



            

        }

        static CD CreateCD()
        {
            CD c = new CD();
            Console.Write("Title: ");
            c.Title = Console.ReadLine();

            Console.Write("Artist: ");
            c.Artist = Console.ReadLine();

            Console.Write("Country: ");
            c.Country = Console.ReadLine();

            Console.Write("Company: ");
            c.Company = Console.ReadLine();

            Console.Write("Price: ");
            c.Price = double.Parse(Console.ReadLine());

            Console.Write("Year: ");
            c.Year = int.Parse(Console.ReadLine());

            return c;
        }

        static List<CD> DeserializeJSON(string file)
        {
            string json = File.ReadAllText(file);
            return JsonConvert.DeserializeObject<List<CD>>(json);
        }


        static List<CD> DeserializeXML(string file)
        {
            List<CD> tmp = new List<CD>();

            XDocument xdoc = XDocument.Load(file);

            foreach (var item in xdoc.Root.Elements())
            {
                tmp.Add(new CD()
                {
                    Title = item.Element("TITLE").Value,
                    Artist = item.Element("ARTIST").Value,
                    Country = item.Element("COUNTRY").Value,
                    Company = item.Element("COMPANY").Value,
                    Price = double.Parse(item.Element("PRICE").Value),
                    Year = int.Parse(item.Element("YEAR").Value)
                });
            }

            return tmp;
        }
    }
}
