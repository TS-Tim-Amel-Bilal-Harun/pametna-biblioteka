using System;
using System.Collections.Generic;
using System.Linq;
using BibliotecaSystem.Enums;
using BibliotecaSystem.Models;

namespace BibliotecaSystem.Services
{
    public class ManagmentInventara
    {
        private readonly List<Knjiga> knjige = new List<Knjiga>();
        private int sledecaID = 1;

        public void DodajKnjigu()
        {
            Console.WriteLine("\n<<<DODAVANJE NOVE KNJIGE>>>");

            Console.Write("Unesite naslov knjige: ");
            string naslov = Console.ReadLine();

            Console.Write("Unesite autora: ");
            string autor = Console.ReadLine();

            Console.Write("Unesite zanr: ");
            string zanr = Console.ReadLine();

            Knjiga novaKnjiga = new Knjiga
            {
                KnjigaID = sledecaID++,
                Naslov = naslov,
                Autor = autor,
                Zanr = zanr,
                Status = StatusKnjige.Dostupna
            };

            knjige.Add(novaKnjiga);
            Console.WriteLine($"Knjiga '{novaKnjiga.Naslov}' dodana");
        }

        public void AzurirajKnjigu()
        {
            Console.WriteLine("\n<<<AZURIRANJE KNJIGE>>>");

            if (knjige.Count == 0)
            {
                Console.WriteLine("\nNema knjiga u inventaru");
                return;
            }

            PrikaziSveKnjige();
            Console.Write("\nUnesite ID knjige za ažuriranje: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("❌ Neispravan ID!");
                return;
            }

            Knjiga knjiga = knjige.FirstOrDefault(k => k.KnjigaID == id);
            if (knjiga == null)
            {
                Console.WriteLine("❌ Knjiga ne postoji!");
                return;
            }

            Console.Write("Novi naslov (ili pritisnite Enter): ");
            string noviNaslov = Console.ReadLine();
            if (!string.IsNullOrEmpty(noviNaslov))
                knjiga.Naslov = noviNaslov;

            Console.Write("Novi autor (ili pritisnite Enter): ");
            string noviAutor = Console.ReadLine();
            if (!string.IsNullOrEmpty(noviAutor))
                knjiga.Autor = noviAutor;

            Console.Write("Novi žanr (ili pritisnite Enter): ");
            string noviZanr = Console.ReadLine();
            if (!string.IsNullOrEmpty(noviZanr))
                knjiga.Zanr = noviZanr;

            Console.Write("Dostupna li je knjiga? (da/ne): ");
            string dostupnostOdg = Console.ReadLine().ToLower();
            if (dostupnostOdg == "da" || dostupnostOdg == "d")
                knjiga.Status = StatusKnjige.Dostupna;
            else if (dostupnostOdg == "ne" || dostupnostOdg == "n")
                knjiga.Status = StatusKnjige.NijeDostupna;

            Console.WriteLine("✅ Knjiga ažurirana!");
        }

        public void ObrisiKnjigu()
        {
            Console.WriteLine("\n=== BRISANJE KNJIGE ===");

            if (knjige.Count == 0)
            {
                Console.WriteLine("\n❌ Nema knjiga u inventaru!");
                return;
            }

            PrikaziSveKnjige();
            Console.Write("\nUnesite ID knjige za brisanje: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("❌ Neispravan ID!");
                return;
            }

            Knjiga knjiga = knjige.FirstOrDefault(k => k.KnjigaID == id);
            if (knjiga == null)
            {
                Console.WriteLine("❌ Knjiga ne postoji!");
                return;
            }

            knjige.Remove(knjiga);
            Console.WriteLine($"✅ Knjiga '{knjiga.Naslov}' obrisana!");
        }

        public void PretragaPoNaslovu()
        {
            Console.WriteLine("\n=== PRETRAGA PO NASLOVU ===");
            Console.Write("Unesite dio naslova: ");
            string upit = Console.ReadLine().ToLower();

            var rezultati = knjige
                .Where(k => k.Naslov != null && k.Naslov.ToLower().Contains(upit))
                .ToList();

            if (rezultati.Count == 0)
            {
                Console.WriteLine("❌ Nema pronađenih knjiga!");
                return;
            }

            Console.WriteLine($"\n✅ Pronađeno {rezultati.Count} knjiga:");
            foreach (var k in rezultati)
            {
                string statusTekst = k.Status == StatusKnjige.Dostupna
                    ? "✅ DOSTUPNA"
                    : "❌ NIJE DOSTUPNA";

                Console.WriteLine(
                    $"ID: {k.KnjigaID} | {k.Naslov} | Autor: {k.Autor} | Zanr: {k.Zanr} | Status: {statusTekst}"
                );
            }
        }

