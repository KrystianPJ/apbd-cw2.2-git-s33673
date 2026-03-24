
namespace WypozyczalniaSp;
class Program
{
    static void Main(string[] args)
    {
        var generator = new GeneratorIdentyfikatorow();
        var polityka = new PolitykaWypozyczen();
        var serwis = new SerwisWypozyczalni(generator, polityka);

        Console.WriteLine("DODAWANIE SPRZĘTU ");
        var laptop1 = serwis.DodajLaptop("Dell Latitude 5520", 16, "Intel i7");
        var laptop2 = serwis.DodajLaptop("Lenovo ThinkPad E14", 8, "AMD Ryzen 5");
        var rzutnik1 = serwis.DodajRzutnik("Epson EB-X06", 3600, "1024x768");
        var aparat1 = serwis.DodajAparat("Canon EOS 250D", 24, true);

        foreach (var sprzet in serwis.PobierzCalySprzet())
            Console.WriteLine(sprzet);

        Console.WriteLine("\nDODAWANIE UŻYTKOWNIKÓW");
        var sluchacz1 = serwis.DodajSluchacza("Anna", "Nowak", "Informatyka", 3);
        var sluchacz2 = serwis.DodajSluchacza("Michał", "Kurek", "Zarządzanie Informacją", 2);
        var pracownik1 = serwis.DodajPracownika("Ewa", "Lis", "IT", "Administrator");

        Console.WriteLine(sluchacz1);
        Console.WriteLine(sluchacz2);
        Console.WriteLine(pracownik1);

        Console.WriteLine("\nPOPRAWNE WYPOŻYCZENIE");
        var wynik1 = serwis.Wypozycz(sluchacz1.Id, laptop1.Id, 7);
        Console.WriteLine(wynik1.Komunikat);

        Console.WriteLine("\nPRÓBA NIEPOPRAWNEJ OPERACJI");
        var wynik2 = serwis.Wypozycz(pracownik1.Id, laptop1.Id, 5);
        Console.WriteLine(wynik2.Komunikat);

        Console.WriteLine("\n  PRÓBA PRZEKROCZENIA LIMITU ");
        Console.WriteLine(serwis.Wypozycz(sluchacz1.Id, laptop2.Id, 5).Komunikat);
        Console.WriteLine(serwis.Wypozycz(sluchacz1.Id, rzutnik1.Id, 5).Komunikat);

        Console.WriteLine("\n  ZWROT W TERMINIE ");
        Console.WriteLine(serwis.Zwroc(laptop1.Id, DateTime.Now.AddDays(3)).Komunikat);

        Console.WriteLine("\n WYPOŻYCZENIE PO ZWROCIE ");
        Console.WriteLine(serwis.Wypozycz(pracownik1.Id, laptop1.Id, 2).Komunikat);

        Console.WriteLine("\n OPÓŹNIONY ZWROT Z KARĄ ");
        Console.WriteLine(serwis.Zwroc(laptop1.Id, DateTime.Now.AddDays(6)).Komunikat);

        Console.WriteLine("\n OZNACZENIE SPRZĘTU JAKO NIEDOSTĘPNY");
        Console.WriteLine(serwis.OznaczJakoNiedostepny(aparat1.Id).Komunikat);

        Console.WriteLine("\nDOSTĘPNY SPRZĘT ");
        foreach (var sprzet in serwis.PobierzDostepnySprzet())
            Console.WriteLine(sprzet);

        Console.WriteLine("\n AKTYWNE WYPOŻYCZENIA SŁUCHACZA ");
        foreach (var wypozyczenie in serwis.PobierzAktywneWypozyczeniaUzytkownika(sluchacz1.Id))
            Console.WriteLine(wypozyczenie);

        Console.WriteLine("\n PRZETERMINOWANE WYPOŻYCZENIA");
        foreach (var wypozyczenie in serwis.PobierzPrzeterminowaneWypozyczenia())
            Console.WriteLine(wypozyczenie);

        Console.WriteLine();
        Console.WriteLine(serwis.GenerujRaport());
    }
}