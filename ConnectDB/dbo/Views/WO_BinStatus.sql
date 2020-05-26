


CREATE VIEW [dbo].[WO_BinStatus]
as
  SELECT 
    b.[WO],
	b.Id,	
	c.Name as EmpCheckOutby,
	b.PunchIn as CheckOutTime,
	cd.Name as CheckOutApprovedby,
	a.Name as EmpCheckInby,
	b.PunchOut as CheckInTime,
	ad.Name as CheckInApprovedby,
	IIF(b.[InBuilt] = 1,'Yes','No') AS Inbuilt,
	IIF(b.Completed = 1,'Yes','No') as Completed
FROM [SPM_Database].[dbo].[WOInOut] b
	left join 
	[SPM_Database].[dbo].[Users]	c	on c.Emp_Id = b.Emp_idCheckOut 
	left join 
	[SPM_Database].[dbo].[Users]	cd	on cd.Emp_Id = b.CheckOutApprovedBy
	left join 
	[SPM_Database].[dbo].[Users]	a	on a.Emp_Id = b.Emp_idCheckIn 
	left join 
	[SPM_Database].[dbo].[Users]	ad	on ad.Emp_Id = b.CheckInApprovedBy


