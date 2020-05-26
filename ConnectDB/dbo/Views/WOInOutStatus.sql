





 CREATE VIEW  [dbo].[WOInOutStatus]
 as 
     SELECT 
     a.[WO],      
	ab.Item,
	ab.Description,
	ab.Qty,
	ab.Job,
	IIF(b.[InBuilt] = 1,'Yes','No') AS Inbuilt,
	IIF(b.[Completed] = 1,'Yes','No') as Completed,
	IIF(b.[InBuilt] = 1,b.[TakeOutLocation],a.[CribLocation]) AS Location,
	b.Id
FROM [SPM_Database].[dbo].[WO_Tracking] a
	left JOIN 
[SPM_Database].[dbo].[WOInOutTop] b on b.WO = a.WO
 join 
 [SPM_Database].[dbo].[WorkOrderManagement] ab on ab.WorkOrder = a.WO

