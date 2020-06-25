SELECT SUM(CONVERT(BIGINT,Population)) from country

SELECT DISTINCT Continent from country

SELECT SUM(GNP) from  country 
WHERE continent = 'Africa'

SELECT COUNT(*) from country 
WHERE surfaceArea > 1000000


SELECT Population,Name from country
WHERE Name IN ( 'France' , 'Germany' ,'Spain')


SELECT Population,Name from country
WHERE Population BETWEEN (SELECT Population from country WHere Name = 'Canada')  
AND (SELECT Population from country WHere Name = 'Egypt') 
AND Name <> 'Canada' AND  Name <> 'Egypt'


SELECT count(*) as CountNUM,Continent from country 
GROUP BY continent




SELECT COUNT(*) , Continent from country 
Where Population > 10000000
GROUP BY continent 


SELECT SUM(CONVERT(BIGINT,Population)) as Totalpopulation, Continent from country 
GROUP BY continent 
Having SUM(CONVERT(BIGINT,Population)) > 1000000


SELECT SUM(CONVERT(BIGINT,Population)) as population, Continent from country 
where Population > 100000000
GROUP BY continent 

SELECT TOP 3 surfaceArea, Name from country 
order by  surfaceArea  DESC

SELECT Name from country 
Where Population = (SELECT MAX(Population) from country)



SELECT Name from country 
Where GNP = (
SELECT MAX(GNP) from country 
Where Continent = 'Europe'
)
AND Continent = 'EUrope'



SELECT TOP(1) SUM(GNP) as s,continent from country 
GROUP BY Continent 
order by s DESC


SELECT Name from country 
where Continent = (
SELECT Continent from country
where name = 'China')


