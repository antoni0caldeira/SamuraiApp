using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace SamuraiApp.UI
{
    class Program
    {
        private static readonly SamuraiContext _context = new();

        private static void Main(string[] args)
        {

            //AddSamuraisByName("Julie", "Sampson", "Julie", "Sampson");
            //AddVariusTypes();
            GetSamurais();
            Console.Write("Press any key...");
            Console.ReadKey();
        }

        private static void AddVariusTypes()
        {
            _context.AddRange(
                new Samurai { Name = "Shimada" },
                new Samurai { Name = "Okamoto" },
                new Battle { Name = "Battle of Anegawa" },
                new Battle { Name = "Battle of Nagashimo" });
            _context.Samurais.AddRange(
                new Samurai { Name = "Shimada" },
                new Samurai { Name = "Okamoto" }
                );
            _context.Battles.AddRange(
                new Battle { Name = "Battle of Anegawa" },
                new Battle { Name = "Battle of Nagashimo" }
                );
            _context.SaveChanges();

        }

        private static void AddSamuraisByName(params string[] names)
        {
            foreach (string name in names)
            {
                _context.Samurais.Add(new Samurai { Name = name });

            }
            _context.SaveChanges();
        }
        private static void GetSamurais()
        {
            var query = _context.Samurais
                .Where(x => x.Name.Contains("Julie") || EF.Functions.Like(x.Name, "%Shimada%"))
                .TagWith("From get")
                .ToList()
                .OrderBy(n => n.Name);
            foreach (var item in query)
            {
                Console.WriteLine(item.Name);
            }
            Console.WriteLine($" Samurai count is {query.Count()}");

        }
    }
}

