-- =============================================
-- Author:		Shail Patel
-- Create date: 2020/05/29
-- Description:	Get Estimate By Id
-- =============================================
CREATE PROCEDURE [dbo].[GetEstimateBOMById] 
	-- Add the parameters for the stored procedure here
	@estid INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT
	a.ItemNumber
   ,a.QuantityPerAssembly
   ,a.Description
   ,a.ItemFamily
   ,a.Manufacturer
   ,a.ManufacturerItemNumber
   ,a.Spare
   ,a.AssyNo
   ,a.estid
   ,eb2.Famille AS AssyFamily
   ,eb2.Des AS AssyDescription
   ,eb2.Des4 AS AssyManufacturer
   ,eb2.Des2 AS AssyManufacturerItemNumber
   ,eb2.Spec1 AS AssySpare
FROM (SELECT
		bm.Item AS ItemNumber
	   ,CONVERT(INT, bm.Qte) AS QuantityPerAssembly
	   ,eb.Des AS Description
	   ,eb.Famille AS ItemFamily
	   ,eb.Des4 AS Manufacturer
	   ,eb.Des2 AS ManufacturerItemNumber
	   ,eb.Spec1 AS Spare
	   ,bm.Produit AS AssyNo
	   ,bm.BomVersionId AS estid
	FROM [SPMDB].[dbo].[tcBomItemVersion] bm
	INNER JOIN [SPMDB].[dbo].[Edb] eb
		ON bm.Item = eb.Item) a
INNER JOIN SPMDB.dbo.Edb eb2
	ON a.AssyNo = eb2.Item

WHERE a.estid = @estid

END