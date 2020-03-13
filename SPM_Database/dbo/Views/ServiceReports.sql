

CREATE VIEW [dbo].[ServiceReports]

as 
  SELECT Title AS ReportId
		 ,ProjectNo as [Job No]
		 ,RefJob
		 ,ProjectManager
		 ,Customer
		 ,Planloc as [Plant Location]
		 ,ContactName
		 ,Authorizedby
		 ,Equipment
		 ,Techname as [Tech Name]
		 ,DateCreated
		 ,concat(Title, ' ', ProjectNo, ' ', Customer, ' ', Planloc, ' ', Equipment) AS FullSearch		 	
		 FROM [SPM_Database].[dbo].[spservicereports]
