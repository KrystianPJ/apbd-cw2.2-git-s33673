namespace WypozyczalniaSp.Modele;

public class Sluchacz : Uzytkownik
{
    public string Kierunek { get; }
    public int Semestr { get; }

    public Sluchacz(string id, string imie, string nazwisko, string kierunek, int semestr)
        : base(id, imie, nazwisko, TypUzytkownika.Sluchacz)
    {
        Kierunek = kierunek;
        Semestr = semestr;
    }

    public override string ToString()
    {
        return $"{base.ToString()} | Kierunek: {Kierunek} | Semestr: {Semestr}";
    }
}