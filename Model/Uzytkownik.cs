namespace WypozyczalniaSp.Model;

public abstract class Uzytkownik
{
    public string Id { get; }
    public string Imie { get; }
    public string Nazwisko { get; }
    public TypUzytkownika Typ { get; }

    protected Uzytkownik(string id, string imie, string nazwisko, TypUzytkownika typ)
    {
        Id = id;
        Imie = imie;
        Nazwisko = nazwisko;
        Typ = typ;
    }

    public override string ToString()
    {
        return $"{Id} | {Imie} {Nazwisko} | Typ: {Typ}";
    }
}