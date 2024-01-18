use DDL;
-- Count() -- Counts the number of rows in a set.
SELECT 
	COUNT(emp01f01) 
FROM 
	emp01;

-- SUM() -- Calculates the sum of values in a numeric column.

SELECT 
	SUM(emp01f05) 
FROM 
	emp01;

-- AVG() --Calculates the average of values in a numeric column.
SELECT 
	AVG(emp01f05) 
FROM 
	emp01;

-- MIN() -- Returns the minimum value in a set.
SELECT 
	MIN(emp01f05) 
FROM 
	emp01;

-- MAX()--Returns the maximum value in a set.
SELECT 
	MAX(emp01f05) 
FROM 
	emp01;

-- Here's a using multiple aggregate functions with the GROUP BY clause

SELECT 
	COUNT(emp01f01) AS total_emp01s,
    AVG(emp01f05) AS average_salary,
    MIN(emp01f05) AS min_salary,
    MAX(emp01f05) AS max_salary
FROM 
	emp01

