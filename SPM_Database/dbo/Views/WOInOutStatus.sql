



 CREATE VIEW  [dbo].[WOInOutStatus]
 as 
  SELECT 
    b.[WO],      
	a.Item,
	a.Description,
	a.Qty,
	a.Job,
	IIF(b.[InBuilt] = 1,'Yes','No') AS Inbuilt,
	IIF(b.Completed = 1,'Yes','No') as Completed,
	Id
FROM [SPM_Database].[dbo].[WOInOut] b
	INNER JOIN 
	[SPM_Database].[dbo].[WorkOrderManagement] a on a.WorkOrder = b.WO
