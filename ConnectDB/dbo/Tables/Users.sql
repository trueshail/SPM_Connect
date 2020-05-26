CREATE TABLE [dbo].[Users] (
    [Emp_Id]               INT            NOT NULL,
    [UserName]             NVARCHAR (50)  NOT NULL,
    [Department]           NVARCHAR (50)  NOT NULL,
    [Name]                 NVARCHAR (50)  NULL,
    [ActiveBlockNumber]    NVARCHAR (50)  NULL,
    [Admin]                BIT            CONSTRAINT [DF_Users_Admin] DEFAULT ((0)) NOT NULL,
    [Developer]            BIT            CONSTRAINT [DF_Users_Developer] DEFAULT ((0)) NOT NULL,
    [Management]           BIT            CONSTRAINT [DF_Users_Management] DEFAULT ((0)) NOT NULL,
    [Quote]                BIT            CONSTRAINT [DF_Users_Quote] DEFAULT ((0)) NOT NULL,
    [PurchaseReq]          BIT            CONSTRAINT [DF_Users_PurchaseReq] DEFAULT ((0)) NOT NULL,
    [PurchaseReqApproval]  BIT            CONSTRAINT [DF_Users_PurchaseReqApproval] DEFAULT ((0)) NOT NULL,
    [PurchaseReqApproval2] BIT            CONSTRAINT [DF_Users_PurchaseReqApproval2] DEFAULT ((0)) NOT NULL,
    [PurchaseReqBuyer]     BIT            CONSTRAINT [DF_Users_PurchaseReqBuyer] DEFAULT ((0)) NOT NULL,
    [PriceRight]           BIT            CONSTRAINT [DF_Users_PriceRight] DEFAULT ((0)) NOT NULL,
    [Shipping]             BIT            CONSTRAINT [DF_Users_Shipping] DEFAULT ((0)) NOT NULL,
    [WOScan]               BIT            CONSTRAINT [DF_Users_ScanWo] DEFAULT ((0)) NOT NULL,
    [CribCheckout]         BIT            CONSTRAINT [DF_Users_CribCheckout] DEFAULT ((0)) NOT NULL,
    [CribShort]            BIT            CONSTRAINT [DF_Users_CribShort] DEFAULT ((0)) NOT NULL,
    [ECR]                  BIT            CONSTRAINT [DF_Users_PurchaseReq1] DEFAULT ((0)) NOT NULL,
    [ECRApproval]          BIT            CONSTRAINT [DF_Users_PurchaseReqApproval1] DEFAULT ((0)) NOT NULL,
    [ECRApproval2]         BIT            CONSTRAINT [DF_Users_PurchaseReqApproval21] DEFAULT ((0)) NOT NULL,
    [ECRHandler]           BIT            CONSTRAINT [DF_Users_PurchaseReqBuyer1] DEFAULT ((0)) NOT NULL,
    [ECRSup]               INT            CONSTRAINT [DF_Users_ECRSup] DEFAULT ((0)) NOT NULL,
    [ItemDependencies]     BIT            CONSTRAINT [DF_Users_ItmDependencies] DEFAULT ((0)) NOT NULL,
    [WORelease]            BIT            CONSTRAINT [DF_Users_ReleaseLog] DEFAULT ((0)) NOT NULL,
    [ShipSupervisor]       BIT            CONSTRAINT [DF_Users_ShippingManager] DEFAULT ((0)) NOT NULL,
    [ShipSup]              INT            CONSTRAINT [DF_Users_ShipSup] DEFAULT ((0)) NOT NULL,
    [ShippingManager]      BIT            CONSTRAINT [DF_Users_Shipper] DEFAULT ((0)) NOT NULL,
    [CheckDrawing]         BIT            CONSTRAINT [DF_Users_CheckDrawing] DEFAULT ((0)) NOT NULL,
    [ApproveDrawing]       BIT            CONSTRAINT [DF_Users_ApproveDrawing] DEFAULT ((0)) NOT NULL,
    [ReleasePackage]       BIT            CONSTRAINT [DF_Users_ReleasePackage] DEFAULT ((0)) NOT NULL,
    [Supervisor]           INT            NULL,
    [Email]                VARCHAR (100)  NULL,
    [SharesFolder]         NVARCHAR (250) NULL,
    [ReadWhatsNew]         BIT            CONSTRAINT [DF_Users_ReadWhatsNew] DEFAULT ((0)) NULL,
    [id]                   INT            IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_Users_1] PRIMARY KEY CLUSTERED ([id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UserName]
    ON [dbo].[Users]([UserName] ASC);

