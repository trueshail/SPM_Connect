
CREATE view [dbo].[workordertemp]
as

SELECT       bm.ParentWorkOrderId 
		, bm.JobId 
		, eb.Job
		, eb.Code_Ope 
		, eb.Wo 
		,eb.Code_Id
		, eb.Des_Ope 
		,eb.Item 
		, bm.ChildWorkOrderId 
FROM  [SPMDB].[dbo].dimWorkOrderTreeView bm 
	INNER JOIN  [SPMDB].[dbo].Wodet eb 
		ON bm.ChildWorkOrderId = eb.Wo




