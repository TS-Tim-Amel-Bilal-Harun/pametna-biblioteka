using System;
using System.Collections.Generic;
using PametnaBiblioteka.Models;
using PametnaBiblioteka.Enums;

namespace PametnaBiblioteka.Services
{
    public class UpravljanjeKorisnicima
    {
        private List<Korisnik> korisnici = new();
        private int nextId = 1;

        public void RegistrujKorisnika(string ime, string prezime, string email, Uloga uloga)
        {
            if (string.IsNullOrWhiteSpace(ime) || string.IsNullOrWhiteSpace(prezime) || string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Greška: Ime, prezime i email moraju biti popunjeni!");
                return;
            }

            if (!email.Contains("@") || !email.Contains("."))
            {
                Console.WriteLine("Greška: Email nije u ispravnom formatu!");
                return;
            }

            var korisnik = new Korisnik
            {
                Id = nextId++,
                Ime = ime,
                Prezime = prezime,
                Email = email,
                Uloga = uloga
            };

            korisnici.Add(korisnik);
            Console.WriteLine($"Korisnik {ime} {prezime} uspješno registrovan!");
        }


        public void AzurirajKorisnika(int id, string novoIme, string novoPrezime, string noviEmail, Uloga novaUloga)
        {
            var korisnik = korisnici.Find(k => k.Id == id);
            if (korisnik == null)
            {
                Console.WriteLine("Korisnik nije pronađen!");
                return;
            }

            korisnik.Ime = novoIme;
            korisnik.Prezime = novoPrezime;
            korisnik.Email = noviEmail;
            korisnik.Uloga = novaUloga;
            Console.WriteLine($"Korisnik {id} uspješno ažuriran!");
        }

        public void ObrisiKorisnika(int id)
        {
            var korisnik = korisnici.Find(k => k.Id == id);
            if (korisnik != null)
            {
                korisnici.Remove(korisnik);
                Console.WriteLine($"Korisnik {id} obrisan.");
            }
            else
            {
                Console.WriteLine("Korisnik nije pronađen!");
            }
        }

        public void PrikaziKorisnike()
        {
            if (korisnici.Count == 0)
            {
                Console.WriteLine("Nema registrovanih korisnika.");
                return;
            }

            Console.WriteLine("\nLista korisnika:");
            foreach (var k in korisnici)
                Console.WriteLine(k);
        }
    }
}