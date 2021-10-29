/*
Part 1: List the columns and their data types in the Jobs table.
id INT
name VARCHAR(200)
employerId INT

Part 2: List the names of the employers in St. Louis City.
SELECT Name FROM employers
WHERE Location = "STL";

Part 3:
SELECT name, description FROM skills
LEFT JOIN jobskills ON jobskills.SkillId = skills.Id
WHERE jobskills.SkillId IS NOT NULL
ORDER BY name ASC;


*/