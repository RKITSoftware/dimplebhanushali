Use
    Practice;

-- Create Orders table
CREATE TABLE IF NOT EXISTS Orders (
    OrderID INT PRIMARY KEY AUTO_INCREMENT,
    CustomerID INT,
    OrderDate DATE,
    ProductID INT,
    Quantity INT
);

insert into Orders (CustomerID, OrderDate, ProductID, Quantity) values (1004, '22-09-2023', 93, 37);
insert into Orders (CustomerID, OrderDate, ProductID, Quantity) values (1003, '17-05-2023', 43, 4);
insert into Orders (CustomerID, OrderDate, ProductID, Quantity) values (1001, '29-11-2023', 90, 19);
insert into Orders (CustomerID, OrderDate, ProductID, Quantity) values (1004, '26-05-2023', 98, 5);
insert into Orders (CustomerID, OrderDate, ProductID, Quantity) values (1003, '09-08-2023', 82, 45);
insert into Orders (CustomerID, OrderDate, ProductID, Quantity) values (1004, '22-01-2023', 68, 50);
insert into Orders (CustomerID, OrderDate, ProductID, Quantity) values (1002, '21-04-2023', 13, 17);
insert into Orders (CustomerID, OrderDate, ProductID, Quantity) values (1004, '26-08-2023', 62, 48);
insert into Orders (CustomerID, OrderDate, ProductID, Quantity) values (1003, '04-11-2023', 100, 12);
insert into Orders (CustomerID, OrderDate, ProductID, Quantity) values (1003, '30-09-2023', 85, 22);
insert into Orders (CustomerID, OrderDate, ProductID, Quantity) values (1003, '14-12-2023', 55, 45);
insert into Orders (CustomerID, OrderDate, ProductID, Quantity) values (1003, '18-07-2023', 22, 48);
insert into Orders (CustomerID, OrderDate, ProductID, Quantity) values (1004, '03-01-2024', 17, 44);
insert into Orders (CustomerID, OrderDate, ProductID, Quantity) values (1003, '24-08-2023', 89, 12);
insert into Orders (CustomerID, OrderDate, ProductID, Quantity) values (1002, '16-02-2023', 43, 8);
insert into Orders (CustomerID, OrderDate, ProductID, Quantity) values (1001, '17-03-2023', 19, 36);
insert into Orders (CustomerID, OrderDate, ProductID, Quantity) values (1001, '27-10-2023', 25, 14);
insert into Orders (CustomerID, OrderDate, ProductID, Quantity) values (1001, '24-01-2023', 11, 48);
insert into Orders (CustomerID, OrderDate, ProductID, Quantity) values (1002, '30-08-2023', 14, 49);
insert into Orders (CustomerID, OrderDate, ProductID, Quantity) values (1004, '12-05-2023', 74, 39);


-- join

-- Inner Join
SELECT 
	Orders.OrderID,
    Customers.CustomerID,
    OrderDate
FROM
	Customers INNER JOIN Orders
ON
	Orders.CustomerID = Customers.CustomerID;

-- Left Join
SELECT 
	Orders.OrderID,
    Customers.CustomerID,
    OrderDate
FROM
	Customers
LEFT JOIN
	Orders
ON
	Orders.CustomerID = Customers.CustomerID;

-- Right Join
SELECT 
	Orders.OrderID,
    Customers.CustomerID,
    OrderDate
FROM
	Customers
RIGHT JOIN
	Orders
ON
	Orders.CustomerID = Customers.CustomerID;

-- Cross Join
SELECT 
	Orders.OrderID,
    Customers.CustomerID,
    OrderDate
FROM
	Customers
CROSS JOIN
	Orders
ON
	Orders.CustomerID = Customers.CustomerID;