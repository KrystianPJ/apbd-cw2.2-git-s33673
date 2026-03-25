using WypozyczalniaSp.Model;

namespace WypozyczalniaSp.Serwisy;

public class PolitykaWypozyczen
{
    public int PobierzLimitDla(Uzytkownik uzytkownik)
    {
        return uzytkownik.Typ switch
        {
            TypUzytkownika.Sluchacz => 2,
            TypUzytkownika.PracownikUczelni => 5,
            _ => 0
        };
    }

    public decimal ObliczKare(DateTime terminZwrotu, DateTime dataZwrotu)
    {
        if (dataZwrotu.Date <= terminZwrotu.Date)
            return 0;

        int dniSpoznienia = (dataZwrotu.Date - terminZwrotu.Date).Days;
        return dniSpoznienia * 15m;
    }
}