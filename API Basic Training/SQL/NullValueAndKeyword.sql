-- Use Practice

USE 
	Practice;

-- Adding new column in Person Table (Practice)
-- which contains null value

ALTER TABLE 
	Person
ADD 
	City varchar(100);

-- Adding new column in Person Table (Practice)
-- which contains not null value
-- If we're adding columns then it's showing empty values.

ALTER TABLE 
	Person
ADD 
	Address varchar(255) NOT NULL;

-- Adding new column in Person Table (Practice)
-- which contains not null value
-- We can use default values too.

ALTER TABLE 
	Person
ADD 
	Country varchar(255) NOT NULL DEFAULT "India";

-- Add row into table

INSERT INTO
	Person
	(Id, FirstName, City, Address)
VALUES
	(1005, "Jalpa", "Rajkot", "Rajkot");

-- Show Table Data

SELECT
	Id, 
    FirstName, 
    LastName, 
    City, 
    Address, 
    Country
FROM
	Person;