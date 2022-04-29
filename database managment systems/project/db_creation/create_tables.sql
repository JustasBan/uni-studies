CREATE TABLE Produkcija(
    ID			INTEGER  NOT NULL 
			GENERATED ALWAYS AS IDENTITY
			(START WITH 1 INCREMENT BY 1),
    Mediena		VARCHAR(20)
			CHECK 
			(Mediena IN 
			('Uosis', 'Berzas', 'Egle', 'Azuolas')),
    Ilgis		INTEGER,
    Plotis		INTEGER,
    Aukstis		INTEGER,
    Kiekis		INTEGER,

    PRIMARY KEY (ID)
);

CREATE TABLE Klientai(
    ID			INTEGER NOT NULL,
    Kontaktas		VARCHAR(50),

    PRIMARY KEY (ID)
);

CREATE TABLE Asmuo(
    ID			INTEGER NOT NULL,
    Vardas		VARCHAR(50),
    Pavarde		VARCHAR(50)
);

CREATE TABLE Imone(
    ID			INTEGER NOT NULL,
    Pavadinimas		VARCHAR(75),
    Imones_kodas	VARCHAR(9)
			CHECK 
			(LENGTH(Imones_kodas)=7 
			 OR LENGTH(Imones_kodas)=9),
    Adresas		VARCHAR(100)
);
    
CREATE TABLE Uzsakymai(
    Klientas 		INTEGER NOT NULL,
    Produkcija 		INTEGER NOT NULL,
    Uzsakymo_data	DATE
			DEFAULT CURRENT_DATE,
    Atsiemimo_data	DATE,
    Kiekis		INTEGER
			DEFAULT 1
			CHECK (Kiekis >= 1)
);