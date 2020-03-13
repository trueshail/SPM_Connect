

/****** Script for SelectTopNRows command from SSMS  ******/
CREATE View [dbo].[VendorsShipTo]
as

SELECT  [Code]
      ,[Name]
      ,[Address1]
	  ,[Address2]
      ,[City]
      ,[Province]
      ,[Country]
      ,[ZipCode]
      ,[Phone]
      ,[Fax]
      ,[SalesContact]
      ,[PaymentTermCode]
      ,[CurrencyCode]
      ,[GlAccountCode]
      ,[Export]
      ,[TaxGroupHeaderCode]
      ,[CarrierCode]
      ,[TollFree]
      ,[ShortName]
      ,[Email]
      ,[VendorGroupCode]
      ,[FullSearch]
  FROM [SPMDB].[dbo].[vcVendor]
