CREATE VIEW Uzsakytas_kiekis
	    (ID, Kiekis)
AS
    SELECT Produkcija, SUM(Kiekis)
    FROM Uzsakymai
    GROUP BY Produkcija
;

CREATE VIEW Medienos_kiekis
	    (ID, Kiekis)
AS
    SELECT ID, SUM(Kiekis)
    FROM Produkcija
    GROUP BY ID
;

CREATE MATERIALIZED VIEW Balansas
		(ID, Balansas)
AS
    SELECT U.ID, (M.Kiekis - U.Kiekis) AS Balansas
    FROM Medienos_kiekis as M, Uzsakytas_kiekis as U
    WHERE M.ID = U.ID
;