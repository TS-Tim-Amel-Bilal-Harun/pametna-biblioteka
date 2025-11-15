using System;

namespace BibliotecaSystem.Models
{
    public class Knjiga
    {
        public int KnjigaID { get; set; }
        public string Naslov { get; set; }
        public string Autor { get; set; }
        public int DostupneKopije { get; set; }
    }
}
