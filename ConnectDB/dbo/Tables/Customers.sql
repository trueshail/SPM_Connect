CREATE TABLE [dbo].[Customers] (
    [CustomerId] INT           NOT NULL,
    [Name]       VARCHAR (64)  NULL,
    [ShortName]  VARCHAR (10)  NULL,
    [Alias]      NVARCHAR (50) NULL,
    [id]         INT           IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([id] ASC)
);

