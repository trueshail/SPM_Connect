CREATE TABLE [dbo].[RP_Remarks] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [RelNo]       INT           NOT NULL,
    [CommentBy]   VARCHAR (50)  NULL,
    [Comment]     VARCHAR (MAX) NULL,
    [DateCreated] DATETIME      DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_RP_Remarks] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RP_Remarks_RP_Base] FOREIGN KEY ([RelNo]) REFERENCES [dbo].[RP_Base] ([RelNo])
);

