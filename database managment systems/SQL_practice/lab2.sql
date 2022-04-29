select distinct vardas, pavarde, K.isbn
from stud.knyga as K, stud.autorius as A
where K.isbn = A.isbn
order by vardas, pavarde
;