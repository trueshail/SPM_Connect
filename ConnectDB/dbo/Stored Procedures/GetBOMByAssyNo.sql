-- =============================================
-- Author:		Shail Patel
-- Create date: 2020/06/01
-- Description:	Get BOM for Assy
-- =============================================
CREATE PROCEDURE [dbo].[GetBOMByAssyNo]
-- Add the parameters for the stored proce780dure here
@Assyno VARCHAR(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Insert statements for procedure here
	WITH q
	AS
	(SELECT
			a.ItemNumber
		   ,a.QuantityPerAssembly
		   ,a.Description
		   ,a.ItemFamily
		   ,a.Manufacturer
		   ,a.ManufacturerItemNumber
		   ,a.Spare
		   ,a.AssyNo
		   ,a.AssyFamily
		   ,a.AssyDescription
		   ,a.AssyManufacturer
		   ,a.AssyManufacturerItemNumber
		   ,a.AssySpare
		FROM (SELECT
				bm.Item AS ItemNumber
			   ,CONVERT(INT, bm.Qte) AS QuantityPerAssembly
			   ,eb.Des AS Description
			   ,eb.Famille AS ItemFamily
			   ,eb.Des4 AS Manufacturer
			   ,eb.Des2 AS ManufacturerItemNumber
			   ,eb.Spec1 AS Spare
			   ,bm.Produit AS AssyNo
			   ,eb2.Famille AS AssyFamily
			   ,eb2.Des AS AssyDescription
			   ,eb2.Des4 AS AssyManufacturer
			   ,eb2.Des2 AS AssyManufacturerItemNumber
			   ,eb2.Spec1 AS AssySpare
			FROM [SPMDB].[dbo].[Bom] bm
			INNER JOIN [SPMDB].[dbo].[Edb] eb
				ON bm.Item = eb.Item
			INNER JOIN SPMDB.dbo.Edb eb2
				ON bm.Produit = eb2.Item) a
		WHERE a.AssyNo = @Assyno

		UNION ALL

		SELECT
			a.ItemNumber
		   ,a.QuantityPerAssembly
		   ,a.Description
		   ,a.ItemFamily
		   ,a.Manufacturer
		   ,a.ManufacturerItemNumber
		   ,a.Spare
		   ,a.AssyNo
		   ,a.AssyFamily
		   ,a.AssyDescription
		   ,a.AssyManufacturer
		   ,a.AssyManufacturerItemNumber
		   ,a.AssySpare
		FROM (SELECT
				bm.Item AS ItemNumber
			   ,CONVERT(INT, bm.Qte) AS QuantityPerAssembly
			   ,eb.Des AS Description
			   ,eb.Famille AS ItemFamily
			   ,eb.Des4 AS Manufacturer
			   ,eb.Des2 AS ManufacturerItemNumber
			   ,eb.Spec1 AS Spare
			   ,bm.Produit AS AssyNo
			   ,eb2.Famille AS AssyFamily
			   ,eb2.Des AS AssyDescription
			   ,eb2.Des4 AS AssyManufacturer
			   ,eb2.Des2 AS AssyManufacturerItemNumber
			   ,eb2.Spec1 AS AssySpare
			FROM [SPMDB].[dbo].[Bom] bm
			INNER JOIN [SPMDB].[dbo].[Edb] eb
				ON bm.Item = eb.Item
			INNER JOIN SPMDB.dbo.Edb eb2
				ON bm.Produit = eb2.Item) a
		JOIN q
			ON q.ItemNumber = a.AssyNo)
	SELECT
		ItemNumber
	   ,QuantityPerAssembly
	   ,Description
	   ,ItemFamily
	   ,Manufacturer
	   ,ManufacturerItemNumber
	   ,Spare
	   ,AssyNo
	   ,AssyFamily
	   ,AssyDescription
	   ,AssyManufacturer
	   ,AssyManufacturerItemNumber
	   ,AssySpare
	FROM q
END