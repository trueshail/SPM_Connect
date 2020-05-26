


/****** Script for SelectTopNRows command from SSMS  ******/
CREATE view  [dbo].[Quotes]
as

SELECT	[Quote]
      ,[Quote_Date]
      ,[Company_Name]
      ,[Title]
      ,[Category]
	  ,[DateCreated]
	  ,CONCAT([Quote], ' ' ,[Quote_Date], ' ',[Company_Name], ' ' ,[Title], ' ' ,[Category])  AS FullSearch     
  FROM [SPM_Database].[dbo].[Opportunities]
