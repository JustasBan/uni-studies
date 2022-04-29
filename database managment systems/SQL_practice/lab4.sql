WITH KnygSk (isbn, pavadinimas, leidykla, kiekis) AS(
	SELECT K.isbn, K.pavadinimas, K.leidykla, COUNT(*)
	FROM stud.knyga AS K, stud.egzempliorius as E
	WHERE K.isbn = E.isbn
	GROUP BY K.isbn, K.pavadinimas, K.leidykla),
    MaxKnygSk (maxKiekis) AS(
	SELECT MAX(kiekis)
	FROM KnygSK)
SELECT pavadinimas, isbn, leidykla,kiekis, maxKiekis
FROM KnygSk, MaxKnygSk
WHERE kiekis != maxKiekis
;