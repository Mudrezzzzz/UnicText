# В корневой папке лежит решение на задание на поиск уникальных слов в текстовом файле

И ответ на решение задачи №1
SELECT *
FROM Employee
WHERE SALARY = (SELECT MAX(SALARY) FROM Employee);

WITH RECURSIVE ManagerChain AS (
  SELECT ID, CHIEF_ID, 1 AS depth
  FROM Employee
  WHERE CHIEF_ID IS NULL
  UNION ALL
  SELECT e.ID, e.CHIEF_ID, mc.depth + 1
  FROM Employee e
  JOIN ManagerChain mc ON e.CHIEF_ID = mc.ID
)
SELECT MAX(depth) AS Max_Chain_Length
FROM ManagerChain;

SELECT d.NAME AS Department_Name, SUM(e.SALARY) AS Total_Salary
FROM Employee e
JOIN Departament d ON e.DEPARTAMENT_ID = d.ID
GROUP BY d.NAME
ORDER BY Total_Salary DESC
LIMIT 1;

SELECT *
FROM Employee
WHERE NAME REGEXP '^Р.*н$';


