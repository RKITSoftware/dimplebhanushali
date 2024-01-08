USE
	Practice;

-- Sub Queries

-- Find employee details whose employee id > 4

SELECT 
	EmployeeID,
    FirstName,
    LastName
FROM 
	employees
WHERE
	EmployeeID
IN
	(SELECT 
		EmployeeId
	FROM
		employees
	WHERE
		EMployeeID > 4
    );

-- Find details of product whose price is more than avg. price

SELECT 
	ProductID,
    ProductName,
    Price
FROM
	Products
WHERE
	ProductID
IN 
	(SELECT 
		ProductID
     FROM
		products
	 WHERE
		price > (SELECT AVG(Price) FROM Products)
	);
    
-- Corelated Subqueries

SELECT 
	ProductID,
    ProductName,
    Price
FROM 
	Products v_product
WHERE 
	Price > (SELECT
				AVG(Price)
			FROM
				Products
    );
