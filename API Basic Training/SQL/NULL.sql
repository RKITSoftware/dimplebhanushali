use demo;
SELECT * FROM col01
WHERE l01f02 IS NULL;

SELECT IFNULL(l01f02, 'AVPTI') AS updated_value
FROM col01;
