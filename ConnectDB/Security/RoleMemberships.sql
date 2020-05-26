ALTER ROLE [db_owner] ADD MEMBER [SPM_Agent];


GO
ALTER ROLE [db_owner] ADD MEMBER [shail];


GO
ALTER ROLE [db_accessadmin] ADD MEMBER [shail];


GO
ALTER ROLE [db_securityadmin] ADD MEMBER [shail];


GO
ALTER ROLE [db_ddladmin] ADD MEMBER [shail];


GO
ALTER ROLE [db_backupoperator] ADD MEMBER [shail];


GO
ALTER ROLE [db_datareader] ADD MEMBER [SPM_Agent];


GO
ALTER ROLE [db_datareader] ADD MEMBER [shail];


GO
ALTER ROLE [db_datawriter] ADD MEMBER [SPM_Agent];


GO
ALTER ROLE [db_datawriter] ADD MEMBER [shail];


GO
ALTER ROLE [db_denydatareader] ADD MEMBER [shail];


GO
ALTER ROLE [db_denydatawriter] ADD MEMBER [shail];

