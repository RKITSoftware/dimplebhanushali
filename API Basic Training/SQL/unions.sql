-- Union

SELECT
	'EMP' AS Type,
    EMployeeID,
    FirstName
FROM 
	employees
UNION 
SELECT
	'cust' AS Type,
	CustomerID,
    CustomerName
FROM
	Customers;

-- Union All 

SELECT
	'EMP' AS Type,
    EMployeeID,
    FirstName
FROM 
	employees
UNION ALL
SELECT
	'cust' AS Type,
	CustomerID,
    CustomerName
FROM
	Customers;