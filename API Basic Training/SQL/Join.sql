use DDL;
-- Create a "dept01" table
CREATE TABLE 
	dep01 (
		p01f01 INT NOT NULL PRIMARY KEY AUTO_INCREMENT, -- departmentId
		p01f02 VARCHAR(50) -- departmenetName
	);

-- Insert some sample data into the "dept01" table
INSERT INTO 
	dep01 (p01f02) 
VALUES
	('IT'),
	('Marketing'),
	('Sales');


-- The INNER JOIN keyword selects records that have matching values in both tables.
SELECT 
	emp01.p01f01, 
	emp01.p01f02, 
	emp01.p01f03, 
	dep01.p01f02
FROM 
	emp01
Inner JOIN 
	dep01 
ON 
	emp01.p01f01 = dep01.p01f01;

-- The LEFT JOIN keyword returns all records from the left table (emp01), 
-- and the matched records from the right table (dept01). 
-- The result is NULL from the right side if there is no match.
SELECT 
	emp01.p01f01, 
	emp01.p01f02, 
	emp01.p01f03, 
	dep01.p01f02
FROM 
	emp01
LEFT JOIN 
	dep01 
ON 
	emp01.p01f01 = dep01.p01f01;

-- The RIGHT JOIN keyword returns all records from the right table (dept01), 
-- and the matched records from the left table (emp01s). 
-- The result is NULL from the left side when there is no match.
SELECT 
	emp01.p01f01, 
	emp01.p01f02, 
	emp01.p01f03, 
	dep01.p01f02
FROM 
	emp01
RIGHT JOIN 
	dep01 
ON 
	emp01.p01f01 = dep01.p01f01;

-- The FULL JOIN keyword returns all records when there is a match in either the left (emp01) or the right (dept01) table records.

	SELECT 
		emp01.p01f01, 
		emp01.p01f02, 
		emp01.p01f03, 
		dep01.p01f02
	FROM 
		emp01 
	FULL outer JOIN 
		dep01 
	ON 
		emp01.p01f01 = depe01.p01f01;
    





