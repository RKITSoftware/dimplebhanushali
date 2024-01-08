-- Create Database

CREATE DATABASE 
	Practice;
    
-- Using a particular Database

USE 
	Practice;
    
-- Create Table

CREATE TABLE 
	Person (
		Id int,
		FirstName varchar(50),
        LastName varchar(50)
    );

-- Enter Data into Table

INSERT INTO
	Person 
VALUES (1001, "Dimple", "Mithiya"),
	   (1004, "Ankit", "Katarmal"),
       (1003, "Shiva", "Shakti"),
       (1002, "Ram", "Krishna");

-- Show Table Data

SELECT
	Id, 
    FirstName, 
    LastName
FROM 
	Person;
    
-- Sorting of Data

-- Here data is sorted by ID then FirstName then LastName in ascending order

SELECT
	Id, 
    FirstName, 
    LastName
FROM 
	Person
ORDER BY
	Id, FirstName, LastName ASC;

-- Here data is sorted by ID then FirstName then LastName in decending order

SELECT
	Id, 
    FirstName, 
    LastName
FROM 
	Person
ORDER BY
	Id, FirstName, LastName DESC;
	