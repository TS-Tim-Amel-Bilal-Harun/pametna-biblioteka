using System;
using System.Collections.Generic;
using System.Linq;
using BibliotecaSystem.Models;

namespace BibliotecaSystem.Services
{
    public class ManagmentPosudbi
    {
        private List<Posudba> posudbe = new List<Posudba>();
        private int sledecaID = 1;
        private const int DANA_POSUDBE = 14;
        private const decimal KAZNA_PO_DANU = 0.50m;

        public void PosudbiKnjigu(List<Korisnik> korisnici, List<Knjiga> knjige)
        {
            Console.WriteLine("\n=== POSUDBA KNJIGE ===");

            if (korisnici.Count == 0)
            {
                Console.WriteLine("Nema registrovanih korisnika!");
                return;
            }

            if (knjige.Count == 0)
            {
                Console.WriteLine("Nema knjiga u inventaru!");
                return;
            }

            PrikaziKorisnike(korisnici);
            Console.Write("\nUnesite ID korisnika: ");
            if (!int.TryParse(Console.ReadLine(), out int korisnikID))
            {
                Console.WriteLine("Neispravan ID!");
                return;
            }

            var korisnik = korisnici.FirstOrDefault(k => k.KorisnikID == korisnikID);
            if (korisnik == null)
            {
                Console.WriteLine("Korisnik ne postoji!");
                return;
            }

            PrikaziKnjige(knjige);
            Console.Write("\nUnesite ID knjige: ");
            if (!int.TryParse(Console.ReadLine(), out int knjigaID))
            {
                Console.WriteLine("Neispravan ID!");
                return;
            }

            var knjiga = knjige.FirstOrDefault(k => k.KnjigaID == knjigaID);
            if (knjiga == null)
            {
                Console.WriteLine("Knjiga ne postoji!");
                return;
            }

            if (knjiga.DostupneKopije <= 0)
            {
                Console.WriteLine("Knjiga nije dostupna!");
                return;
            }

            int posudjeneKnjige = posudbe.Count(p => p.KorisnikID == korisnikID && !p.Vracena);
            if (posudjeneKnjige >= 3)
            {
                Console.WriteLine("Korisnik može posuđiti maksimalno 3 knjige!");
                return;
            }

            Posudba novaPosudba = new Posudba
            {
                PosudbaID = sledecaID++,
                KorisnikID = korisnikID,
                KnjigaID = knjigaID,
                DatumPosudbe = DateTime.Now,
                RokVracanja = DateTime.Now.AddDays(DANA_POSUDBE),
                Vracena = false,
                Kazna = 0
            };

            posudbe.Add(novaPosudba);
            knjiga.DostupneKopije--;

            Console.WriteLine($"Knjiga '{knjiga.Naslov}' je uspješno posuđena!");
            Console.WriteLine($"Rok vračanja: {novaPosudba.RokVracanja:dd.MM.yyyy}");
        }

        public void VratiKnjigu(List<Knjiga> knjige)
        {
            Console.WriteLine("\n=== VRAĆANJE KNJIGE ===");

            if (posudbe.Count == 0)
            {
                Console.WriteLine("Nema aktivnih posudbi!");
                return;
            }

            // ovdje možeš proslijediti i listu korisnika ako želiš puno ime u prikazu
            PrikaziAktivnePosudbe(new List<Korisnik>(), knjige);

            Console.Write("\nUnesite ID posudbe: ");
            if (!int.TryParse(Console.ReadLine(), out int posudbaID))
            {
                Console.WriteLine("Neispravan ID!");
                return;
            }

            var posudba = posudbe.FirstOrDefault(p => p.PosudbaID == posudbaID && !p.Vracena);
            if (posudba == null)
            {
                Console.WriteLine("Posudba ne postoji ili je već vraćena!");
                return;
            }

            var knjiga = knjige.FirstOrDefault(k => k.KnjigaID == posudba.KnjigaID);
            if (knjiga == null)
            {
                Console.WriteLine("Greška: Knjiga ne postoji!");
                return;
            }

            posudba.DatumVracanja = DateTime.Now;
            posudba.Vracena = true;

            if (posudba.DatumVracanja > posudba.RokVracanja)
            {
                int danaPrekašnjenja = (int)(posudba.DatumVracanja.Value - posudba.RokVracanja).TotalDays;
                posudba.Kazna = danaPrekašnjenja * KAZNA_PO_DANU;
            }

            knjiga.DostupneKopije++;

            Console.WriteLine("Knjiga je uspješno vraćena!");

            if (posudba.Kazna > 0)
            {
                Console.WriteLine($"KAZNA za kanjenje: {posudba.Kazna:C}");
            }
            else
            {
                Console.WriteLine("Nema kazne - vraćeno na vrijeme!");
            }
        }

        public void PrikaziKorisnike(List<Korisnik> korisnici)
        {
            Console.WriteLine("\n--- REGISTROVANI KORISNICI ---");
            foreach (var k in korisnici)
            {
                Console.WriteLine($"ID: {k.KorisnikID} | {k.Ime} {k.Prezime}");
            }
        }

        public void PrikaziKnjige(List<Knjiga> knjige)
        {
            Console.WriteLine("\n--- DOSTUPNE KNJIGE ---");
            foreach (var k in knjige)
            {
                Console.WriteLine($"ID: {k.KnjigaID} | {k.Naslov} ({k.Autor}) | Dostupno: {k.DostupneKopije}");
            }
        }

