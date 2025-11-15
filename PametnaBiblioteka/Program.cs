using System;
using System.Collections.Generic;
using PametnaBiblioteka.Services;
using BibliotecaSystem.Services;
using BibliotecaSystem.Models;
using PametnaBiblioteka.Enums;
using BibliotecaSystem.Enums;

namespace PametnaBiblioteka
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var upravljanje = new UpravljanjeKorisnicima();
            var inventar = new ManagmentInventara();
            var posudbe = new ManagmentPosudbi();

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n=== PAMETNA BIBLIOTEKA - GLAVNI MENI ===");
                Console.WriteLine("1) Upravljanje korisnicima");
                Console.WriteLine("2) Inventar knjiga");
                Console.WriteLine("3) Sistem posudbi");
                Console.WriteLine("0) Izlaz");
                Console.Write("Odabir: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        MeniKorisnici(upravljanje);
                        break;
                    case "2":
                        inventar.MeniInventara();
                        break;
                    case "3":
                        var korisnici = upravljanje.DajKorisnike();
                        var knjige = inventar.DajKnjige();

                        posudbe.MeniPosudbi(korisnici, knjige);
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Neispravan izbor, pokušajte ponovo.");
                        break;
                }
            }

            Console.WriteLine("Doviđenja!");
        }

        static void MeniKorisnici(UpravljanjeKorisnicima sistem)
        {
            bool active = true;
            while (active)
            {
                Console.WriteLine("\n--- UPRAVLJANJE KORISNICIMA ---");
                Console.WriteLine("1) Registruj korisnika");
                Console.WriteLine("2) Ažuriraj korisnika (po ID)");
                Console.WriteLine("3) Obriši korisnika (po ID)");
                Console.WriteLine("4) Prikaži korisnike");
                Console.WriteLine("0) Nazad");
                Console.Write("Odabir: ");

                string op = Console.ReadLine();
                switch (op)
                {
                    case "1":
                        Console.Write("Ime: ");
                        var ime = Console.ReadLine();
                        Console.Write("Prezime: ");
                        var prezime = Console.ReadLine();
                        Console.Write("Email: ");
                        var email = Console.ReadLine();
                        Console.Write("Uloga (Administrator/Clan): ");
                        var ulogaStr = Console.ReadLine();

                        Uloga uloga = Uloga.Clan;
                        if (!string.IsNullOrWhiteSpace(ulogaStr) && ulogaStr.IndexOf("admin", StringComparison.OrdinalIgnoreCase) >= 0)
                            uloga = Uloga.Administrator;

                        sistem.RegistrujKorisnika(ime, prezime, email, uloga);
                        break;
                    case "2":
                        Console.Write("ID korisnika: ");
                        if (int.TryParse(Console.ReadLine(), out int idA))
                        {
                            Console.Write("Novo ime: ");
                            var ni = Console.ReadLine();
                            Console.Write("Novo prezime: ");
                            var np = Console.ReadLine();
                            Console.Write("Novi email: ");
                            var ne = Console.ReadLine();
                            Console.Write("Nova uloga (Administrator/Clan): ");
                            var nu = Console.ReadLine();
                            Uloga u = Uloga.Clan;
                            if (!string.IsNullOrWhiteSpace(nu) && nu.IndexOf("admin", StringComparison.OrdinalIgnoreCase) >= 0)
                                u = Uloga.Administrator;

                            sistem.AzurirajKorisnika(idA, ni, np, ne, u);
                        }
                        else
                        {
                            Console.WriteLine("Neispravan ID.");
                        }
                        break;
                    case "3":
                        Console.Write("ID korisnika za brisanje: ");
                        if (int.TryParse(Console.ReadLine(), out int idB))
                            sistem.ObrisiKorisnika(idB);
                        else
                            Console.WriteLine("Neispravan ID.");
                        break;
                    case "4":
                        sistem.PrikaziKorisnike();
                        break;
                    case "0":
                        active = false;
                        break;
                    default:
                        Console.WriteLine("Neispravan izbor.");
                        break;
                }
            }
        }
    }
}
