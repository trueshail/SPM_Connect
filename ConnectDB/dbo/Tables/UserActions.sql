CREATE TABLE [dbo].[UserActions] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [timeStamp] DATETIME      NULL,
    [formName]  VARCHAR (MAX) NULL,
    [ctrlName]  VARCHAR (MAX) NULL,
    [eventName] VARCHAR (MAX) NULL,
    [value]     VARCHAR (MAX) NULL,
    [UserName]  NVARCHAR (50) NULL
);

