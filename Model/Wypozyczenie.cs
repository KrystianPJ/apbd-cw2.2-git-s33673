namespace WypozyczalniaSp.Model;

public class Wypozyczenie
{
    public string Id { get; }
    public Uzytkownik Uzytkownik { get; }
    public Sprzet Sprzet { get; }
    public DateTime DataWypozyczenia { get; }
    public DateTime TerminZwrotu { get; }
    public DateTime? DataZwrotu { get; private set; }
    public decimal Kara { get; private set; }

    public bool CzyAktywne => DataZwrotu == null;
    public bool CzyPrzeterminowane => CzyAktywne && DateTime.Now.Date > TerminZwrotu.Date;

    public Wypozyczenie(string id, Uzytkownik uzytkownik, Sprzet sprzet, DateTime dataWypozyczenia, int liczbaDni)
    {
        Id = id;
        Uzytkownik = uzytkownik;
        Sprzet = sprzet;
        DataWypozyczenia = dataWypozyczenia;
        TerminZwrotu = dataWypozyczenia.AddDays(liczbaDni);
        Kara = 0;
    }

    public void OznaczZwrot(DateTime dataZwrotu, decimal kara)
    {
        DataZwrotu = dataZwrotu;
        Kara = kara;
    }

    public override string ToString()
    {
        string zwrot = DataZwrotu.HasValue ? DataZwrotu.Value.ToShortDateString() : "brak";
        return $"{Id} | {Uzytkownik.Imie} {Uzytkownik.Nazwisko} | {Sprzet.Nazwa} | Od: {DataWypozyczenia:yyyy-MM-dd} | Do: {TerminZwrotu:yyyy-MM-dd} | Zwrot: {zwrot} | Kara: {Kara} zł";
    }
}