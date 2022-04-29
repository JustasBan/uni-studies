SELECT K.isbn, K.pavadinimas, SUM(COALESCE(verte, 10)) AS bendra_verte,
    COUNT(*) AS visu_sk, COUNT(E.skaitytojas) AS paimtu_sk,
    COUNT(*)-COUNT(E.skaitytojas) AS nepaimta_sk
FROM stud.knyga AS K, stud.egzempliorius AS E
WHERE K.isbn = E.isbn
GROUP BY K.isbn, K.pavadinimas
HAVING COUNT(*)-COUNT(E.skaitytojas) > 1 --nepaimtu sk > 1
ORDER BY nepaimta_sk
;
