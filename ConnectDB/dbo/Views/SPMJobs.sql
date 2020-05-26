







CREATE view [dbo].[SPMJobs]
as
SELECT [Job]
,[BomVersionId] as EstId
,[EstimateDocumentLink] as EstDoc
,LEFT([Product],6) AS BOMItem
,[Description1] AS Description
,[CustomerOrder] AS SalesOrder
,[CustomerName]
,[PlannedQuantity] AS Qty
,[TendersOfContract] AS Note
,CONCAT(Job,' ' ,EstimateDocumentLink, ' ', Product,' ',Description1,' ',CustomerOrder,' ',CustomerName) AS FullSearch
,[CustomerID]
FROM [SPMDB].[dbo].[vgJcoJob]
WHERE LEN(Job)=5
and isnumeric(substring(Job, 0, 5)) = 1
and JobStatusCode != 'Canceled'
