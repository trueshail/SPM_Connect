



CREATE VIEW [dbo].[VReleaseLogs]
AS
Select
	ab.RlogNo,
	ab.JobNo,
	jb.Description as JobDescription,
	ab.WO,
	ab.AssyNo,
	ab.Description,
	ab.ReleaseType,
	ab.CreatedBy,
	ab.CreatedOn,
	CONCAT(ab.JobNo, ' ' ,jb.Description, ' ',ab.WO, ' ' ,ab.AssyNo, ' ', ab.Description, ' ' ,ab.ReleaseType)  AS FullSearch from
(SELECT
	b.RlogNo,
	b.JobNo,
	b.WO,
	b.AssyNo,
	a.Description1 as Description,
	b.ReleaseType,
	b.CreatedBy,
	b.CreatedOn
	FROM [SPM_Database].[dbo].[WOReleaseLog] b
	inner join [SPMDB].[dbo].[vgJcoWorkOrder] a on a.WorkOrder = b.WO)ab
	inner join [SPM_Database].[dbo].[SPMJobs] jb on jb.Job = ab.JobNo
