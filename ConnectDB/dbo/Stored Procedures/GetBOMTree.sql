


CREATE PROCEDURE [dbo].[GetBOMTree] 
@product varchar(25)
AS

  SET NOCOUNT ON;

  WITH q
  AS (SELECT
    *
  FROM [dbo].SPMConnectBOM
  WHERE AssyNo = @product

  UNION ALL

  SELECT
    m.*
  FROM [dbo].SPMConnectBOM m
  JOIN q
    ON q.ItemNumber = m.AssyNo)
  SELECT
    q.AssyNo AS parent_id,
    q.ItemNumber AS children,
    q.ItemNumber,
    q.QuantityPerAssembly,
    q.Description,
    q.Manufacturer,
    q.ManufacturerItemNumber
  FROM q

  UNION
  SELECT TOP (1)
    NULL AS parent_id,
    a.ItemNumber AS children,
    a.ItemNumber,
    0 AS QuantityPerAssembly,
    a.Description,
    a.Manufacturer,
    a.ManufacturerItemNumber
  FROM [dbo].UnionInventory a
  WHERE a.ItemNumber = @product