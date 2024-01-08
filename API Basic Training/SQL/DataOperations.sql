-- DDL

-- Data Defination Languguage

USE Practice;

-- Create

-- Create table of Employee

CREATE TABLE 
	Customers(						-- Employee
		CustomerId INT PRIMARY KEY, -- CustomerId
		CustomerName VARCHAR(50),		-- CustomerName
		Country VARCHAR(50),		-- Country
		Age INT(2),			-- Age
		MobileNumber INT(10)			-- MobileNumber
);

-- Create table of employee by using another employee table

CREATE TABLE 
	Cust 						-- Employee
AS SELECT
	Id,
    FirstName,
    Organization
FROM
	Customers;
	
-- Drop                                        
-- Used to delete whole database or table

DROP TABLE
	Cust;

-- Alter
-- Used to add, remove or modify columns in existing table

-- Add Columns

ALTER TABLE 
	Customers
ADD(
	Salary INT,			
    Remarks VARCHAR(50)	
);

-- Drop Column

ALTER TABLE
	Customers
DROP COLUMN
	Remarks;
    
-- Modifiing Column 
-- Datatype is modifies to varchar(30)

ALTER TABLE
	Customers
MODIFY COLUMN
	Remarks VARCHAR(30);

-- Truncate
-- Removes all data within database or table

TRUNCATE TABLE 
	employee;


-- DML
-- Data Manupalation Language

-- Insert

INSERT INTO
	Customers
VALUES (1001, "Dimple", "India", 23, 60, 50000),
	   (1002, "Ankit", "London", 26, 70, 65000),
       (1003, "Ekta", "UK", 22, 80, 40000),
	   (1004, "Jalpa", "USA", 23, 90, 55000);

-- Update

UPDATE 
	Customers
SET 
	CustomerName = 'Sachin'
WHERE
	CustomerId = 1002;

-- Delete

DELETE FROM
	Customers
WHERE 
	CustomerId = 1003;

-- DCL

-- Use Practice

USE 
	Practice;

-- Create Table which contains one auto increment field
-- Auto Increment strats from 1001 with increment of 1
-- while both contains 1 as default values.

CREATE TABLE 
	Employees (
		Id int PRIMARY KEY AUTO_INCREMENT,
		FirstName varchar(255),
        LastName varchar(255),
        Organization varchar(255),
        Salary int
    ) AUTO_INCREMENT = 1001;

-- Enter Data into Table

INSERT INTO
	Employees 
    (FirstName, LastName, Organization, Salary)
VALUES 
	("Sachin", "Tendulkar", "ABCD", 1000000),
    ("Mahindra", "Dhoni", "ABCD", 1200000),
    ("Virat", "Kohli", "ABCD", 450000),
    ("Surya", "Yadav", "PQRS", 800000),
    ("Ravindra", "Jadeja", "MNPQ", 1000000);
    
-- Show Table Data

SELECT 
	Id, 
    FirstName, 
    LastName, 
    Organization, 
    Salary
FROM 
	Employees;