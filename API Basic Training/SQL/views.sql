-- view
-- view is a virtual table which is based on sql statements and conditions.
-- view has rows and columns as real tables which can be initialized or created as well as updated and dropped

-- use wschoools database

USE 
	Practice;
    
-- create view
-- France Customers 

CREATE VIEW
	CutomersView
AS 
SELECT
	CustomerID,
    CustomerName,
    City
FROM
	Customers
WHERE
	Country = 'London';
    
-- How to use view

SELECT
	CustomerID,
    CustomerName,
    City
FROM
	CutomersView;

-- UPDATE VIEW

CREATE OR REPLACE VIEW
	CutomersView
AS 
SELECT
	CustomerID,
    CustomerName,
    City,
    Address
FROM
	Customers
WHERE
	Country = 'France';

SELECT
	CustomerID,
    CustomerName,
    City,
    Address
FROM
	CutomersView;
    
-- Drop(Discard) View

DROP VIEW
	CutomersView;
