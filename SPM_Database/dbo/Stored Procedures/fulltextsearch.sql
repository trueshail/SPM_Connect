/****** Script for SelectTopNRows command from SSMS  ******/
create procedure fulltextsearch
@filtertext varchar
as

SELECT *
  FROM [dbo].[InventoryBackView]
  where  contains(fullsearch, @filtertext)



