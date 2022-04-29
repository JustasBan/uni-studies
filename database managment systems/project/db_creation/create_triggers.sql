CREATE FUNCTION TikSiandien()
RETURNS TRIGGER AS $$
BEGIN
IF(
    NEW.uzsakymo_data != CURRENT_DATE
    )
THEN
    RAISE EXCEPTION 'Uzsakymai registruojami tik siandien';
END IF;
RETURN NEW;
END; $$
LANGUAGE plpgsql;

CREATE TRIGGER UzsakymasTikSiandien
BEFORE INSERT OR UPDATE ON Uzsakymai
FOR EACH ROW
EXECUTE PROCEDURE TikSiandien()
;

-------------------------------------------------------------

CREATE FUNCTION MaxUzsakymai()
RETURNS TRIGGER AS $$
BEGIN
IF(
    SELECT COUNT(*)
    FROM Uzsakymai
    WHERE Uzsakymai.klientas = NEW.klientas
	    AND Uzsakymai.uzsakymo_data = CURRENT_DATE
    )>=3
THEN
    RAISE EXCEPTION 'Perdaug uzsakymu';
END IF;
RETURN NEW;
END; $$
LANGUAGE plpgsql;

CREATE TRIGGER MaxUzsakymai
BEFORE INSERT OR UPDATE OF Uzsakymo_data ON Uzsakymai
FOR EACH ROW
EXECUTE PROCEDURE MaxUzsakymai()
;

 