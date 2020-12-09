# IRF_Project
Amit fel kell használnom a projekben:
- Adatbázis létrehozása, kapcsolódás és legalább egy tábla adatainak beolvasása
-LINQ lekérdezés használata legalább egy WHERE feltétellel
-Formázott Excel létrehozása a rendelkezésre álló adatokból
-Véletlenszám generálás és felhasználás

Ötletem:
Egy olyan lottózós játék, amiben a felhasználó lottózhat és az eltalált számok alapján nyereményt kap.
1) A felhasználó megadja a nevét, a születési dátumát, a lakhelyét (ha nem múlt el 18 --> hibaüzenet jön fel, hogy nem játszhat).
2) Amikor megadja a 5 db lottószámát és rámegy a 'Játszom' gombra,  a gép a 25 számból generál 5 véletlen számot, ami a kihúzott számok lesznek.
A játékos ez alapján tudja meg hányas találata volt.
--> a felhasználó adataira és statisztikáira(azaz mennyit nyert, hányas találata volt) egy adatbázist hoznék létre és oda kerülnek mentésre.
Ezekből az adatokból később formázott excelt hoznék létre.
A LINQ lekérdezéshez eredetileg  egy legördülő menüt szerettem volna, az adatbázisban szereplő különböző nevekből, és ebből kiválasztva megjelenik az adott névhez tartozó összes játék adata.
