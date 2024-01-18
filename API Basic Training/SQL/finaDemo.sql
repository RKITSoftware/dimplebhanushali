use final_demo;

CREATE TABLE `oom01` (
  `m01f01` INT NOT NULL AUTO_INCREMENT COMMENT 'Room ID',
  `m01f02` VARCHAR(15) NULL COMMENT 'Room Name',
  `m01f03` INT NULL COMMENT 'Room Capacity',
  PRIMARY KEY (`m01f01`))
COMMENT = 'RoomTable';


CREATE TABLE `ook01` (
  `k01f01` INT NOT NULL AUTO_INCREMENT COMMENT 'Booking ID',
  `k01f02` VARCHAR(45) NULL COMMENT 'Guest Name',
  `k01f03` DATE NULL COMMENT 'Check-in Date',
  `k01f04` DATE NULL COMMENT 'Check-out Date',
  `k01f05` INT NULL COMMENT 'Room ID',
  PRIMARY KEY (`k01f01`),
    FOREIGN KEY (`k01f05`)
    REFERENCES `oom01` (`m01f01`)
)
COMMENT = 'Book';

INSERT INTO `oom01` (`m01f02`, `m01f03`) VALUES ('RoomC', '20');
INSERT INTO `oom01` (`m01f02`, `m01f03`) VALUES ('RoomD', '5');
INSERT INTO `oom01` (`m01f02`, `m01f03`) VALUES ('RoomE', '25');
INSERT INTO `oom01` (`m01f02`, `m01f03`) VALUES ('RoomF', '10');
INSERT INTO `oom01` (`m01f02`, `m01f03`) VALUES ('RoomG', '15');


INSERT INTO `ook01` (`k01f02`, `k01f03`, `k01f04`, `k01f05`) VALUES ('Dimple', '2024-01-15', '2024-01-17', '1');
INSERT INTO `ook01` (`k01f02`, `k01f03`, `k01f04`, `k01f05`) VALUES ('Pankaj', '2024-01-20', '2024-01-21', '2');
INSERT INTO `ook01` (`k01f02`, `k01f03`, `k01f04`, `k01f05`) VALUES ('Vanshika', '2024-02-01', '2024-02-02', '3');
INSERT INTO `ook01` (`k01f02`, `k01f03`, `k01f04`, `k01f05`) VALUES ('Brijesh', '2024-01-02', '2024-01-03', '2');

-- Read
select 
	m01f01,
    m01f02,
    m01f03
from 
	oom01;

select 
	k01f01,
    k01f02,
    k01f03,
    k01f04,
    k01f05
from 
	ook01;

-- Select Data from ook01 (Booking) with Room Information
SELECT
    k01.k01f01,
    k01.k01f02,
    k01.k01f03,
    k01.k01f04,
    m01.m01f02 AS room_name,
    m01.m01f03 AS room_capacity
FROM 
	ook01 k01
INNER JOIN 
	oom01 m01 
ON 
	k01.k01f05 = m01.m01f01
LIMIT
	2,1;

-- Update Data in oom01 (Room)
UPDATE 
	oom01 
SET 
	m01f03 = 30 
WHERE 
	m01f01 = 1;

-- Update Data in ook01 (Booking)
UPDATE 
	ook01 
SET 
	k01f03 = '2024-01-25' 
WHERE 
	k01f01 = 1;

-- Delete Data from oom01 (Room)
DELETE FROM 
	oom01 
WHERE 
	m01f01 = 1;

-- Delete Data from ook01 (Booking)
DELETE FROM 
	ook01 
WHERE 
	k01f01 = 1;

-- Subquery using Aggregate Function

create view vws_oom01 AS
SELECT
    AVG(avgCapacity) 
FROM (
    -- Subquery: Retrieve average capacity of rooms with capacity after 15
    SELECT
        m01.m01f01 AS room_id,
        m01.m01f03 As avgCapacity
    FROM oom01 m01
    JOIN ook01 k01 ON m01.m01f01 = k01.k01f05
    WHERE m01.m01f03 > '15'
   
) AS subquery;

select * from vws_oom01;

explain analyze
(select * from oom01 where m01f02 = 'RoomA');

CREATE INDEX 
	idx_m01_f02
ON 
	oom01 (m01f02); -- RoomName

explain analyze
(select * from oom01 where m01f02 = 'RoomA');

drop index 
	idx_m01_f02 
on 
	oom01;

-- Grant SELECT , INSERT , UPDATE privilege on the oom01 and ook01 tables to a user
GRANT SELECT, INSERT , UPDATE ON oom01 TO 'user'@'localhost';
GRANT SELECT , INSERT ,UPDATE ON ook01 TO 'user'@'localhost';

-- Revoke DELETE privilege on the oom01 table from a user
REVOKE DELETE ON oom01 FROM 'user'@'localhost';

REVOKE ALL PRIVILEGES ON *.* FROM 'user'@'localhost';

SET SQL_SAFE_UPDATES = 0;

-- Initial Transaction Start
START TRANSACTION;
select * from oom01;
-- Update Data in oom01 (Room)
UPDATE 
	oom01 
SET 
	m01f03 = 40 
WHERE 
	m01f02 = 'RoomC';

-- Savepoint after Room Update
SAVEPOINT after_room_update;

-- Update Data in ook01 (Booking)
UPDATE 
	oom01 
SET 
	m01f03 = 30 
WHERE 
	m01f02 = 'RoomC';

-- Checkpoint: Commit Changes so far
COMMIT;

-- Delete Data from oom01 (Room)
DELETE FROM 
	oom01 
WHERE 
	m01f02 = 'RoomG';

-- Checkpoint: Rollback to the savepoint after Room Update
ROLLBACK TO SAVEPOINT after_room_update;

-- Final Transaction End: Rollback or Commit
COMMIT;



select * from ook01;

SELECT
    k01.k01f02 AS guest_name,
    k01.k01f03 AS checkin_date,
    k01.k01f04 AS checkout_date,
    DATEDIFF(k01.k01f04, k01.k01f03) AS days_stayed,
    k01.k01f05 AS room_id,
    m01.m01f02 AS room_name
FROM 
    ook01 k01
JOIN
    oom01 m01 ON k01.k01f05 = m01.m01f01;  

