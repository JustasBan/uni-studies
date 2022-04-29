--Netinkama rusis
--INSERT INTO Produkcija (Mediena, Ilgis, Plotis, Aukstis, Kiekis)
--VALUES 	('Pusis', 5, 1, 25, 44)
--;

--Automatiskai generuojamas ID
--INSERT INTO Produkcija (Mediena, Ilgis, Plotis, Aukstis, Kiekis)
--VALUES 	('Azuolas', 2, 10, 25, 14)
--;

--toks kontaktas jau egzistuoja
--INSERT INTO Klientai (ID, Kontaktas)
--VALUES 	(4, '+37063424371')
--;

--Per trumpas imones kodas
--INSERT INTO Imone (ID, Pavadinimas, Imones_kodas, Adresas)
--VALUES (3, 'Petras ir co', '123', 'Vasaros 16')
--;

--Uzsakymo kiekis per mazas
--INSERT INTO Uzsakymas 
--    (Klientas, Produkcija, Uzsakymo_dat, Atsiemimo_data, Kiekis)
--VALUES (3, 1, CURRENT_DATE, CURRENT_DATE+1, 0)
--;

--Uzsakoma vakar dienos laiku
--INSERT INTO Uzsakymas 
--    (Klientas, Produkcija, Uzsakymo_dat, Atsiemimo_data, Kiekis)
--VALUES (3, 1, CURRENT_DATE-1, CURRENT_DATE+1, 1)
--;

--Klientas uzsako per daug
--INSERT INTO Uzsakymas 
--    (Klientas, Produkcija, Uzsakymo_dat, Atsiemimo_data, Kiekis)
--VALUES 	(3, 1, CURRENT_DATE, CURRENT_DATE+1, 1),
--	(3, 1, CURRENT_DATE, CURRENT_DATE+1, 1),
--	(3, 1, CURRENT_DATE, CURRENT_DATE+1, 1),
--	(3, 1, CURRENT_DATE, CURRENT_DATE+1, 1)
--;

--Klientas uzsako per daug (update)
UPDATE Uzsakymai SET uzsakymo_data = CURRENT_DATE
WHERE klientas = 1;
;

REFRESH MATERIALIZED VIEW Balansas;