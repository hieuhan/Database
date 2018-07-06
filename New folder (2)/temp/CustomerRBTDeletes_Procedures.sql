--region Drop Existing Procedures

IF OBJECT_ID(N'[dbo].[CustomerRBTDeletes_GetList]') IS NOT NULL
    DROP PROCEDURE [dbo].[CustomerRBTDeletes_GetList]

IF OBJECT_ID(N'[dbo].[CustomerRBTDeletes_Insert]') IS NOT NULL
    DROP PROCEDURE [dbo].[CustomerRBTDeletes_Insert]

IF OBJECT_ID(N'[dbo].[CustomerRBTDeletes_Insert_Quick]') IS NOT NULL
    DROP PROCEDURE [dbo].[CustomerRBTDeletes_Insert_Quick]

IF OBJECT_ID(N'[dbo].[CustomerRBTDeletes_Update]') IS NOT NULL
    DROP PROCEDURE [dbo].[CustomerRBTDeletes_Update]

IF OBJECT_ID(N'[dbo].[CustomerRBTDeletes_Update_Quick]') IS NOT NULL
    DROP PROCEDURE [dbo].[CustomerRBTDeletes_Update_Quick]

IF OBJECT_ID(N'[dbo].[CustomerRBTDeletes_InsertUpdate]') IS NOT NULL
    DROP PROCEDURE [dbo].[CustomerRBTDeletes_InsertUpdate]

IF OBJECT_ID(N'[dbo].[CustomerRBTDeletes_Delete_Quick]') IS NOT NULL
    DROP PROCEDURE [dbo].[CustomerRBTDeletes_Delete_Quick]

IF OBJECT_ID(N'[dbo].[CustomerRBTDeletes_Delete]') IS NOT NULL
    DROP PROCEDURE [dbo].[CustomerRBTDeletes_Delete]

IF OBJECT_ID(N'[dbo].[CustomerRBTDeletes_GetByID]') IS NOT NULL
    DROP PROCEDURE [dbo].[CustomerRBTDeletes_GetByID]

IF OBJECT_ID(N'[dbo].[CustomerRBTDeletes_SelectPaged]') IS NOT NULL
    DROP PROCEDURE [dbo].[CustomerRBTDeletes_SelectPaged]

IF OBJECT_ID(N'[dbo].[CustomerRBTDeletes_GetAll]') IS NOT NULL
    DROP PROCEDURE [dbo].[CustomerRBTDeletes_GetAll]

IF OBJECT_ID(N'[dbo].[CustomerRBTDeletes_GetPage]') IS NOT NULL
    DROP PROCEDURE [dbo].[CustomerRBTDeletes_GetPage]

--endregion

GO


--Genarate system message
EXEC [dbo].[SysMessages_Insert] N'Inserting CustomerRBTDeletes successful',N'Thêm mới dữ liệu thành công',1
EXEC [dbo].[SysMessages_Insert] N'Inserting CustomerRBTDeletes failed',N'Thêm mới dữ liệu không thành công, Lỗi có thể do trùng lặp hoặc đầu vào không hợp lệ',2
EXEC [dbo].[SysMessages_Insert] N'Deleting CustomerRBTDeletes successful',N'Xóa dữ liệu thành công',1
EXEC [dbo].[SysMessages_Insert] N'Deleting CustomerRBTDeletes failed',N'Xóa dữ liệu không thành công, lỗi có thể do bạn chưa xóa hết dữ liệu phụ thuộc',2
EXEC [dbo].[SysMessages_Insert] N'Updating CustomerRBTDeletes successful',N'Cập nhật dữ liệu thành công',1
EXEC [dbo].[SysMessages_Insert] N'Updating CustomerRBTDeletes failed',N'Cập nhật dữ liệu không thành công, lỗi có thể do đầu vào không hợp lệ',2

GO





