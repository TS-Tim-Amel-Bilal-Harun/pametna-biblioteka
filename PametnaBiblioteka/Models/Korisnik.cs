using System;
using PametnaBiblioteka.Enums;

namespace BibliotecaSystem.Models
{
    public class Korisnik
    {
        private int _id;
        public int KorisnikID { get => _id; set => _id = value; }
        public int Id { get => _id; set => _id = value; }

        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public Uloga Uloga { get; set; }

        public override string ToString()
        {
            return $"ID: {KorisnikID} | {Ime} {Prezime} | {Email} | Uloga: {Uloga}";
        }
    }
}