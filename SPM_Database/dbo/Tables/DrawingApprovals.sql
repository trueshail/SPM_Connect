CREATE TABLE [dbo].[DrawingApprovals] (
    [Id]         INT          IDENTITY (1, 1) NOT NULL,
    [ItemNo]     VARCHAR (10) NULL,
    [CheckedOn]  DATETIME     NULL,
    [CheckedBy]  VARCHAR (50) NULL,
    [ApprovedOn] DATETIME     NULL,
    [ApprovedBy] VARCHAR (50) NULL
);

