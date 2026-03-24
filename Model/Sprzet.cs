namespace WypozyczalniaSp.Model;

public abstract class Sprzet
{
    public string Id { get; }
    public string Nazwa { get; }
    public StatusSprzetu Status { get; private set; }

    protected Sprzet(string id, string nazwa)
    {
        Id = id;
        Nazwa = nazwa;
        Status = StatusSprzetu.Dostepny;
    }

    public void UstawStatus(StatusSprzetu nowyStatus)
    {
        Status = nowyStatus;
    }

    public override string ToString()
    {
        return $"{Id} | {Nazwa} | Status: {Status}";
    }
}