------------------------------------------------------------------------------------------------------------------------
-- Created by:    huyht  
-- Function:      [dbo].[CustomerRBTDeletes_Insert_Quick]
-- Date created:  25/01/2016
------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[CustomerRBTDeletes_Insert_Quick]
    @ActUserId INT = 0,
    @PhoneNumber nvarchar(50),
    @ProcessStatusId tinyint,
    @RBTCode nvarchar(50),
    @ReturnCode nvarchar(50),
    @ReturnDesc nvarchar(200),
    @CrUserId int,
    @CustomerRBTDeleteId int OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET @CustomerRBTDeleteId=0;
    BEGIN TRY
        BEGIN TRANSACTION
        INSERT INTO [CustomerRBTDeletes] ([PhoneNumber],[ProcessStatusId],[RBTCode],[ReturnCode],[ReturnDesc],[CrUserId])
        VALUES (@PhoneNumber,@ProcessStatusId,@RBTCode,@ReturnCode,@ReturnDesc,@CrUserId);
        IF (@@ERROR=0) BEGIN
            COMMIT TRANSACTION;
            SET @CustomerRBTDeleteId=@@IDENTITY;
        END
        ELSE BEGIN
            ROLLBACK TRANSACTION;
        END
    END TRY
    BEGIN CATCH
      ROLLBACK TRANSACTION;        
      DECLARE @ProcId INT;
	  DECLARE @ProcName NVARCHAR(100);
	  DECLARE @ResultMessage NVARCHAR(4000);
	  DECLARE @ErrorLevelID TINYINT;
	  SET @ProcId = @@PROCID;  
	  SET @ProcName = OBJECT_NAME(@ProcId);
	  SET @ResultMessage = ERROR_MESSAGE(); 
	  SET @ErrorLevelID = 1;  
      EXEC [dbo].[SystemErrors_Insert] @ProcId,@ProcName,@ResultMessage,@ErrorLevelID;
    END CATCH
END 


--endregion

GO




------------------------------------------------------------------------------------------------------------------------
-- Created by:    huyht  
-- Function:      [dbo].[CustomerRBTDeletes_Insert]
-- Date created:  25/01/2016
------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[CustomerRBTDeletes_Insert]
    @Replicated TINYINT = 0,
    @ActUserId INT = 0,
    @PhoneNumber nvarchar(50),
    @ProcessStatusId tinyint,
    @RBTCode nvarchar(50),
    @ReturnCode nvarchar(50),
    @ReturnDesc nvarchar(200),
    @CrUserId int,
    @CustomerRBTDeleteId int OUTPUT,    
    @SysMessageId INT OUTPUT,  
    @SysMessageTypeId TINYINT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        EXEC [dbo].[CustomerRBTDeletes_Insert_Quick] @ActUserId, @PhoneNumber,@ProcessStatusId,@RBTCode,@ReturnCode,@ReturnDesc,@CrUserId,@CustomerRBTDeleteId OUTPUT;       
        IF (@CustomerRBTDeleteId>0) BEGIN
            EXEC dbo.SysMessages_GetSysMessageId N'Inserting CustomerRBTDeletes successful',@SysMessageId OUTPUT,@SysMessageTypeId OUTPUT;
        END
        ELSE BEGIN
            EXEC dbo.SysMessages_GetSysMessageId N'Inserting CustomerRBTDeletes failed',@SysMessageId OUTPUT,@SysMessageTypeId OUTPUT;
        END
    END TRY
    BEGIN CATCH
        DECLARE @ProcId INT;
        DECLARE @ProcName NVARCHAR(100);
        DECLARE @ResultMessage NVARCHAR(4000);
        DECLARE @ErrorLevelID TINYINT;
        SET @ProcId = @@PROCID;  
        SET @ProcName = OBJECT_NAME(@ProcId);
        SET @ResultMessage = ERROR_MESSAGE(); 
        SET @ErrorLevelID = 1;  
        EXEC [dbo].[SystemErrors_Insert] @ProcId,@ProcName,@ResultMessage,@ErrorLevelID;    
        EXEC dbo.SysMessages_GetSysMessageId N'Inserting CustomerRBTDeletes failed',@SysMessageId OUTPUT,@SysMessageTypeId OUTPUT; 
    END CATCH
