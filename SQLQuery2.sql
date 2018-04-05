CREATE TABLE [dbo].[Employee_Skills]
(
	[Employee_Id] INT NOT NULL, 
    [Skill_Id] INT NOT NULL, 
	primary key (Employee_Id, Skill_Id),
    CONSTRAINT [FK_Employee_Skills_ToEmployees] FOREIGN KEY ([Employee_Id]) REFERENCES [Employees]([Id]),
	CONSTRAINT [FK_Employee_Skills_ToSkills] FOREIGN KEY ([Skill_Id]) REFERENCES [Skills]([Id]) 

)
