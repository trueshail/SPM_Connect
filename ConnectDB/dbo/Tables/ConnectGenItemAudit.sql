CREATE TABLE [dbo].[ConnectGenItemAudit] (
    [ItemID]    VARCHAR (50) NULL,
    [Activity]  VARCHAR (20) NULL,
    [DoneBy]    VARCHAR (50) NULL,
    [Date_Time] DATETIME     DEFAULT (getdate()) NOT NULL
);

