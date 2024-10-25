:it: Made in Italy. Il primo prodotto maui ad utilizzare i Resource Dictionary nel mondo.

## TrumpSuitGame
Il gioco della briscola in maui, internazionalizzato in inglese, francese, spagnolo, tedesco, italiano e portoghese.
Su windows per cambiare il dialetto é sufficente modificare le impostazioni di sistema, su android 14 e 15 pure, sui precedenti bisogna disinstallare e reinstallare il programma.
Il gioco èpiù godibile sui tablet in modalità portrait, non landscape.
Vi segnalo che questa app è stata utilizzata da google per una campagna pubblicitaria contro la ludopatia.


## La campagna pubblicitaria di google

Personalmente ho visto un po' in giro i nuovi giochini per android. Tutti molto semplici e banali, però sono delle macchinette mangiasoldi.

Sono gratuiti, però per passare la base dal livello 40 al livello 39 richiedono 500 euro: 300 per il legno, 200 per il fuoco e 6 giorni di tempo col cellulare acceso senza fare nulla. Io personalmente a queste condizioni preferisco NON GIOCARE, perché costa tutto, compresa la corrente e la batteria nuova del cellulare.

I giochi di carte anche sono gratuiti, però si accoppiano le carte per vincere. La briscola di nonno nanni non si capisce manco se è briscola, scopa o asso piglia tutto.

La CBriscola Maui (Trump Suit Game internazionalizzato) sapete cosa promette? Per 5 euro LORDI ed UNA TANTUM promette un numero ben determinato e molto alto di partite al giorno, un gioco che segue ciecamente le regole della briscola classica (al meglio delle due partite), con un mazzo di carte semplice da capire ed una IA che cerca di prendere il maggior numero di carte, senza accoppiarsele, infatti il 3 può venire tranquillamente mangiato dall'asso ed AGGIORNAMENTI GRATUITI.

Vi sfido a fare di meglio.

## Screenshots su windows

![Screenshot 2024-10-25 015619](https://github.com/user-attachments/assets/5656666e-8ea0-4dec-a008-7e02745ddaa5)
![Screenshot 2024-10-25 015337](https://github.com/user-attachments/assets/db4de058-f882-4cfb-84ad-ac0bd99f815f)
![Screenshot 2024-10-25 015325](https://github.com/user-attachments/assets/16fba492-dc81-47ac-a575-686c389d2c45)
![Screenshot 2024-10-25 015304](https://github.com/user-attachments/assets/70f771f5-9485-4672-86ad-cab488c07bbe)



## Video di presentazione

https://1drv.ms/u/s!ApmOB0x2yBN0k5QhMlcaOk024Ev_1A

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

## Bug noti
Il programma accetta anche valore 0 e negativi come tempo di risposta. In caso di valore 0 non si può giocare, in caso di valori negativi il programma crasha.

## Donazioni

http://numerone.altervista.org/donazioni.php

## Bibliografia

https://github.com/GiulianoSpaghetti/TrumpSuitGame
