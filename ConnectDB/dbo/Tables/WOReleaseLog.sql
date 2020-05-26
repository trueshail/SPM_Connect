CREATE TABLE [dbo].[WOReleaseLog] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [RlogNo]       VARCHAR (50)  NOT NULL,
    [JobNo]        VARCHAR (50)  NOT NULL,
    [WO]           INT           NOT NULL,
    [AssyNo]       VARCHAR (50)  NULL,
    [ReleaseType]  VARCHAR (50)  NOT NULL,
    [ReleaseNotes] VARCHAR (MAX) NULL,
    [CreatedBy]    VARCHAR (50)  NULL,
    [CreatedOn]    DATETIME      NULL,
    [LastSavedBy]  VARCHAR (50)  NULL,
    [LastSaved]    DATETIME      NULL,
    [NxtReleaseNo] INT           NOT NULL,
    CONSTRAINT [PK_WOReleaseLog] PRIMARY KEY CLUSTERED ([RlogNo] ASC),
    CONSTRAINT [FK_WOReleaseLog_WOReleaseLog] FOREIGN KEY ([RlogNo]) REFERENCES [dbo].[WOReleaseLog] ([RlogNo])
);

