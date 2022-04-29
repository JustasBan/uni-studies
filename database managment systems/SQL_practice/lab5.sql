SELECT DISTINCT Table_Schema, Table_name
FROM Information_Schema.table_privileges
WHERE privilege_type = 'SELECT' AND (grantee = 'PUBLIC' OR grantee = CURRENT_USER)
ORDER BY 1, 2
;