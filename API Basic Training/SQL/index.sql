use DDL;
-- creating index
CREATE INDEX 
	idx_p01_f02_f03
ON 
	emp01 (p01f02,p01f03); -- firstname

-- using index
explain analyze
SELECT 
	p01f02,
	p01f03,
	p01f04,
	p01f05
FROM 
	emp01 
WHERE	
	p01f02 = 'Dimple';


