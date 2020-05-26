CREATE TABLE [dbo].[DrawingApprovals] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [ItemNo]      VARCHAR (10) NULL,
    [SubmittedOn] DATETIME     NULL,
    [SubmittedBy] VARCHAR (50) NULL,
    [SubmittedTo] INT          CONSTRAINT [DF_DrawingApprovals_SubmittedTo] DEFAULT ((0)) NOT NULL,
    [CheckedOn]   DATETIME     NULL,
    [CheckedBy]   VARCHAR (50) NULL,
    [ApprovedOn]  DATETIME     NULL,
    [ApprovedBy]  VARCHAR (50) NULL,
    [ReleasedOn]  DATETIME     NULL,
    [ReleaseBy]   VARCHAR (50) NULL
);

