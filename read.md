GB7

????????????? ??
1) 
CREATE TABLE [dbo].[Department]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [DepartName] VARCHAR(20) NULL
)

CREATE TABLE [dbo].[Worker]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(20) NULL, 
    [SurName] VARCHAR(20) NULL, 
    [SecondName] VARCHAR(20) NULL, 
    [DepartName] VARCHAR(20) NULL
)
2) ?????????? ??????? Department
INSERT INTO Department (Id, DepartName) VALUES (1, 'Department1'), (1, 'Department2')
3) ?????????? ??????? Worker
INSERT INTO Worker (Id, Name, SurName, SecondName, DepartName) VALUES (1, 'Name1', 'SurName1', 'SecondName1', 'DepartName1'), (2, 'Name2', 'SurName2', 'SecondName2', 'DepartName2')