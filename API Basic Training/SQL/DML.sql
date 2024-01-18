
use DML;
-- SELECT: Retrieve data
SELECT 
	emp02f01, -- employeeid 
	emp02f02, -- firstname
	emp02f03, -- lastname
    emp02f04 -- deparment
FROM 
	emp02 
WHERE 
	emp02f04 = 'IT';

-- INSERT: Add a new employee
INSERT INTO 
	emp02(emp02f02, emp02f03, emp02f04) 
VALUES 
	('Ankit', 'Katarmal', 'Marketing');

-- UPDATE: Modify employee information
UPDATE 
	emp02 
SET 
	emp02f04 = 'Finance' 
WHERE 
	emp02f01 = 3;

-- DELETE: Remove an employee
DELETE FROM 
	emp02 
WHERE 
	emp02f01 = 4;
