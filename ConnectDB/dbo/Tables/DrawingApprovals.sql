CREATE TABLE [dbo].[DrawingApprovals] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [ItemNo]      VARCHAR (10) NOT NULL,
    [SubmittedOn] DATETIME     DEFAULT (getdate()) NULL,
    [SubmittedBy] VARCHAR (50) NULL,
    [SubmittedTo] INT          CONSTRAINT [DF_DrawingApprovals_SubmittedTo] DEFAULT ((0)) NOT NULL,
    [CheckedOn]   DATETIME     NULL,
    [CheckedBy]   VARCHAR (50) NULL,
    [ApprovedOn]  DATETIME     NULL,
    [ApprovedBy]  VARCHAR (50) NULL,
    [ReleasedOn]  DATETIME     NULL,
    [ReleaseBy]   VARCHAR (50) NULL,
    CONSTRAINT [PK_DrawingApprovals] PRIMARY KEY CLUSTERED ([ItemNo] ASC)
);