END 


--endregion

GO




------------------------------------------------------------------------------------------------------------------------
-- Created by:    huyht  
-- Function:      [dbo].[CustomerRBTDeletes_Update_Quick]
-- Date created:  25/01/2016
------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[CustomerRBTDeletes_Update_Quick]
    @ActUserId INT = 0,
    @PhoneNumber nvarchar(50),
    @ProcessStatusId tinyint,
    @RBTCode nvarchar(50),
    @ReturnCode nvarchar(50),
    @ReturnDesc nvarchar(200),
    @CustomerRBTDeleteId int
AS 
BEGIN
    SET NOCOUNT ON; 
    BEGIN TRY
    BEGIN TRANSACTION 
        UPDATE [CustomerRBTDeletes] SET
            [PhoneNumber] = @PhoneNumber
            ,[ProcessStatusId] = @ProcessStatusId
            ,[RBTCode] = @RBTCode
            ,[ReturnCode] = @ReturnCode
            ,[ReturnDesc] = @ReturnDesc
        WHERE [CustomerRBTDeleteId] = @CustomerRBTDeleteId;
                            
        IF (@@ERROR=0) BEGIN
            COMMIT TRANSACTION;
        END
        ELSE BEGIN
            ROLLBACK TRANSACTION;
        END
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        DECLARE @ProcId INT;
        DECLARE @ProcName NVARCHAR(100);
        DECLARE @ResultMessage NVARCHAR(4000);
        DECLARE @ErrorLevelID TINYINT;
        SET @ProcId = @@PROCID;  
        SET @ProcName = OBJECT_NAME(@ProcId);
        SET @ResultMessage = ERROR_MESSAGE(); 
        SET @ErrorLevelID = 1;  
        EXEC [dbo].[SystemErrors_Insert] @ProcId,@ProcName,@ResultMessage,@ErrorLevelID;     
    END CATCH
END


--endregion

GO




------------------------------------------------------------------------------------------------------------------------
-- Created by:    huyht  
-- Function:      [dbo].[CustomerRBTDeletes_Update]
-- Date created:  25/01/2016
------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[CustomerRBTDeletes_Update]
    @Replicated TINYINT = 0,
    @ActUserId INT = 0,
    @PhoneNumber nvarchar(50),
    @ProcessStatusId tinyint,
    @RBTCode nvarchar(50),
    @ReturnCode nvarchar(50),
    @ReturnDesc nvarchar(200),
    @CustomerRBTDeleteId int,
    @SysMessageId INT OUTPUT,
    @SysMessageTypeId TINYINT OUTPUT
AS 

BEGIN
    SET NOCOUNT ON;
    BEGIN TRY  
        EXEC [dbo].[CustomerRBTDeletes_Update_Quick] @ActUserId, @PhoneNumber,@ProcessStatusId,@RBTCode,@ReturnCode,@ReturnDesc,@CustomerRBTDeleteId;
        EXEC dbo.SysMessages_GetSysMessageId N'Updating CustomerRBTDeletes successful',@SysMessageId OUTPUT,@SysMessageTypeId OUTPUT;
    END TRY
    BEGIN CATCH
        DECLARE @ProcId INT;
        DECLARE @ProcName NVARCHAR(100);
        DECLARE @ResultMessage NVARCHAR(4000);
        DECLARE @ErrorLevelID TINYINT;
        SET @ProcId = @@PROCID;  
        SET @ProcName = OBJECT_NAME(@ProcId);
        SET @ResultMessage = ERROR_MESSAGE(); 
        SET @ErrorLevelID = 1;  
        EXEC [dbo].[SystemErrors_Insert] @ProcId,@ProcName,@ResultMessage,@ErrorLevelID;    
        EXEC dbo.SysMessages_GetSysMessageId N'Updating CustomerRBTDeletes failed',@SysMessageId OUTPUT,@SysMessageTypeId OUTPUT;
    END CATCH
END


--endregion

GO



