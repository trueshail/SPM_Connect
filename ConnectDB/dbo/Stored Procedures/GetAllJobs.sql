-- =============================================
-- Author:  	Shail Patel
-- Create date: 2020/05/29
-- Description:	Get All SPM Jobs
-- =============================================
CREATE PROCEDURE [dbo].[GetAllJobs]
-- Add the parameters for the stored procedure here
AS
BEGIN
  -- SET NOCOUNT ON added to prevent extra result sets from
  -- interfering with SELECT statements.
  SET NOCOUNT ON;

  -- Insert statements for procedure here

  SELECT
    [Job],
    [BomVersionId] AS EstId,
    [EstimateDocumentLink] AS EstDoc,
    LEFT([Product], 6) AS BOMItem,
    [Description1] AS Description,
    [CustomerOrder] AS SalesOrder,
    [CustomerName],
    [PlannedQuantity] AS Qty,
    [TendersOfContract] AS Note,
    CONCAT(Job, ' ', EstimateDocumentLink, ' ', Product, ' ', Description1, ' ', CustomerOrder, ' ', CustomerName) AS FullSearch,
    [CustomerID]
  FROM [SPMDB].[dbo].[vgJcoJob]
  WHERE LEN(Job) = 5
  AND ISNUMERIC(SUBSTRING(Job, 0, 5)) = 1
  AND JobStatusCode != 'Canceled'
  --AND ISNULL(BomVersionId ,'') <> ''
END