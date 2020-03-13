CREATE TABLE [dbo].[PurchaseReq] (
    [ID]            INT            IDENTITY (1, 1) NOT NULL,
    [ReqNumber]     INT            NOT NULL,
    [OrderId]       INT            NULL,
    [Item]          NCHAR (10)     NULL,
    [Qty]           INT            NULL,
    [Description]   NVARCHAR (MAX) NULL,
    [Manufacturer]  NVARCHAR (MAX) NULL,
    [OEMItemNumber] NVARCHAR (MAX) NULL,
    [Price]         MONEY          NULL,
    [Notes]         NVARCHAR (MAX) NULL,
    CONSTRAINT [PK__Purchase__3214EC27C1B1F482] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PurchaseReq_PurchaseReq] FOREIGN KEY ([ReqNumber]) REFERENCES [dbo].[PurchaseReqBase] ([ReqNumber])
);

