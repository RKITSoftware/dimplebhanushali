CREATE USER 'user'@'localhost';

-- Grant SELECT privilege to the user
GRANT SELECT,INSERT ON 
	emp01 
TO 
	'user'@'localhost';

-- REVOKE INSERT privilege from the same user
REVOKE INSERT ON 
	emp01 
FROM 
	'user'@'localhost';
