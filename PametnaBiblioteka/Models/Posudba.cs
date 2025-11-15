using System;

namespace BibliotecaSystem.Models
{
    public class Posudba
    {
        public int PosudbaID { get; set; }
        public int KorisnikID { get; set; }
        public int KnjigaID { get; set; }
        public DateTime DatumPosudbe { get; set; }
        public DateTime RokVracanja { get; set; }
        public DateTime? DatumVracanja { get; set; }
        public bool Vracena { get; set; }
        public decimal Kazna { get; set; }
    }
}