        public void PrikaziAktivnePosudbe(List<Korisnik> korisnici, List<Knjiga> knjige)
        {
            Console.WriteLine("\n=== AKTIVNE POSUDBE ===");

            var aktivne = posudbe.Where(p => !p.Vracena).ToList();

            if (aktivne.Count == 0)
            {
                Console.WriteLine("Nema aktivnih posudbi.");
                return;
            }

            foreach (var p in aktivne)
            {
                var korisnik = korisnici.FirstOrDefault(k => k.KorisnikID == p.KorisnikID);
                var knjiga = knjige.FirstOrDefault(k => k.KnjigaID == p.KnjigaID);

                string status = DateTime.Now > p.RokVracanja ? "⚠ PREKAŠNJENO" : "✅ U roku";

                Console.WriteLine($"\nID: {p.PosudbaID} | {status}");
                Console.WriteLine($"  Korisnik: {korisnik?.Ime} {korisnik?.Prezime}");
                Console.WriteLine($"  Knjiga: {knjiga?.Naslov}");
                Console.WriteLine($"  Rok vraćanja: {p.RokVracanja:dd.MM.yyyy}");
            }
        }

        public void PrikaziPrekoracenja(List<Korisnik> korisnici, List<Knjiga> knjige)
        {
            Console.WriteLine("\n=== KNJIGE SA PREKORAČENIM ROKOM ===");

            var prekoracenja = posudbe
                .Where(p => !p.Vracena && DateTime.Now > p.RokVracanja)
                .ToList();

            if (prekoracenja.Count == 0)
            {
                Console.WriteLine("Nema prekršitenih rokova!");
                return;
            }

            Console.WriteLine($"Pronađeno {prekoracenja.Count} prekoraćenih posudbi:\n");

            foreach (var p in prekoracenja)
            {
                var korisnik = korisnici.FirstOrDefault(k => k.KorisnikID == p.KorisnikID);
                var knjiga = knjige.FirstOrDefault(k => k.KnjigaID == p.KnjigaID);
                int danaPrekašnjenja = (int)(DateTime.Now - p.RokVracanja).TotalDays;
                decimal kazna = danaPrekašnjenja * KAZNA_PO_DANU;

                Console.WriteLine($"ID: {p.PosudbaID}");
                Console.WriteLine($"  Korisnik: {korisnik?.Ime} {korisnik?.Prezime}");
                Console.WriteLine($"  Knjiga: {knjiga?.Naslov}");
                Console.WriteLine($"  Prekoračenje: {danaPrekašnjenja} dana");
                Console.WriteLine($"  Trenutna kazna: {kazna:C}\n");
            }
        }

        public void GenerisiIstoriju(List<Korisnik> korisnici, List<Knjiga> knjige)
        {
            Console.WriteLine("\n=== ISTORIJA POSUDBI ===");

            if (posudbe.Count == 0)
            {
                Console.WriteLine("Nema posudbi u sistemu.");
                return;
            }

            Console.WriteLine($"Ukupno posudbi: {posudbe.Count}\n");

            foreach (var p in posudbe)
            {
                var korisnik = korisnici.FirstOrDefault(k => k.KorisnikID == p.KorisnikID);
                var knjiga = knjige.FirstOrDefault(k => k.KnjigaID == p.KnjigaID);
                string status = p.Vracena ? "✅ Vraćena" : "⏳ Aktivna";

                Console.WriteLine($"ID: {p.PosudbaID} | {status}");
                Console.WriteLine($"  Korisnik: {korisnik?.Ime} {korisnik?.Prezime}");
                Console.WriteLine($"  Knjiga: {knjiga?.Naslov}");
                Console.WriteLine($"  Posudba: {p.DatumPosudbe:dd.MM.yyyy}");
                Console.WriteLine($"  Rok: {p.RokVracanja:dd.MM.yyyy}");

                if (p.Vracena)
                {
                    Console.WriteLine($"  Vraćena: {p.DatumVracanja:dd.MM.yyyy}");
                    if (p.Kazna > 0)
                        Console.WriteLine($"  Kazna: {p.Kazna:C}");
                }
                Console.WriteLine();
            }
        }

        public void MeniPosudbi(List<Korisnik> korisnici, List<Knjiga> knjige)
        {
            bool aktivan = true;
            while (aktivan)
            {
                Console.WriteLine("\n1) Posudi knjigu");
                Console.WriteLine("2) Vrati knjigu");
                Console.WriteLine("3) Prikaži aktivne posudbe");
                Console.WriteLine("4) Prikaži prekoračenja");
                Console.WriteLine("5) Istorija posudbi");
                Console.WriteLine("0) Nazad");
                Console.Write("\nOdabir: ");

                string opcija = Console.ReadLine();

                switch (opcija)
                {
                    case "1":
                        PosudbiKnjigu(korisnici, knjige);
                        break;
                    case "2":
                        VratiKnjigu(knjige);
                        break;
                    case "3":
                        PrikaziAktivnePosudbe(korisnici, knjige);
                        break;
                    case "4":
                        PrikaziPrekoracenja(korisnici, knjige);
                        break;
                    case "5":
                        GenerisiIstoriju(korisnici, knjige);
                        break;
                    case "0":
                        aktivan = false;
                        break;
                    default:
                        Console.WriteLine("❌ Neispravan izbor!");
                        break;
                }
            }
        }
    }
}
