using PametnaBiblioteka.Enums;
using PametnaBiblioteka.Services;

namespace PametnaBiblioteka
{
    class Program
    {
        static void Main()
        {
            var sistem = new UpravljanjeKorisnicima();

            sistem.RegistrujKorisnika("Amel", "Divović", "adivovic1@email.com", Uloga.Administrator);
            sistem.RegistrujKorisnika("Bilal", "Ozdić", "bozdic@email.com", Uloga.Clan);
            sistem.RegistrujKorisnika("", "Divović", "adivovic1@email.com", Uloga.Administrator);
            sistem.RegistrujKorisnika("Amel", "Divović", "adivovic1#email.com", Uloga.Administrator);
            sistem.RegistrujKorisnika("Amel", "Divović", "adivovic1@email.com", Uloga.Administrator);


            sistem.PrikaziKorisnike();

            sistem.AzurirajKorisnika(2, "Bilal", "Ozdić", "b.ozdic@email.com", Uloga.Clan);
            sistem.PrikaziKorisnike();

            sistem.ObrisiKorisnika(1);
            sistem.PrikaziKorisnike();
        }
    }
}