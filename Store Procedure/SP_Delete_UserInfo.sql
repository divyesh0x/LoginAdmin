/* ------------------------------------------------------------
   Description:  Delete proc for table 'ClaimStatusHistory'
   ------------------------------------------------------------ */
IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('dbo.DeleteUserInfo') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
    DROP PROCEDURE dbo.DeleteUserInfo
GO

CREATE PROCEDURE dbo.DeleteUserInfo
(
    @Id int
)
AS

BEGIN
    SET NOCOUNT ON
   
    DELETE [UserInfo]
    WHERE [Id] = @Id
   
END