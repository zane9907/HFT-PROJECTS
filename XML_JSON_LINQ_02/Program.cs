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

        

    }

    class Program
    {
        static void Main(string[] args)
        {
            var catalogXML = DeserializeXML("cd_catalog.xml");
            var catalogJSON = DeserializeJSON("cd_catalog.json");
            

            
            ;

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
