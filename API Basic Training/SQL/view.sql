use ddl;
-- Create a view named 'employee_view'
CREATE VIEW vws_emp01 AS
SELECT
    emp01.p01f01,
    emp01.p01f02,
    emp01.p01f03,
    emp01.p01f04,
    dep01.p01f01,
    dep01.p01f02
FROM
    emp01
JOIN
    dep01 ON emp01.p01f01 = dep01.p01f01;

select 
	* 
from 
	employee_view;
