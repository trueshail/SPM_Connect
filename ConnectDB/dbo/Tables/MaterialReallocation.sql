CREATE TABLE [dbo].[MaterialReallocation] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [InvoiceNo]    VARCHAR (50)  NOT NULL,
    [DateCreated]  DATETIME      NULL,
    [CreatedBy]    VARCHAR (50)  NULL,
    [EmployeeId]   INT           NULL,
    [EmployeeName] VARCHAR (50)  NULL,
    [ItemId]       VARCHAR (20)  NULL,
    [Description]  VARCHAR (150) NULL,
    [OEM]          VARCHAR (100) NULL,
    [OEMItem]      VARCHAR (100) NULL,
    [Qty]          INT           NULL,
    [JobReq]       VARCHAR (50)  NULL,
    [WOReq]        VARCHAR (50)  NULL,
    [JobTaken]     VARCHAR (50)  NULL,
    [WOTaken]      VARCHAR (50)  NULL,
    [ApprovedId]   INT           NULL,
    [ApprovedName] VARCHAR (50)  NULL,
    [LastSavedBy]  VARCHAR (50)  NULL,
    [LastSavedOn]  DATETIME      NULL,
    [Notes]        VARCHAR (MAX) NULL,
    CONSTRAINT [PK_MaterialReallocation] PRIMARY KEY CLUSTERED ([InvoiceNo] ASC)
);

