CREATE TABLE [dbo].[WOInOut] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [WO]                 INT           NOT NULL,
    [Emp_idCheckOut]     INT           CONSTRAINT [DF_WOInOut_Emp_idCheckOut] DEFAULT ((-1)) NOT NULL,
    [Emp_idCheckIn]      INT           CONSTRAINT [DF_WOInOut_Emp_idCheckIn] DEFAULT ((-1)) NOT NULL,
    [PunchIn]            DATETIME      NULL,
    [PunchOut]           DATETIME      NULL,
    [TimeElapsed]        DATETIME      NULL,
    [CheckOutApprovedBy] INT           CONSTRAINT [DF_WOInOut_CheckOutApprovedBy] DEFAULT ((-1)) NOT NULL,
    [CheckInApprovedBy]  INT           CONSTRAINT [DF_WOInOut_CheckInApprovedBy] DEFAULT ((-1)) NOT NULL,
    [InBuilt]            INT           CONSTRAINT [DF_WOInOut_InBuilt] DEFAULT ((0)) NOT NULL,
    [Completed]          INT           CONSTRAINT [DF_WOInOut_Completed] DEFAULT ((0)) NOT NULL,
    [TakeOutLocation]    VARCHAR (150) NULL,
    CONSTRAINT [PK_WOInOut] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_WO] FOREIGN KEY ([WO]) REFERENCES [dbo].[WO_Tracking] ([WO])
);

