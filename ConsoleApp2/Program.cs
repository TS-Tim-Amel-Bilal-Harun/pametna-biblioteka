using System;
using System.Collections.Generic;
using BibliotecaSystem.Models;
using BibliotecaSystem.Services;

namespace BibliotecaSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            List<Korisnik> korisnici = new List<Korisnik>
            {
                new Korisnik { KorisnikID = 1, Ime = "Marko", Prezime = "Marković" },
                new Korisnik { KorisnikID = 2, Ime = "Ana", Prezime = "Anić" },
                new Korisnik { KorisnikID = 3, Ime = "Petar", Prezime = "Petrović" }
            };

            List<Knjiga> knjige = new List<Knjiga>
            {
                new Knjiga { KnjigaID = 1, Naslov = "Harry Potter", Autor = "J.K. Rowling", DostupneKopije = 3 },
                new Knjiga { KnjigaID = 2, Naslov = "1984", Autor = "George Orwell", DostupneKopije = 2 },
                new Knjiga { KnjigaID = 3, Naslov = "Dune", Autor = "Frank Herbert", DostupneKopije = 1 },
                new Knjiga { KnjigaID = 4, Naslov = "Vlad", Autor = "S.D. Perry", DostupneKopije = 0 }
            };

            ManagmentPosudbi managment = new ManagmentPosudbi();

            Console.WriteLine("\nUČITANI TEST PODACI:");
            Console.WriteLine("\n--- KORISNICI ---");
            foreach (var k in korisnici)
            {
                Console.WriteLine($"ID: {k.KorisnikID} | {k.Ime} {k.Prezime}");
            }
            Console.WriteLine("\n--- KNJIGE ---");
            foreach (var k in knjige)
            {
                Console.WriteLine($"ID: {k.KnjigaID} | {k.Naslov} ({k.Autor}) | Dostupno: {k.DostupneKopije}");
            }
            Console.WriteLine("\n" + new string('=', 60));

            managment.MeniPosudbi(korisnici, knjige);

            Console.WriteLine("\nHvala što ste koristili sistem! Doviđenja!");
        }
    }
}
