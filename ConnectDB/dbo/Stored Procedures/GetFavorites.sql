





CREATE PROCEDURE GetFavorites
AS
  SET NOCOUNT ON;

  SELECT
    uinv.*,
    fav.UserName
  FROM [dbo].Inventory AS uinv
  INNER JOIN [dbo].[favourite] AS fav
    ON uinv.ItemNumber = fav.Item