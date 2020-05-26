CREATE TABLE [dbo].[EFTEmailTracker] (
    [ID]        INT      NOT NULL,
    [PaymentNo] INT      NOT NULL,
    [EmailSent] INT      CONSTRAINT [DF_EFTEmailTracker_EmailSent] DEFAULT ((0)) NOT NULL,
    [DateSent]  DATETIME NULL,
    CONSTRAINT [PK_EFTEmailTracker] PRIMARY KEY CLUSTERED ([ID] ASC)
);