------------------------------------------------------------------------------------------------------------------------
-- Created by:    huyht  
-- Function:      [dbo].[CustomerRBTDeletes_Exists]
-- Date created:  25/01/2016
------------------------------------------------------------------------------------------------------------------------

CREATE FUNCTION [dbo].[CustomerRBTDeletes_Exists]
    (@CustomerRBTDeleteId INT  )
RETURNS INT  
AS
BEGIN
  DECLARE @RetVal INT  ;
  IF (EXISTS(SELECT 1 FROM [CustomerRBTDeletes] WHERE (CustomerRBTDeleteId=@CustomerRBTDeleteId))) BEGIN
    SET @RetVal=1;
  END
  ELSE BEGIN
    SET @RetVal=0;
  END
  RETURN @RetVal;
END

--endregion

GO





------------------------------------------------------------------------------------------------------------------------
-- Created by:    huyht  
-- Function:      [dbo].[CustomerRBTDeletes_InsertOrUpdate]
-- Date created:  25/01/2016
------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[CustomerRBTDeletes_InsertOrUpdate]
    @Replicated TINYINT = 0,
    @ActUserId INT = 0,
    @PhoneNumber nvarchar(50),
    @ProcessStatusId tinyint,
    @RBTCode nvarchar(50),
    @ReturnCode nvarchar(50),
    @ReturnDesc nvarchar(200),
    @CrUserId int,
    @CustomerRBTDeleteId int OUTPUT,
    @SysMessageId INT OUTPUT,
    @SysMessageTypeId TINYINT OUTPUT
AS 

BEGIN
    SET NOCOUNT ON;
    BEGIN TRY  
        IF ([dbo].[CustomerRBTDeletes_Exists](@CustomerRBTDeleteId)>0) BEGIN    
            EXEC [dbo].[CustomerRBTDeletes_Update] @Replicated,@ActUserId, @PhoneNumber,@ProcessStatusId,@RBTCode,@ReturnCode,@ReturnDesc,@CustomerRBTDeleteId,@SysMessageId OUTPUT,@SysMessageTypeId OUTPUT;  
        END
        ELSE BEGIN      
            EXEC [dbo].[CustomerRBTDeletes_Insert] @Replicated,@ActUserId, @PhoneNumber,@ProcessStatusId,@RBTCode,@ReturnCode,@ReturnDesc,@CrUserId,@CustomerRBTDeleteId OUTPUT,@SysMessageId OUTPUT,@SysMessageTypeId OUTPUT;     
        END
    END TRY
    BEGIN CATCH
        DECLARE @ProcId INT;
        DECLARE @ProcName NVARCHAR(100);
        DECLARE @ResultMessage NVARCHAR(4000);
        DECLARE @ErrorLevelID TINYINT;
        SET @ProcId = @@PROCID;  
        SET @ProcName = OBJECT_NAME(@ProcId);
        SET @ResultMessage = ERROR_MESSAGE(); 
        SET @ErrorLevelID = 1;  
        EXEC [dbo].[SystemErrors_Insert] @ProcId,@ProcName,@ResultMessage,@ErrorLevelID;    
        EXEC dbo.SysMessages_GetSysMessageId N'Updating CustomerRBTDeletes failed',@SysMessageId OUTPUT,@SysMessageTypeId OUTPUT;
    END CATCH
END


--endregion

GO





------------------------------------------------------------------------------------------------------------------------
-- Created by:    huyht  
-- Function:      [dbo].[CustomerRBTDeletes_Delete_Quick]
-- Date created:  25/01/2016
------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[CustomerRBTDeletes_Delete_Quick]
    @ActUserId INT = 0,
    @CustomerRBTDeleteId int
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION
        DELETE FROM [CustomerRBTDeletes]
        WHERE [CustomerRBTDeleteId] = @CustomerRBTDeleteId;
        IF (@@ERROR=0) BEGIN
            COMMIT TRANSACTION;
        END
        ELSE BEGIN
            ROLLBACK TRANSACTION;
        END
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        DECLARE @ProcId INT;
        DECLARE @ProcName NVARCHAR(100);
        DECLARE @ResultMessage NVARCHAR(4000);
        DECLARE @ErrorLevelID TINYINT;
        SET @ProcId = @@PROCID;  
        SET @ProcName = OBJECT_NAME(@ProcId);
        SET @ResultMessage = ERROR_MESSAGE(); 
        SET @ErrorLevelID = 1;  
        EXEC [dbo].[SystemErrors_Insert] @ProcId,@ProcName,@ResultMessage,@ErrorLevelID;   
    END CATCH
