namespace WypozyczalniaSp.Model;

public class Rzutnik : Sprzet
{
    public int JasnoscAnsi { get; }
    public string Rozdzielczosc { get; }

    public Rzutnik(string id, string nazwa, int jasnoscAnsi, string rozdzielczosc)
        : base(id, nazwa)
    {
        JasnoscAnsi = jasnoscAnsi;
        Rozdzielczosc = rozdzielczosc;
    }

    public override string ToString()
    {
        return $"{base.ToString()} | Jasnosc: {JasnoscAnsi} ANSI | Rozdz.: {Rozdzielczosc}";
    }
}