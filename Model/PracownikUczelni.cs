namespace WypozyczalniaSp.Model;

public class PracownikUczelni : Uzytkownik
{
    public string Dzial { get; }
    public string Stanowisko { get; }

    public PracownikUczelni(string id, string imie, string nazwisko, string dzial, string stanowisko)
        : base(id, imie, nazwisko, TypUzytkownika.PracownikUczelni)
    {
        Dzial = dzial;
        Stanowisko = stanowisko;
    }

    public override string ToString()
    {
        return $"{base.ToString()} | Dzial: {Dzial} | Stanowisko: {Stanowisko}";
    }
}