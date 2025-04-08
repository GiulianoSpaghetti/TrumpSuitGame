:it: Made in Italy. Il primo prodotto maui ad utilizzare i Resource Dictionary nel mondo.

Il secondo software in Maui che secondo Google non crasha.

Questo gioco dimostra che la teoria dei giochi è vera: l'algorimo brevettato funziona su tutti i giochi di carte senza piatto.

Il primo prodotto distribuito di terze parti ad usare il LINQ.

![Napoli-Logo](https://github.com/user-attachments/assets/8163c808-62d3-40d3-bce3-0957e57bc26a)
![made in parco grifeo](https://github.com/user-attachments/assets/fadbf046-aeae-4f11-bda4-eb332c701d56)


## TrumpSuitGame
Quello che avete davanti non è il gioco della briscola come si intende oggi, perché oggi tutti i simulatori di briscola dicono "hai preso l'asso, bravo" e finisce lì. Quello che avete davanti è un simulatore equo e professionale, con punteggio aggiornato in tempo reale, in modo da poter decidere se "rischiare" o meno coscientemente, scritto in maui, internazionalizzato in inglese, francese, spagnolo, tedesco, italiano e portoghese.
Su windows per cambiare il dialetto é sufficente modificare le impostazioni di sistema, su android 14 e 15 pure, sui precedenti bisogna disinstallare e reinstallare il programma.
Il gioco è più godibile sui tablet in modalità portrait, non landscape.
Sembra strano a dirsi, ma il gioco è hard core perché consente di cambiare in ogni istante l'andamento della partita cosicentemente con le proprie scelte.
Vi segnalo che questa app è stata utilizzata da google per una campagna pubblicitaria contro la ludopatia.
L'assembly su cui si basa usa il linq, su android funziona, su windows no, per via delle ottimizzazioni del framework.


## La campagna pubblicitaria di google

Personalmente ho visto un po' in giro i nuovi giochini per android. Tutti molto semplici e banali, però sono delle macchinette mangiasoldi.

Sono gratuiti, però per passare la base dal livello 40 al livello 39 richiedono 500 euro: 300 per il legno, 200 per il fuoco e 6 giorni di tempo col cellulare acceso senza fare nulla. Io personalmente a queste condizioni preferisco NON GIOCARE, perché costa tutto, compresa la corrente e la batteria nuova del cellulare.

I giochi di carte anche sono gratuiti, però si accoppiano le carte per vincere. La briscola di nonno nanni non si capisce manco se è briscola, scopa o asso piglia tutto.

La CBriscola Maui (Trump Suit Game internazionalizzato) sapete cosa promette? Per 5 euro LORDI ed UNA TANTUM promette un numero ben determinato e molto alto di partite al giorno, un gioco che segue ciecamente le regole della briscola classica (al meglio delle due partite), con un mazzo di carte semplice da capire ed una IA che cerca di prendere il maggior numero di carte, senza accoppiarsele, infatti il 3 può venire tranquillamente mangiato dall'asso ed AGGIORNAMENTI GRATUITI.

Vi sfido a fare di meglio.

## Lo use case

Ormai i giochi di carte sono vecchi e vengono fatti da pochissime persone. Vi immaginate il nonno che si compra il cellulare da 150 euro, con 5 euro la briscola e si mette a giocare aspettando il pullman o la metropolitana?

Nella mia briscola può capitare che il computer abbia il 3 e l'asso di briscola, facciamo denari, e che esca un carico, a quel punto da primo di mano gioca il carico.
Il nonnino secondo di mano non ha né denari né come sopratagliare, non può prendere. Capita la prima volta, capita la seconda volta consecutiva, carica la terza volta consecutiva, a quel punto urla "Sto piombo a denari!".
E' questo il motto registrato, in entrambe le sue forme.
Negli anni 2000 capitava a casa grazie alla wxbriscola, oggi può capitare per la strada.

## Screenshots su windows

![Screenshot 2024-10-25 015619](https://github.com/user-attachments/assets/5656666e-8ea0-4dec-a008-7e02745ddaa5)
![Screenshot 2024-10-25 015337](https://github.com/user-attachments/assets/db4de058-f882-4cfb-84ad-ac0bd99f815f)
![Screenshot 2024-10-25 015325](https://github.com/user-attachments/assets/16fba492-dc81-47ac-a575-686c389d2c45)
![Screenshot 2024-10-25 015304](https://github.com/user-attachments/assets/70f771f5-9485-4672-86ad-cab488c07bbe)



## Video di presentazione

https://www.youtube.com/watch?v=mjKOcRQevEY

## Come installare

## Su Android

[![google](https://play.google.com/intl/it_it/badges/static/images/badges/en_badge_web_generic.png)](https://play.google.com/store/apps/details?id=org.altervista.numerone.trumpsuitgame)


## Come internazionalizzare il software
## Tramite ResourceDictionary (Windows e mac)
Il mio metodo non è fare uno switch case infinito, ma è fare i resource dictionary e chiamarli it_IT, en_US, fr_FR, de_DE.

Una volta realizzato il resource dictionary dopo aver importato il namespace system ed aver scritto il realativo codice xml, bisogna registrare la risorsa come resourcedictionary nell'app. Il mio metodo è di usare la denominazione internazionalizzata a due caratteri della traduzione.
Per cui es_MX (messico) è es, ma anche es_ES (spagna) è es.

Poi si legge il resourcedictionary relativo alla denominazione a due caratteri del linguaggio di sistema, ed è fatta. Se la risorsa c'è si carica, se no si carica quella di default.


## Come funziona
Per festeggiare, vi spiego come funziona il mio algoritmo brevettato:
i punti in totale sono 120, ossia 4 assi che valgono 11 punti ciascuno, 4 3 che valgono 10 punti ciascuno, 4 10 che valgono 4 punti ciascuno, 4 9 che valgono 3 punti ciascuno, 4 8 che valgono 2 punti ciascuno.
Dal momento che la matematica non è una opinione:
4x11+4x10=84.
4x4+4x3+4x2=16+12+8=36

84+36=120 punti totali

120/2 = 60, servono 61 punti per vincere

basandosi solo sui carichi si rischia di perdere, perché

84-61=23, bisogna prenderli quasi tutti e lasciare solo 23 punti di carichi

60-36=24, prendendo tutte le altre carte bastano solo 3 carichi per vincere.

## Aggiornamenti

Per windows i package msix sono platform indepedent ed in IL, per cui è sufficiente scaricarsi il nuovo dotnet framework runtime e reinstallarsi il pacchetto per ottenere il codice binario ottimizzato con le ultime patch.

## Bug noti
Se il cellulare finisce sotto un tram risulta impossibile avviare il software xD

## Donazioni

http://numerone.altervista.org/donazioni.php
