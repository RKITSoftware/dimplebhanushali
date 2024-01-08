-- index

USE 
	Practices;
   
-- without using index

SELECT
	CustomerID,
    CustomerName,
    City
FROM
	Customers
WHERE
	CustomerID > 85;
    
EXPLAIN SELECT
	CustomerID,
    CustomerName,
    City
FROM
	Customers
WHERE
	CustomerID > 85;

-- with index
    
CREATE INDEX
	Index_ID 
ON
	Customers(CustomerID);
    
EXPLAIN SELECT
	CustomerID,
    CustomerName,
    City
FROM
	Customers
WHERE
	CustomerID > 85;
    
-- Unique Index
-- Doesnt allow duplicate values

CREATE UNIQUE INDEX
	Index_Contact
ON
	Customers(CustomerID, CustomerName);