        public void PretragaPoAutoru()
        {
            Console.WriteLine("\n=== PRETRAGA PO AUTORU ===");
            Console.Write("Unesite dio imena autora: ");
            string upit = Console.ReadLine().ToLower();

            var rezultati = knjige
                .Where(k => k.Autor != null && k.Autor.ToLower().Contains(upit))
                .ToList();

            if (rezultati.Count == 0)
            {
                Console.WriteLine("❌ Nema pronađenih knjiga!");
                return;
            }

            Console.WriteLine($"\n✅ Pronađeno {rezultati.Count} knjiga:");
            foreach (var k in rezultati)
            {
                string statusTekst = k.Status == StatusKnjige.Dostupna
                    ? "✅ DOSTUPNA"
                    : "❌ NIJE DOSTUPNA";

                Console.WriteLine(
                    $"ID: {k.KnjigaID} | {k.Naslov} | Autor: {k.Autor} | Zanr: {k.Zanr} | Status: {statusTekst}"
                );
            }
        }

        public void PretragaPoZanru()
        {
            Console.WriteLine("\n=== PRETRAGA PO ŽANRU ===");
            Console.Write("Unesite dio naziva žanra: ");
            string upit = Console.ReadLine().ToLower();

            var rezultati = knjige
                .Where(k => k.Zanr != null && k.Zanr.ToLower().Contains(upit))
                .ToList();

            if (rezultati.Count == 0)
            {
                Console.WriteLine("❌ Nema pronađenih knjiga!");
                return;
            }

            Console.WriteLine($"\n✅ Pronađeno {rezultati.Count} knjiga:");
            foreach (var k in rezultati)
            {
                string statusTekst = k.Status == StatusKnjige.Dostupna
                    ? "✅ DOSTUPNA"
                    : "❌ NIJE DOSTUPNA";

                Console.WriteLine(
                    $"ID: {k.KnjigaID} | {k.Naslov} | Zanr: {k.Zanr} | Status: {statusTekst}"
                );
            }
        }

        public void PrikaziDostupnost()
        {
            Console.WriteLine("\n<<<DOSTUPNOST KNJIGA>>>");

            if (knjige.Count == 0)
            {
                Console.WriteLine("Nema knjiga u inventaru");
                return;
            }

            var dostupne = knjige
                .Where(k => k.Status == StatusKnjige.Dostupna)
                .ToList();

            var nedostupne = knjige
                .Where(k => k.Status == StatusKnjige.NijeDostupna)
                .ToList();

            Console.WriteLine("\n<<<DOSTUPNE KNJIGE>>>");
            if (dostupne.Count == 0)
            {
                Console.WriteLine("Nema dostupnih knjiga.");
            }
            else
            {
                foreach (var k in dostupne)
                {
                    Console.WriteLine(
                        $"ID: {k.KnjigaID} | {k.Naslov} | Autor: {k.Autor}"
                    );
                }
            }

            Console.WriteLine("\n<<<NEDOSTUPNE KNJIGE>>>");
            if (nedostupne.Count == 0)
            {
                Console.WriteLine("Sve knjige su dostupne");
            }
            else
            {
                foreach (var k in nedostupne)
                {
                    Console.WriteLine(
                        $"ID: {k.KnjigaID} | {k.Naslov} | Autor: {k.Autor}"
                    );
                }
            }
        }

        public void PrikaziSveKnjige()
        {
            if (knjige.Count == 0)
            {
                Console.WriteLine("Nema knjiga u inventaru");
                return;
            }

            Console.WriteLine("\n<<<INVENTAR KNJIGA>>>");
            foreach (var k in knjige)
            {
                string statusTekst = k.Status == StatusKnjige.Dostupna
                    ? "DOSTUPNA"
                    : "NIJE DOSTUPNA";

                Console.WriteLine(
                    $"ID: {k.KnjigaID} | {k.Naslov} | Autor: {k.Autor} | Zanr: {k.Zanr} | {statusTekst}"
                );
            }
        }

        public void MeniInventara()
        {
            bool aktivan = true;
            while (aktivan)
            {
                Console.WriteLine("\n1) Dodaj novu knjigu");
                Console.WriteLine("2) Ažuriraj knjigu");
                Console.WriteLine("3) Obriši knjigu");
                Console.WriteLine("4) Pretraga po naslovu");
                Console.WriteLine("5) Pretraga po autoru");
                Console.WriteLine("6) Pretraga po žanru");
                Console.WriteLine("7) Prikaži dostupnost");
                Console.WriteLine("8) Prikaži sve knjige");
                Console.WriteLine("0) Nazad");
                Console.Write("\nOdabir: ");

                string opcija = Console.ReadLine();

                switch (opcija)
                {
                    case "1":
                        DodajKnjigu();
                        break;
                    case "2":
                        AzurirajKnjigu();
                        break;
                    case "3":
                        ObrisiKnjigu();
                        break;
                    case "4":
                        PretragaPoNaslovu();
                        break;
                    case "5":
                        PretragaPoAutoru();
                        break;
                    case "6":
                        PretragaPoZanru();
                        break;
                    case "7":
                        PrikaziDostupnost();
                        break;
                    case "8":
                        PrikaziSveKnjige();
                        break;
                    case "0":
                        aktivan = false;
                        break;
                    default:
                        Console.WriteLine("Neispravan izbor");
                        break;
                }
            }
        }

        public List<Knjiga> DajKnjige()
        {
            return knjige;
        }
    }
}
