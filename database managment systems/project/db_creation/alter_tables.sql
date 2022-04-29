ALTER TABLE Asmuo
    ADD CONSTRAINT IKlientus
    FOREIGN KEY (ID) REFERENCES Klientai
		     ON DELETE CASCADE
		     ON UPDATE CASCADE
;

ALTER TABLE Imone
    ADD CONSTRAINT IKlientus
    FOREIGN KEY (ID) REFERENCES Klientai
		     ON DELETE CASCADE
		     ON UPDATE CASCADE
;

ALTER TABLE Uzsakymai
    ADD CONSTRAINT IKlientus
    FOREIGN KEY (Klientas) REFERENCES Klientai
			   ON DELETE RESTRICT
			   ON UPDATE RESTRICT
;

ALTER TABLE Uzsakymai
    ADD CONSTRAINT IProdukcija
    FOREIGN KEY (Produkcija) REFERENCES Produkcija
			     ON DELETE RESTRICT
			     ON UPDATE RESTRICT
;

CREATE UNIQUE INDEX Index__kontaktas
		    ON Klientai(Kontaktas)
;

CREATE INDEX Index_mediena 
		    ON Produkcija(Mediena)
;