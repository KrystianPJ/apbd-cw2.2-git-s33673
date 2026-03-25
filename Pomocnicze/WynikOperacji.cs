namespace WypozyczalniaSp.Pomocnicze;

public class WynikOperacji
{
    public bool CzySukces { get; }
    public string Komunikat { get; }

    private WynikOperacji(bool czySukces, string komunikat)
    {
        CzySukces = czySukces;
        Komunikat = komunikat;
    }

    public static WynikOperacji Sukces(string komunikat) => new(true, komunikat);
    public static WynikOperacji Blad(string komunikat) => new(false, komunikat);
}