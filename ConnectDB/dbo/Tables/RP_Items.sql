CREATE TABLE [dbo].[RP_Items] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [RelNo]       INT           NOT NULL,
    [AssyNo]      VARCHAR (50)  NOT NULL,
    [ItemId]      VARCHAR (50)  NOT NULL,
    [ItemNotes]   VARCHAR (250) NULL,
    [ItemQty]     INT           NOT NULL,
    [IsRevised]   INT           CONSTRAINT [DF_RP_Items_IsRevised] DEFAULT ((0)) NOT NULL,
    [DateCreated] DATETIME      DEFAULT (getdate()) NULL,
    [Path]        VARCHAR (500) NULL,
    CONSTRAINT [PK_RP_Items] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RP_Items_RP_Base] FOREIGN KEY ([RelNo]) REFERENCES [dbo].[RP_Base] ([RelNo])
);

