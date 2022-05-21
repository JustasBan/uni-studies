SELECT Table_Schema, Table_Name
FROM Information_Schema.Table_privileges
WHERE Table_Schema = 'stud'
    EXCEPT
    SELECT Table_schema, Table_Name
    FROM Information_Schema.Views
order by 1, 2
;