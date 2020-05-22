





CREATE VIEW [dbo].[VReleaseBOM]
AS


SELECT
  ab.RlogNo,
  ab.JobNo,
  ab.Parent,
  jb.Description AS JobDescription,
  ab.WO,
  ab.AssyNo,
  ab.Description,
  ab.OEM,
  ab.OEMItemNo,
  ab.Family,
  ab.ReleaseType,
  ab.IsRevised,
  ab.ReleaseNotes,
  ab.CreatedBy,
  ab.CreatedOn,
  ab.LastSavedBy,
  ab.LastSaved
FROM (SELECT
  b.RlogNo,
  b.JobNo,
  b.JobNo AS Parent,
  b.WO,
  b.AssyNo,
  a.Des AS Description,
  a.Des4 AS OEM,
  a.Des2 AS OEMItemNo,
  a.Famille AS Family,
  b.ReleaseType,
  '3' as IsRevised,
  b.ReleaseNotes,
  b.CreatedBy,
  b.CreatedOn,
  b.LastSavedBy,
  b.LastSaved
FROM [SPM_Database].[dbo].[WOReleaseLog] b
INNER JOIN [SPMDB].[dbo].[Edb] a
  ON a.Item = b.AssyNo) ab
INNER JOIN [SPM_Database].[dbo].[SPMJobs] jb
  ON jb.Job = ab.JobNo
INNER JOIN (SELECT
  WO,
  MIN(RlogNo) AS id
FROM [SPM_Database].[dbo].[VReleaseLogs]
GROUP BY WO) AS b
  ON ab.WO = b.WO
  AND ab.RlogNo = b.id

UNION ALL

SELECT
  ab.RlogNo,
  ab.JobNo ,
  ab.Parent,
  jb.Description AS JobDescription,
  ab.WO,
  ab.AssyNo,
  ab.Description,
  ab.OEM,
  ab.OEMItemNo,
  ab.Family,
  ab.ReleaseType,
  ab.IsRevised,
  ab.ReleaseNotes,
  ab.CreatedBy,
  ab.CreatedOn,
  ab.LastSavedBy,
  ab.LastSaved
FROM (SELECT
  b.RlogNo,
  b.JobNo,
  b.WO AS Parent,
  b.WO,
  b.AssyNo,
  a.Des AS Description,
  a.Des4 AS OEM,
  a.Des2 AS OEMItemNo,
  a.Famille AS Family,
  b.ReleaseType,
  '3' as IsRevised,
  b.ReleaseNotes,
  b.CreatedBy,
  b.CreatedOn,
  b.LastSavedBy,
  b.LastSaved
FROM [SPM_Database].[dbo].[WOReleaseLog] b
INNER JOIN [SPMDB].[dbo].[Edb] a
  ON a.Item = b.AssyNo) ab
INNER JOIN [SPM_Database].[dbo].[SPMJobs] jb
  ON jb.Job = ab.JobNo

UNION ALL

SELECT
  bm.RlogNo,
  bm.JobNo AS JobNo,
  bm.RlogNo AS Parent,
  NULL AS JobDescription,
  bm.WO,
  bm.ItemId AS AssyNo,
  eb.Des AS Description,
  eb.Des4 AS OEM,
  eb.Des2 AS OEMItemNo,
  eb.Famille AS Family,
  CONVERT(varchar, bm.ItemQty) AS ReleaseType,
  bm.IsRevised,
  bm.ItemNotes AS ReleaseNotes,
  bm.ItemCreatedBy AS CreatedBy,
  bm.ItemCreatedOn AS CreatedOn,
  bm.ItemLastSavedBy AS LastSavedBy,
  bm.ItemLastSaved AS LastSaved
FROM [SPM_Database].[dbo].[WOReleaseDetails] bm
INNER JOIN [SPMDB].[dbo].[Edb] eb
  ON bm.ItemId = eb.Item