END


--endregion

GO



------------------------------------------------------------------------------------------------------------------------
-- Created by:    huyht  
-- Function:      [dbo].[CustomerRBTDeletes_Delete]
-- Date created:  25/01/2016
------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[CustomerRBTDeletes_Delete]
    @Replicated TINYINT = 0,
    @ActUserId INT = 0,
    @CustomerRBTDeleteId int, 
    @SysMessageId INT OUTPUT,
    @SysMessageTypeId TINYINT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY 
        EXEC [dbo].[CustomerRBTDeletes_Delete_Quick] @ActUserId, @CustomerRBTDeleteId;
        IF (EXISTS(SELECT 1 FROM CustomerRBTDeletes WHERE (CustomerRBTDeleteId=@CustomerRBTDeleteId))) BEGIN
            EXEC dbo.SysMessages_GetSysMessageId N'Deleting CustomerRBTDeletes failed',@SysMessageId OUTPUT,@SysMessageTypeId OUTPUT;
        END
        ELSE BEGIN
            EXEC dbo.SysMessages_GetSysMessageId N'Deleting CustomerRBTDeletes successful',@SysMessageId OUTPUT,@SysMessageTypeId OUTPUT;
        END
    END TRY 
    BEGIN CATCH
        DECLARE @ProcId INT;
        DECLARE @ProcName NVARCHAR(100);
        DECLARE @ResultMessage NVARCHAR(4000);
        DECLARE @ErrorLevelID TINYINT;
        SET @ProcId = @@PROCID;  
        SET @ProcName = OBJECT_NAME(@ProcId);
        SET @ResultMessage = ERROR_MESSAGE(); 
        SET @ErrorLevelID = 1;  
        EXEC [dbo].[SystemErrors_Insert] @ProcId,@ProcName,@ResultMessage,@ErrorLevelID ;    
        EXEC dbo.SysMessages_GetSysMessageId N'Deleting CustomerRBTDeletes failed',@SysMessageId OUTPUT,@SysMessageTypeId OUTPUT;
    END CATCH
END


--endregion

GO




------------------------------------------------------------------------------------------------------------------------
-- Created by:    huyht  
-- Function:      [dbo].[CustomerRBTDeletes_GetPage]
-- Date created:  25/01/2016
------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE  [dbo].[CustomerRBTDeletes_GetPage] 
     @CustomerRBTDeleteId INT=0
    ,@PhoneNumber NVARCHAR(50)=''
    ,@ProcessStatusId TINYINT=0
    ,@RBTCode NVARCHAR(50)=''
    ,@ReturnCode NVARCHAR(50)=''
    ,@ReturnDesc NVARCHAR(200)=''
    ,@CrUserId INT=0
    ,@DateFrom NVARCHAR(50)=''
    ,@DateTo NVARCHAR(50)=''
    ,@OrderBy NVARCHAR(255)=''
    ,@PageSize INT=10
    ,@PageNumber INT=0
    ,@RowCount INT OUTPUT    
