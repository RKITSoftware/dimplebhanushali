use DDL;

-- Retrieve the first 2 rows from the "employee" table it is like LIMIT
SELECT 
	p01f01, 
	p01f02,
	p01f03,
	p01f04,
	p01f05
FROM 
	emp01
Limit 
	2,3;

-- Retrieve next 2 row after 2 row from the "employee" table 
SELECT 
    emp01f01,
    emp01f02,
    emp01f03,
    emp01f04,
    emp01f05
FROM 
    emp01
LIMIT 
    2 OFFSET 2;
