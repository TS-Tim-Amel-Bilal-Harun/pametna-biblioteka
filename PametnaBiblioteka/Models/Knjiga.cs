using System;
using BibliotecaSystem.Enums;

namespace BibliotecaSystem.Models
{
    public class Knjiga
    {
        public int KnjigaID { get; set; }
        public string Naslov { get; set; }
        public string Autor { get; set; }
        public string Zanr { get; set; }
        public int DostupneKopije { get; set; } = 1;
        public StatusKnjige Status { get; set; } = StatusKnjige.Dostupna;
    }
}