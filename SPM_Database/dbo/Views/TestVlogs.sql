

CREATE View [dbo].[TestVlogs]
as


SELECT  a.RlogNo, a.JobNo, a.JobDescription, a.WO, a.AssyNo, a.Description, a.ReleaseType,a.CreatedOn, a.CreatedBy
FROM [SPM_Database].[dbo].[VReleaseLogs] a
INNER JOIN 
  (SELECT WO,
    MIN(RlogNo) as id
  FROM [SPM_Database].[dbo].[VReleaseLogs] 
  GROUP BY WO 
) AS b
  ON a.WO = b.WO 
  AND a.RlogNo = b.id

  UNION ALL
  SELECT RlogNo, WO AS JobNo, JobDescription, WO, AssyNo, Description, ReleaseType,CreatedOn, CreatedBy  FROM [SPM_Database].[dbo].[VReleaseLogs] 
  
