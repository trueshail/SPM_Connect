CREATE TABLE [dbo].[Item_CostRollup] (
    [ItemId]      NVARCHAR (50)  NULL,
    [Cost]        MONEY          NULL,
    [SalesPrice]  MONEY          NULL,
    [Qty]         NCHAR (10)     NULL,
    [Notes]       NVARCHAR (MAX) NULL,
    [Date]        DATETIME       NULL,
    [LastSavedBy] NVARCHAR (50)  NULL
);

