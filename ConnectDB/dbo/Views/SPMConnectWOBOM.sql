









CREATE VIEW [dbo].[SPMConnectWOBOM]
AS
SELECT
  a.*,
  eb2.Famille AS AssyFamily,
  eb2.Des AS AssyDescription,
  eb2.Des4 AS AssyManufacturer,
  eb2.Des2 AS AssyManufacturerItemNumber,
  eb2.Spec1 AS AssySpare
FROM (SELECT
  bm.Job,
  bm.Woprec,
  bm.Wo,
  bm.Item AS ItemNumber,
  CONVERT(int, bm.Qte_Ass) AS QuantityPerAssembly,
  eb.Des AS Description,
  eb.Famille AS ItemFamily,
  eb.Des4 AS Manufacturer,
  eb.Des2 AS ManufacturerItemNumber,
  eb.Spec1 AS Spare,
  bm.Piece AS AssyNo
FROM [SPMDB].[dbo].[Mrpres] bm
INNER JOIN [SPMDB].[dbo].[Edb] eb
  ON bm.Item = eb.Item
WHERE bm.RML_Active = '1') a
INNER JOIN SPMDB.dbo.Edb eb2
  ON a.AssyNo = eb2.Item

