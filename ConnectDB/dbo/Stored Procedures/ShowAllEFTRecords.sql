
CREATE Procedure  [dbo].[ShowAllEFTRecords]
as
select *  FROM [SPM_Database].[dbo].[EFTHome]
ORDER BY [PaymentDate] DESC
