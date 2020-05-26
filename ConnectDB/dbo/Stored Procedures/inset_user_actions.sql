CREATE PROCEDURE [dbo].[inset_user_actions]

    @mytable [UserActionsBulk] READONLY

AS



INSERT INTO UserActions

    (timeStamp,formName,ctrlName,eventName,value,UserName)

SELECT

    timeStamp,formName,ctrlName,eventName,value,UserName

FROM

    @mytable