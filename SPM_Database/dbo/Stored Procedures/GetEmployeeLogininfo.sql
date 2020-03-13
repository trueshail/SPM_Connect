
CREATE procedure [dbo].[GetEmployeeLogininfo]

@username varchar(30),
@password varchar(150)


as

SELECT COUNT(*) FROM [SPM_Database].[dbo].[EmployeeLogin] 
WHERE UserName = @username AND Password = @password






