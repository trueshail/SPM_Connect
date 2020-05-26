CREATE PROCEDURE [dbo].[findInventory] @itemnumber varchar(30)

AS

  SET NOCOUNT ON;

  SELECT
    *
  FROM Inventory
  WHERE ItemNumber = @itemnumber