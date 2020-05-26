




CREATE VIEW [dbo].[SPMConnectFavorites]
AS
SELECT       uinv.*,
			 fav.UserName                   
FROM         [dbo].[UnionInventory] as uinv INNER JOIN
             [dbo].[favourite] as fav ON uinv.ItemNumber =  fav.Item
						 
