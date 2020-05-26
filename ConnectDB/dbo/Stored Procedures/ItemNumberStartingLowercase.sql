create procedure ItemNumberStartingLowercase
as



SELECT *
FROM [SPM_Database].[dbo].[Inventory] 
WHERE ItemNumber 
LIKE '%[abcdefghijklmnopqrstuvwxyz]%'
collate Latin1_General_CS_AS