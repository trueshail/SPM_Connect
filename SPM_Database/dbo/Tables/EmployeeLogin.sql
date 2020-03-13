CREATE TABLE [dbo].[EmployeeLogin] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [EmpId]       NCHAR (10)     NULL,
    [UserName]    NCHAR (100)    NULL,
    [Password]    NVARCHAR (MAX) NOT NULL,
    [EmpName]     VARCHAR (50)   NULL,
    [EmpLastname] VARCHAR (50)   NULL
);

