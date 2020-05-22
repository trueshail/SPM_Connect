

CREATE VIEW [dbo].[WorkOrderManagement]
AS
SELECT [Job]
      ,[WorkOrder]
      , [Product] AS AssyNo
      ,[Item]
      ,[Description1] AS Description
      ,[PlannedQuantity] AS Qty
      ,CONCAT(Job, ' ' ,Item, ' ',WorkOrder, ' ' ,Description1)  AS FullSearch
  FROM [SPMDB].[dbo].[vgJcoWorkOrder]
  WHERE LEN(Job)= 5 and isnumeric(substring(Job, 1, 5)) = 1
