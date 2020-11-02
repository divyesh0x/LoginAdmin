/* ------------------------------------------------------------
   Description:  Update proc for table 'UserInfo'
   ------------------------------------------------------------ */
IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('dbo.UpdateUserInfo') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
    DROP PROCEDURE dbo.UpdateUserInfo
GO

CREATE PROCEDURE dbo.UpdateUserInfo
(
	@Id int,
    @FirstName varchar(20) ,
    @LastName varchar(20) ,
    @Address varchar(20) ,
    @City varchar(20) ,
    @Province varchar(20) ,
    @Country varchar(20) ,
    @PhoneNumber varchar(20) ,
    @Email varchar(20) ,
    @Password varchar(20) ,
    @isAdmin int
)
AS

BEGIN
    SET NOCOUNT ON
   
    UPDATE [UserInfo]
    SET
		[Id] = @Id,
        [FirstName] = @FirstName,
		[LastName] = @LastName,
		[Address] = @Address,
		[City] = @City,
		[Province] = @Province,
		[Country] = @Country,
		[PhoneNumber] = @PhoneNumber,
		[Email] = @Email,
		[Password] = @Password,
		[isAdmin] = @isAdmin
		
    WHERE [Id] = @id
   
   
END

