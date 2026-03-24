namespace WypozyczalniaSp.Model;

public class Laptop : Sprzet
{
    public int RamGb { get; }
    public string Procesor { get; }

    public Laptop(string id, string nazwa, int ramGb, string procesor)
        : base(id, nazwa)
    {
        RamGb = ramGb;
        Procesor = procesor;
    }

    public override string ToString()
    {
        return $"{base.ToString()} | RAM: {RamGb} GB | CPU: {Procesor}";
    }
}