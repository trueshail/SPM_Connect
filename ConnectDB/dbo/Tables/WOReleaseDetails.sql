CREATE TABLE [dbo].[WOReleaseDetails] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [RlogNo]          VARCHAR (50)  NOT NULL,
    [ReleaseType]     VARCHAR (50)  NULL,
    [JobNo]           VARCHAR (50)  NULL,
    [WO]              INT           NOT NULL,
    [AssyNo]          VARCHAR (50)  NOT NULL,
    [ItemId]          VARCHAR (50)  NOT NULL,
    [ItemNotes]       VARCHAR (250) NULL,
    [ItemQty]         INT           CONSTRAINT [DF_WOReleaseDetails_ItemQty] DEFAULT ((1)) NOT NULL,
    [IsRevised]       INT           CONSTRAINT [DF_WOReleaseDetails_IsRevised] DEFAULT ((0)) NOT NULL,
    [ItemCreatedOn]   DATETIME      NULL,
    [ItemCreatedBy]   VARCHAR (50)  NULL,
    [ItemLastSaved]   DATETIME      NULL,
    [ItemLastSavedBy] VARCHAR (50)  NULL,
    [Order]           INT           CONSTRAINT [DF_WOReleaseDetails_Order] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_WOReleaseDetails] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_WOReleaseDetails_WOReleaseLog] FOREIGN KEY ([RlogNo]) REFERENCES [dbo].[WOReleaseLog] ([RlogNo])
);

