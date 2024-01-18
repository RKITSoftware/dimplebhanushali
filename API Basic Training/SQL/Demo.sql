-- Create Database
CREATE DATABASE demo;

-- DDL: Create Table (College)
CREATE TABLE col01 (
    l01f01 INT PRIMARY KEY NOT NULL AUTO_INCREMENT COMMENT 'College ID',
    l01f02 VARCHAR(50) COMMENT 'College Name',
    l01f03 VARCHAR(50) COMMENT 'College Location' 
) Comment 'College';

-- DDL: Create Table (Student)
CREATE TABLE stu01 (
    u01f01 INT PRIMARY KEY AUTO_INCREMENT COMMENT 'Student ID',
    u01f02 VARCHAR(25) COMMENT 'Student Name',
    u01f03 INT COMMENT 'Student Age',
    l01f01 INT comment 'College ID',
    CONSTRAINT fk_stu01_student_college FOREIGN KEY (l01f01) REFERENCES stu01_college(l01f01)
) Comment 'Student';

-- DML: Insert Data into college
INSERT INTO col01 (l01f02, l01f03)
VALUES 
	('VVP' , 'Dwarka'),
    ('RK University', 'Rajkot'), 
    ('AVPTI', 'Dwarka'), 
    ('Darshan University', 'Surat'); 

-- DML: Insert Data into student
INSERT INTO stu01 (u01f02, u01f03, l01f01)
VALUES 
    ('Dimple', 20, 1), 
    ('Pankaj', 22, 2), 
    ('Ankit', 21, 3),
    ('Vanshika', 25, 3);


-- Select Data from stu01 and col01 with Join
SELECT 
    u01f01,
    u01f02,
    u01f03,
    l01f02 AS college_name,
    l01f03 AS college_location
FROM 
    stu01 u01
INNER JOIN
    col01 l01 ON u01.u01f01 = l01.l01f01;

-- DDL: Create View
CREATE VIEW vws_stu01 AS
SELECT u01f01, u01f02, u01f03 FROM stu01 ;

select * from vws_stu01;
-- DDL: Create Index
CREATE INDEX idx_u01_f02 ON stu01(u01f02);

-- DML: Update Data
UPDATE stu01
SET u01f03 = 23
WHERE u01f02 = 'Ankit';

-- DML: Delete Data
DELETE FROM col01 WHERE l01f01 = 3;

SELECT AVG(u01f03) AS AverageAge FROM stu01;

-- Subquery: Use a subquery
SELECT 
    u01f01,
    u01f02,
    u01f03,
    l01f01 AS CollegeID
FROM 
    stu01
WHERE 
    u01f01 IN (SELECT l01f01 FROM col01 WHERE l01f03 = 'Dwarka');

EXPLAIN SELECT * FROM stu01 WHERE u01f01 = 1;
