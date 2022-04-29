DROP VIEW Azuolo_produkcija;
DROP VIEW Medienos_kiekis;

ALTER TABLE Uzsakymai
    DROP CONSTRAINT Iprodukcija;
ALTER TABLE Uzsakymai
    DROP CONSTRAINT IKlientus;

ALTER TABLE Imone
    DROP CONSTRAINT IKlientus;
ALTER TABLE Asmuo
    DROP CONSTRAINT IKlientus;

DROP TABLE Uzsakymai;
DROP TABLE Produkcija;
DROP TABLE Klientai;
DROP TABLE Asmuo;
DROP TABLE Imone;