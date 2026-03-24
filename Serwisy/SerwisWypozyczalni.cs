using WypozyczalniaSprzetu.Model;
using WypozyczalniaSprzetu.Pomocnicze;

namespace WypozyczalniaSprzetu.Serwisy;

public class SerwisWypozyczalni
{
    private readonly List<Sprzet> _sprzety = new();
    private readonly List<Uzytkownik> _uzytkownicy = new();
    private readonly List<Wypozyczenie> _wypozyczenia = new();

    private readonly GeneratorIdentyfikatorow _generator;
    private readonly PolitykaWypozyczen _polityka;

    public SerwisWypozyczalni(GeneratorIdentyfikatorow generator, PolitykaWypozyczen polityka)
    {
        _generator = generator;
        _polityka = polityka;
    }

    public Sluchacz DodajSluchacza(string imie, string nazwisko, string kierunek, int semestr)
    {
        var sluchacz = new Sluchacz(_generator.NoweIdUzytkownika(), imie, nazwisko, kierunek, semestr);
        _uzytkownicy.Add(sluchacz);
        return sluchacz;
    }

    public PracownikUczelni DodajPracownika(string imie, string nazwisko, string dzial, string stanowisko)
    {
        var pracownik = new PracownikUczelni(_generator.NoweIdUzytkownika(), imie, nazwisko, dzial, stanowisko);
        _uzytkownicy.Add(pracownik);
        return pracownik;
    }

    public Laptop DodajLaptop(string nazwa, int ramGb, string procesor)
    {
        var laptop = new Laptop(_generator.NoweIdSprzetu(), nazwa, ramGb, procesor);
        _sprzety.Add(laptop);
        return laptop;
    }

    public Rzutnik DodajRzutnik(string nazwa, int jasnoscAnsi, string rozdzielczosc)
    {
        var rzutnik = new Rzutnik(_generator.NoweIdSprzetu(), nazwa, jasnoscAnsi, rozdzielczosc);
        _sprzety.Add(rzutnik);
        return rzutnik;
    }

    public Aparat DodajAparat(string nazwa, int megapiksele, bool czy4K)
    {
        var aparat = new Aparat(_generator.NoweIdSprzetu(), nazwa, megapiksele, czy4K);
        _sprzety.Add(aparat);
        return aparat;
    }

    public List<Sprzet> PobierzCalySprzet()
    {
        return _sprzety;
    }

    public List<Sprzet> PobierzDostepnySprzet()
    {
        return _sprzety.Where(s => s.Status == StatusSprzetu.Dostepny).ToList();
    }

    public WynikOperacji OznaczJakoNiedostepny(string idSprzetu)
    {
        var sprzet = _sprzety.FirstOrDefault(s => s.Id == idSprzetu);
        if (sprzet == null)
            return WynikOperacji.Blad("Nie znaleziono sprzętu.");

        sprzet.UstawStatus(StatusSprzetu.Niedostepny);
        return WynikOperacji.Sukces($"Sprzęt {sprzet.Nazwa} oznaczono jako niedostępny.");
    }

    public WynikOperacji Wypozycz(string idUzytkownika, string idSprzetu, int liczbaDni)
    {
        var uzytkownik = _uzytkownicy.FirstOrDefault(u => u.Id == idUzytkownika);
        if (uzytkownik == null)
            return WynikOperacji.Blad("Nie znaleziono użytkownika");

        var sprzet = _sprzety.FirstOrDefault(s => s.Id == idSprzetu);
        if (sprzet == null)
            return WynikOperacji.Blad("Nie znaleziono sprzętu");

        if (sprzet.Status != StatusSprzetu.Dostepny)
            return WynikOperacji.Blad("Sprzęt nie jest dostępny do wypożyczenia ");

        int aktywneWypozyczenia = _wypozyczenia.Count(w => w.Uzytkownik.Id == idUzytkownika && w.CzyAktywne);
        int limit = _polityka.PobierzLimitDla(uzytkownik);

        if (aktywneWypozyczenia >= limit)
            return WynikOperacji.Blad($"Użytkownik przekroczył limit aktywnych wypożyczeń limit: {limit}");

        var wypozyczenie = new Wypozyczenie(_generator.NoweIdWypozyczenia(), uzytkownik, sprzet, DateTime.Now, liczbaDni);
        _wypozyczenia.Add(wypozyczenie);
        sprzet.UstawStatus(StatusSprzetu.Wypozyczony);

        return WynikOperacji.Sukces($"Wypożyczono sprzęt: {sprzet.Nazwa} użytkownikowi {uzytkownik.Imie} {uzytkownik.Nazwisko}.");
    }

    public WynikOperacji Zwroc(string idSprzetu, DateTime dataZwrotu)
    {
        var wypozyczenie = _wypozyczenia
            .LastOrDefault(w => w.Sprzet.Id == idSprzetu && w.CzyAktywne);

        if (wypozyczenie == null)
            return WynikOperacji.Blad("Nie znaleziono aktywnego wypożyczenia dla tego sprzętu");

        decimal kara = _polityka.ObliczKare(wypozyczenie.TerminZwrotu, dataZwrotu);
        wypozyczenie.OznaczZwrot(dataZwrotu, kara);
        wypozyczenie.Sprzet.UstawStatus(StatusSprzetu.Dostepny);

        return WynikOperacji.Sukces($"Sprzęt zwrócono Kara-> {kara} zł");
    }

    public List<Wypozyczenie> PobierzAktywneWypozyczeniaUzytkownika(string idUzytkownika)
    {
        return _wypozyczenia
            .Where(w => w.Uzytkownik.Id == idUzytkownika && w.CzyAktywne)
            .ToList();
    }

    public List<Wypozyczenie> PobierzPrzeterminowaneWypozyczenia()
    {
        return _wypozyczenia.Where(w => w.CzyPrzeterminowane).ToList();
    }

    public string GenerujRaport()
    {
        int wszystkichSprzetow = _sprzety.Count;
        int dostepnych = _sprzety.Count(s => s.Status == StatusSprzetu.Dostepny);
        int wypozyczonych = _sprzety.Count(s => s.Status == StatusSprzetu.Wypozyczony);
        int niedostepnych = _sprzety.Count(s => s.Status == StatusSprzetu.Niedostepny);
        int aktywnychWypozyczen = _wypozyczenia.Count(w => w.CzyAktywne);
        decimal sumaKar = _wypozyczenia.Sum(w => w.Kara);

        return
$@"===== RAPORT WYPOŻYCZALNI =====
Liczba sprzętów: {wszystkichSprzetow}
Dostępne: {dostepnych}
Wypożyczone: {wypozyczonych}
Niedostępne: {niedostepnych}
Aktywne wypożyczenia: {aktywnychWypozyczen}
Suma naliczonych kar: {sumaKar} zł
===============================";
    }
}