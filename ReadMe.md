# Bolero antenna kereső
## Előkszítés/telepítése
1. A release letöltése a githubról
2. A `findMyBolero.reg` file futtatása
## Használat
`FindMyBolero.exe`-t kell futtatni.
Magát az ablakot nyugodtan be lehet zárni, a tálca szélén, az értesítési területen található ikonok között ott marad a program, az ikonra kattintva újra megnyílik.
```diff
- A Program bezárásához az érteítési területen az ikonra kell jobb egérgombbal kattintani, majd a Quit feliratra kattintani.
```
A beállított IP címeket a program periodikusan automatikusan pingeli, és egy elérhető Antennát kiválaszt. Az aktív antenna mindaddig kiválasztva marad, amíg elérhető. Az aktív antennát kézzel is ki lehet választani. A Registryben a bind nevű kulcs értékére bindol a webserver, amit megnyitva automatikusan az aktív antennára irányít át a böngésző.
## IP címek szerkesztése
Az IP címek a `HKEY_LOCAL_MACHINE\SOFTWARE\FindMyBolero\Antennas` kulcsban érhetőek el. Itt bármilyen nevű karakterlánc típusú értékbe kell beírni az IP címeket.
```diff
! A programban a szerkesztés csak akkor működik, ha rendszergazdai jogokkal fut a program.
```
