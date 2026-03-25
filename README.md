WypożyczalniaSprzętu - porjekt 

Opis: 
Prosty program projektu przedstawiajacy  aplikacje konsolową działjąco w C#, która obsługuje uczelnianą wypożyczalnie sprzętu. System  umożliwai dodawanie użytkowników i sprzęt,  a także wypożyczyć zwrócić. 
//=================================================
Zastosowane elementy: 
 - Abstrakcyjna klasa Sprzet
 - klasa dziedziczące, Laptop ,Rzutnik Wypozyczenie
 Aparat, sluchacz , pracownik ,Uczleni 
 - Abstrakcyjna klasa Uzytkownik 
//=================================================
W projekcie w folderach model  zawarte są wyłącznie dane i podstawowoe zachowanie obiektów, natomiast w Serwis_Wypozyczalni odpowida za operacje biznesowe (między innymi: wypozyczenie zwrot, raport).
Kolejena Kalsa PolitykaWypozyczen - przechowuje reguły biznesowe dotyczące limitów i naliczanie kar. 
Klasa Program.cs zawiera przygłady  jak działa program. 
//=================================================
Kohezja i coupling 
Strałem sie zachaowac wysoką kohezję przez przypsianie każdej klasie jednje głownej odpoweidzi: 
Przykładowo: 
- PolitykWypozyczen odpowida tylko za  limity i kary
-SerwisWypozyczalni tylko za operacje systemowe 
//=================================================
Coupling  zmniejszenie osiągnełem po przez  odzzielnie modeli od logiki biznesowej i interfejsu. Dzięki temu zmiana sposobi liczenia kar lub limitów nie wymga zmian w wielu miejscach. 
//=================================================
Dlaczego taki podział ? 
Wybrałm taki podział, gdyż jest czytelny i ułatwia rozwój i dobrze wygląda dla oka. 

uruchomienie: 
Otowrzyć tewrminal  wpsiać 
 dotnet build 
 dotnet run 
 Powino zadziałać. 
 

