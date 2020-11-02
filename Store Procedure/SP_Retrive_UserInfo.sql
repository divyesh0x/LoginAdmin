IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('dbo.RetrieveUserInfo') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
    DROP PROCEDURE dbo.RetrieveUserInfo
GO

CREATE PROCEDURE dbo.RetrieveUserInfo
(
    @Id int,
    @FirstName varchar(20) OUTPUT,
    @LastName varchar(20) OUTPUT,
    @Address varchar(20) OUTPUT,
    @City varchar(20) OUTPUT,
    @Province varchar(20) OUTPUT,
    @Country varchar(20) OUTPUT,
    @PhoneNumber varchar(20) OUTPUT,
    @Email varchar(20) OUTPUT,
    @Password varchar(20) OUTPUT,
    @isAdmin int OUTPUT
    
)
AS

BEGIN
    SET NOCOUNT ON
   
    SELECT
        @Id = [Id],
        @FirstName = [FirstName],
        @LastName = [LastName],
        @Address = [Address],
        @City = [City],
        @Province = [Province],
        @Country = [Country],
        @PhoneNumber = [PhoneNumber],
        @Email = [Email],
        @Password = [Password],
        @isAdmin = [isAdmin]
       
    FROM userInfo
    WHERE Id = @Id
   
END
