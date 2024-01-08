USE 
	Practice;

-- Count

SELECT 
	COUNT(OrderId)
FROM 
	Orders;
    
-- Sum

SELECT 
	SUM(Price)
FROM 
	Products
WHERE 
	Price < 100;

-- Average

SELECT 
	AVG(Price)
FROM 
	Products;

-- Minimum

SELECT 
	MIN(Price)
FROM 
	Products
WHERE
	PRICE > 100;

-- Maximum

SELECT 
	MAX(Price)
FROM 
	Products
WHERE
	PRICE < 200;