AS  
BEGIN
    SET NOCOUNT ON;
    DECLARE @DbName NVARCHAR(255);
    DECLARE @FieldName NVARCHAR(255);
    DECLARE @where_clause NVARCHAR(4000);
    DECLARE @fieldSelect NVARCHAR(4000);
    DECLARE @FieldListOrder NVARCHAR(4000);
    DECLARE @query NVARCHAR(4000);
    BEGIN TRY  
        SET @DbName='';
        SET @FieldName='CustomerRBTDeleteId'; 
        IF (@OrderBy='' OR @OrderBy IS NULL) SET @OrderBy = 'CustomerRBTDeleteId DESC';
        SET @where_clause = '';              
        IF (@CustomerRBTDeleteId>0) BEGIN
          IF (LEN(@where_clause)>0) SET @where_clause=@where_clause + ' AND ';
          SET @where_clause = @where_clause + '(CustomerRBTDeleteId=' +  CONVERT(NVARCHAR, @CustomerRBTDeleteId) + ')';
        END
        -------------------------------------------
        IF (LEN(@PhoneNumber)>0) BEGIN
          IF (LEN(@where_clause)>0) SET @where_clause=@where_clause + ' AND ';
          SET @where_clause = @where_clause + '(PhoneNumber LIKE N''%' + @PhoneNumber + '%'')';
        END
        -------------------------------------------
        IF (@ProcessStatusId>0) BEGIN
          IF (LEN(@where_clause)>0) SET @where_clause=@where_clause + ' AND ';
          SET @where_clause = @where_clause + '(ProcessStatusId=' +  CONVERT(NVARCHAR, @ProcessStatusId) + ')';
        END
        -------------------------------------------
        IF (LEN(@RBTCode)>0) BEGIN
          IF (LEN(@where_clause)>0) SET @where_clause=@where_clause + ' AND ';
          SET @where_clause = @where_clause + '(RBTCode LIKE N''%' + @RBTCode + '%'')';
        END
        -------------------------------------------
        IF (LEN(@ReturnCode)>0) BEGIN
          IF (LEN(@where_clause)>0) SET @where_clause=@where_clause + ' AND ';
          SET @where_clause = @where_clause + '(ReturnCode LIKE N''%' + @ReturnCode + '%'')';
        END
        -------------------------------------------
        IF (LEN(@ReturnDesc)>0) BEGIN
          IF (LEN(@where_clause)>0) SET @where_clause=@where_clause + ' AND ';
          SET @where_clause = @where_clause + '(ReturnDesc LIKE N''%' + @ReturnDesc + '%'')';
        END
        -------------------------------------------
        IF (@CrUserId>0) BEGIN
          IF (LEN(@where_clause)>0) SET @where_clause=@where_clause + ' AND ';
          SET @where_clause = @where_clause + '(CrUserId=' +  CONVERT(NVARCHAR, @CrUserId) + ')';
        END
        -------------------------------------------
        -------------------------------------------
        IF (@DateFrom <> '') BEGIN
        IF (LEN(@where_clause)>0) SET @where_clause=@where_clause + ' AND ';
        SET @where_clause = @where_clause + '(CrDateTime >=''' + @DateFrom + ' 00:00:00'')';
        END
        -------------------------------------------
        IF (@DateTo <> '') BEGIN
        IF (LEN(@where_clause)>0) SET @where_clause=@where_clause + ' AND ';
        SET @where_clause = @where_clause + '(CrDateTime <=''' + @DateTo + ' 23:59:59'')';
        END
        -------------------------------------------
        SET @query = 'CustomerRBTDeletes';   
        EXEC [dbo].[Dyn_CountField]  @DbName,@Query,@FieldName, @where_clause,@RowCount  OUTPUT;
        EXEC [dbo].[Dyn_GetPage_FieldListDiff] @DbName,@Query,@where_clause,@OrderBy,@PageSize,@PageNumber,@RowCount,@fieldSelect,@FieldListOrder;
        
    END TRY
    BEGIN CATCH
        DECLARE @ProcId INT;
        DECLARE @ProcName NVARCHAR(100);
        DECLARE @ResultMessage NVARCHAR(4000);
        DECLARE @ErrorLevelID TINYINT;
        SET @ProcId = @@PROCID;  
        SET @ProcName = OBJECT_NAME(@ProcId);
        SET @ResultMessage = ERROR_MESSAGE() + '- Query: ' + @where_clause; 
        SET @ErrorLevelID = 1;  
        EXEC [dbo].[SystemErrors_Insert] @ProcId,@ProcName,@ResultMessage,@ErrorLevelID; 
    END CATCH
END

--endregion

GO

