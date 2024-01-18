create database employees;
use employees;
-- Create  tables
CREATE TABLE emp01 (
    p01f01 INT PRIMARY KEY,  -- employeeID 
    p01f02 VARCHAR(50), -- Employee first Name 
    p01f03 VARCHAR(50) -- Employee Last Name
);

CREATE TABLE emp02 (
    p02f01 INT PRIMARY KEY, -- new employeeID
    p02f02 VARCHAR(50), -- New employee FirstName
    p02f03 VARCHAR(50)-- New employee LastName
);

-- Insert data
INSERT INTO 
	emp01 (p01f01, p01f02, p01f03) 
VALUES
	(1, 'Dimple', 'Mithiya'),
	(2, 'Pankaj', 'Mithiya');

INSERT INTO 
	emp02 (p02f01, p02f02, p02f03) 
VALUES
	(2, 'Pankaj', 'Mithiya'),
	(3, 'Ankit', 'Katarmal'),
	(4, 'Kamal', 'Chavda');

-- Combine employees from both tables using UNION
SELECT 
	p01f01, 
	p01f02, 
	p01f03 
FROM 
	emp01
UNION 
SELECT 
	p02f01, 
	p02f02, 
	p02f03 
FROM 
	emp02;
-- Union all don't remove duplicate rows
SELECT 
	p01f01, 
	p01f02, 
	p01f03 
FROM 
	emp01
UNION ALL
SELECT 
	p02f01, 
	p02f02, 
	p02f03 
FROM 
	emp02;
