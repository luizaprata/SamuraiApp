using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Linq;

namespace SamuraiApp.UI
{
    class Program
    {
        private static SamuraiContext _context = new SamuraiContext();

        private static void Main(string[] args)
        {
            _context.Database.EnsureCreated();
            //GetSamurais("Before Add");
            //AddSamuraisByName("Shimada", "Okamoto", "Kikuchio", "Hayashida");
            //AddVariousTypes();
            //GetBattles("After Add");
            //QueryFilters("Misu");

            // RetrieveAndUpdateSamurai();

            //RetrieveAndUpdateMultipleSamurais();
            RetrieveAndDeleteSamurai();
            GetSamurais("After Add");

            Console.Write("Press any key...");
            Console.ReadKey();

        }

        private static void AddVariousTypes()
        {
            _context.AddRange(
                new Samurai { Name = "Misu" },
                new Battle { Name = "Battle of Okinawa" },
                new Battle { Name = "Battle of Sekigahara" }
                );
            _context.SaveChanges();
        }

        private static void AddSamuraisByName(params string[] names)
        {
            foreach(string name in names)
            {
                _context.Samurais.Add(new Samurai { Name = name });
            }
            _context.SaveChanges();
        }

        private static void GetSamurais(string text)
        {
            var samurais = _context.Samurais.ToList();
            Console.WriteLine($"{text}: Samurai count is {samurais.Count}");
            foreach (var samurai in samurais)
            {
                Console.WriteLine(samurai.Name);
            }
        }

        private static void GetBattles(string text)
        {
            var battles = _context.Battles.ToList();
            Console.WriteLine($"{text}: Battles count is {battles.Count}");
            foreach (var battle in battles)
            {
                Console.WriteLine(battle.Name);
            }
        }

        private static void QueryFilters(string name)
        {
            var samurais = _context.Samurais.Where(s => s.Name.Contains(name)).ToList();
        }

        private static void QueryAggregates(string name)
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name.Contains(name));
        }

        private static void RetrieveAndUpdateSamurai()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += "San";
            _context.SaveChanges();
        }

        private static void RetrieveAndUpdateMultipleSamurais()
        {
            var samurais = _context.Samurais.Skip(1).Take(4).ToList();
            samurais.ForEach((samurai)=>samurai.Name += "San");
            _context.SaveChanges();
        }

        private static void RetrieveAndDeleteSamurai()
        {
            var samurai = _context.Samurais.Find(4);
            _context.Samurais.Remove(samurai);
            _context.SaveChanges();
        }
    }
}
