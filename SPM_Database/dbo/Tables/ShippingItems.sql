CREATE TABLE [dbo].[ShippingItems] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [OrderId]     INT           NULL,
    [Item]        VARCHAR (10)  NULL,
    [Description] VARCHAR (MAX) NULL,
    [Origin]      VARCHAR (50)  NULL,
    [TarriffCode] VARCHAR (150) NULL,
    [Qty]         INT           NULL,
    [Cost]        MONEY         NULL,
    [Total]       MONEY         NULL,
    [InvoiceNo]   VARCHAR (50)  NULL,
    CONSTRAINT [PK_ShippingItems] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ShippingItems_ShippingBase] FOREIGN KEY ([InvoiceNo]) REFERENCES [dbo].[ShippingBase] ([InvoiceNo])
);

