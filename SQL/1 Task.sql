SELECT * 
FROM Employee 
WHERE SALARY = (SELECT MAX(SALARY) FROM Employee);

WITH RECURSIVE EmployeeHierarchy AS (
    SELECT ID, CHIEF_ID, 1 AS ChainLength
    FROM EMPLOYEE
    WHERE CHIEF_ID IS NULL
    UNION ALL
    SELECT E.ID, E.CHIEF_ID, EH.ChainLength + 1
    FROM EMPLOYEE E
    JOIN EmployeeHierarchy EH ON E.CHIEF_ID = EH.ID
)
SELECT MAX(ChainLength) AS MaxChainLength
FROM EmployeeHierarchy;

SELECT 
		d.NAME AS Department_Name, 
		SUM(e.SALARY) AS Total_Salary 
FROM Employee e 
JOIN Departament d ON e.DEPARTAMENT_ID = d.ID 
GROUP BY d.NAME 
ORDER BY Total_Salary DESC LIMIT 1;

SELECT * FROM Employee WHERE NAME REGEXP '^Р.*н$';