namespace WypozyczalniaSp.Model;

public class Aparat : Sprzet
{
    public int Megapiksele { get; }
    public bool Czy4K { get; }

    public Aparat(string id, string nazwa, int megapiksele, bool czy4K)
        : base(id, nazwa)
    {
        Megapiksele = megapiksele;
        Czy4K = czy4K;
    }

    public override string ToString()
    {
        return $"{base.ToString()} | MP: {Megapiksele} | 4K: {(Czy4K ? "Tak" : "Nie")}";
    }
}