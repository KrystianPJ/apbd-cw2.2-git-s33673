namespace WypozyczalniaSp.Serwisy;

public class GeneratorIdentyfikatorow
{
    private int _licznikSprzetu = 1;
    private int _licznikUzytkownikow = 1;
    private int _licznikWypozyczen = 1;

    public string NoweIdSprzetu() => $"SPR-{_licznikSprzetu++:000}";
    public string NoweIdUzytkownika() => $"UZY-{_licznikUzytkownikow++:000}";
    public string NoweIdWypozyczenia() => $"WYP-{_licznikWypozyczen++:000}";
}