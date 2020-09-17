IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspQueryGet]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[uspQueryGet] 
GO
CREATE PROCEDURE uspQueryGet
		@TypeCode int
AS
BEGIN
		Select [CodeID], [CodeTypeId], ISNULL([Description],'')AS [Description], ISNULL([Ref],'')AS [Ref] from TCode WHERE CodeTypeId = @TypeCode
END
GO


-- uspLogin 'admin', 'M//EekBeJOtNs7V+Sp0xBA{a61}{a61}'
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspLogin]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[uspLogin]
GO
CREATE PROCEDURE uspLogin
		@username nvarchar(200),
		@password nvarchar(max)
AS
BEGIN
	Declare @error int,
			@ErrorSuccessMsg varchar(100)
		
		IF EXISTS (SELECT 1 FROM TAdminUser WHERE Username = @username and Password = @password)
		BEGIN
			UPDATE TAdminUser SET LastLogged = LoginBeforeLastLogged, LoginBeforeLastLogged = Getdate() WHERE Username = @username and Password = @password
			print 123
			SELECT
				FirstName + ' ' + LastName as DisplayName,
				AdminUserID AS UserID,
				LastLogged,
				LoginBeforeLastLogged,
				1 LoginTypeCode,
				1 Success,
				'Admin Login Successfully!' AS ErrorSuccessMsg
			FROM
				TAdminUser
			WHERE 
				Username = @username and Password = @password
				print 456
		END
		ELSE IF EXISTS (SELECT 1 FROM TCompany WHERE Username = @username and Password = @password)
		BEGIN
			UPDATE TCompany SET LastLogged = LoginBeforeLastLogged, LoginBeforeLastLogged = GETDATE() WHERE Username = @username and Password = @password
			SELECT
				Name as DisplayName,
				CompanyID AS UserID,
				LastLogged,
				LoginBeforeLastLogged,
				2 LoginTypeCode,
				1 Success,
				'Company Login Successfully!' AS ErrorSuccessMsg
			FROM
				TCompany 
			WHERE 
				Username = @username and Password = @password
		END
		ELSE IF EXISTS (SELECT 1 FROM TBranch WHERE Username = @username and Password = @password)
		BEGIN
			UPDATE TBranch SET LastLogged = LoginBeforeLastLogged, LoginBeforeLastLogged = GETDATE() WHERE Username = @username and Password = @password
			SELECT
				Name as DisplayName,
				BranchID AS UserID,
				LastLogged,
				LoginBeforeLastLogged,
				3 LoginTypeCode,
				1 Success,
				'Branch Login Successfully!' AS ErrorSuccessMsg
			FROM
				TBranch
			WHERE 
				Username = @username and Password = @password
		END
	ELSE
	BEGIN
		SELECT
			0 AS UserID,
			Null LastLogged,
			Null LoginBeforeLastLogged,
			0 LoginTypeCode,
			0 Success,
			'User doesnot exist Please try again to enter valid user details!' AS ErrorSuccessMsg
	END
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCompanySave]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[uspCompanySave]
GO
CREATE PROCEDURE [uspCompanySave]
			@CompanyID bigint,
			@Name nvarchar(255),
			@Address nvarchar(max),
			@Email nvarchar(20),
			@Mobile nvarchar(15),
			@LogoImgPath nvarchar(255),
			@Username nvarchar(25),
			@Password nvarchar(100),
			@CreatedBy int,
			@ModifiedBy int
AS
BEGIN
	Declare @error int,
			@ErrorSuccessMsg varchar(100)
		
	BEGIN TRANSACTION CompanyDetailsSave
		
	IF(@CompanyID = 0)
	BEGIN
		-- Staus : 47 Pending, 48 : Approve, 49 : Rejected
		INSERT INTO TCompany([Name], [Address], [Email], [Mobile], [LogoImage], [Username], [Password], [Status], [LastLogged], [LoginBeforeLastLogged], [Created], [CreatedBy], [Modified], [ModifiedBy], [AdminUserID])
		SELECT 
			@Name, @Address, @Email, @Mobile, @LogoImgPath, @Username, @Password, 6, GETDATE(), GETDATE(), GETDATE(), @CreatedBy, GETDATE(), @ModifiedBy, @CreatedBy

		SET @error = @@ERROR
		SET @CompanyID = @@IDENTITY
		SET @ErrorSuccessMsg = 'Company Added Succesfully!'
	END
	ELSE IF ((SELECT COUNT(1) FROM TCompany WHERE CompanyID = @CompanyID) > 0)
	BEGIN
			UPDATE TCompany
			SET
				Name = @Name, 
				Address = @Address, 
				Mobile= @Mobile,
				Email = @Email,
				Password = CASE WHEN ISNULL(@Password,'') != '' AND @Password != 'xxxxxxxxxx' THEN @Password ELSE Password END,
				LogoImage = (CASE WHEN (ISNULL(@LogoImgPath,'') != '') THEN @LogoImgPath ELSE LogoImage END),
				Modified = GETDATE(),
				ModifiedBy = @ModifiedBy
			WHERE
				CompanyID = @CompanyID
				SET @error = @@ERROR
			
			SET @ErrorSuccessMsg = 'Company Detail Updated Succesfully!'
	END

	IF @error != 0
	BEGIN
		ROLLBACK TRANSACTION CompanyDetailsSave
		SELECT 0 IsSuccess,
			'Something went wrong. Please try again.!!' AS ErrorSuccessMsg
	END
	ELSE
	BEGIN
		COMMIT TRANSACTION CompanyDetailsSave
		SELECT 1 IsSuccess,
				@ErrorSuccessMsg AS ErrorSuccessMsg
	END
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCompanyInfoGet]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[uspCompanyInfoGet]
GO
-- uspCompanyInfoGet 1, 1
CREATE PROCEDURE [uspCompanyInfoGet]
			@CompanyID int null
AS
BEGIN
	IF(@CompanyID <> 0)
	BEGIN
	SELECT [CompanyID], [Name], [Address], [Email], [Mobile], [LogoImage], [Username], [Password], 
		(SELECT Description FROM TCode WHERE CodeID = [Status])AS [Status], 
		[LastLogged], [LoginBeforeLastLogged], [Created], [CreatedBy], [Modified], [ModifiedBy], [AdminUserID]
		FROM TCompany
		WHERE CompanyID = @CompanyID
	END
	ELSE
	BEGIN
		SELECT [CompanyID], [Name], [Address], [Email], [Mobile], [LogoImage], [Username], [Password], 
		(SELECT Description FROM TCode WHERE CodeID = [Status])AS [Status], 
		[LastLogged], [LoginBeforeLastLogged], [Created], [CreatedBy], [Modified], [ModifiedBy], [AdminUserID]
		FROM TCompany
	END
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspBranchDetailSave]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[uspBranchDetailSave]
GO

CREATE PROCEDURE [uspBranchDetailSave]
			@BranchID bigint,
			@CompanyID bigint,
			@Name nvarchar(255),
			@Address nvarchar(max),
			@Email nvarchar(20),
			@Location nvarchar(200),
			@Mobile nvarchar(15),
			@Username nvarchar(25),
			@Password nvarchar(100),
			@CreatedBy int,
			@ModifiedBy int,
			@ModifiedSourceCode int
AS
BEGIN
	Declare @error int,
			@ErrorSuccessMsg varchar(100)
		
	BEGIN TRANSACTION BranchDetailsSave
		
	IF(@BranchID = 0)
	BEGIN
		INSERT INTO TBranch([Name], [Address], [Email], [Location], [Mobile], [Username], [Password], [Status], [LastLogged], [LoginBeforeLastLogged], [Created], [CreatedBy], [Modified], [ModifiedBy], [CompanyID], [ModifiedSourceCode])
		SELECT 
			@Name, @Address, @Email, @Location, @Mobile, @Username, @Password, 6, GETDATE(), GETDATE(), GETDATE(), @CreatedBy, GETDATE(), @ModifiedBy, @CompanyID, @ModifiedSourceCode

		SET @error = @@ERROR
		SET @BranchID = @@IDENTITY
		SET @ErrorSuccessMsg = 'Branch Details Added Succesfully!'
	END
	ELSE IF ((SELECT COUNT(1) FROM TBranch WHERE BranchID = @BranchID) > 0)
	BEGIN
			UPDATE TBranch
			SET
				[Name] = @Name, 
				[Address] = @Address, 
				Mobile= @Mobile,
				Email = @Email,
				[Location] = @Location,
				[Password] = CASE WHEN ISNULL(@Password,'') != '' AND @Password != 'xxxxxxxxxx' THEN @Password ELSE [Password] END,
				Modified = GETDATE(),
				ModifiedBy = @ModifiedBy,
				ModifiedSourceCode = @ModifiedSourceCode
			WHERE
				BranchID = @BranchID
				SET @error = @@ERROR
			
			SET @ErrorSuccessMsg = 'Branch Details Updated Succesfully!'
	END

	IF @error != 0
	BEGIN
		ROLLBACK TRANSACTION BranchDetailsSave
		SELECT 0 IsSuccess,
			'Something went wrong. Please try again.!!' AS ErrorSuccessMsg
	END
	ELSE
	BEGIN
		COMMIT TRANSACTION BranchDetailsSave
		SELECT 1 IsSuccess,
				@ErrorSuccessMsg AS ErrorSuccessMsg
	END
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspBranchInfoGet]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[uspBranchInfoGet]
GO
-- uspCompanyInfoGet 1, 0
CREATE PROCEDURE [uspBranchInfoGet]
			@CompanyID int null,
			@BranchID int null
AS
BEGIN
	IF(@CompanyID <> 0)
	BEGIN
		SELECT [BranchID], [Name], [Address], [Email], [Mobile], [Username], [Password], [Location],
		(SELECT Description FROM TCode WHERE CodeID = [Status])AS [Status], 
		[LastLogged], [LoginBeforeLastLogged], [Created], [CreatedBy], [Modified], [ModifiedBy], [CompanyID]
		FROM TBranch
		WHERE CompanyID = @CompanyID
	END
	ELSE
	BEGIN
		SELECT [BranchID], [Name], [Address], [Email], [Mobile], [Username], [Password], [Location],
		(SELECT Description FROM TCode WHERE CodeID = [Status])AS [Status], 
		[LastLogged], [LoginBeforeLastLogged], [Created], [CreatedBy], [Modified], [ModifiedBy], [CompanyID]
		FROM TBranch
		WHERE BranchID = @BranchID
	END
END
GO


IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCategoryDetailSave]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[uspCategoryDetailSave]
GO
CREATE PROCEDURE [uspCategoryDetailSave]
			@CategoryID bigint,
			@Name nvarchar(255),
			@Description nvarchar(max),
			@LogoImgPath nvarchar(255),
			@CreatedBy int,
			@ModifiedBy int,
			@ModifiedSourceCode int
AS
BEGIN
	Declare @error int,
			@ErrorSuccessMsg varchar(100)
		
	BEGIN TRANSACTION CategoryDetailSave
		
	IF(@CategoryID = 0)
	BEGIN
		INSERT INTO TCategory([Name], [Description], [LogoImage], [Created], [CreatedBy], [Modified], [ModifiedBy],[CreatedSourceCode], [ModifiedSourceCode])
		SELECT 
			@Name, @Description, @LogoImgPath, GETDATE(), @CreatedBy, GETDATE(), @ModifiedBy, @ModifiedSourceCode, @ModifiedSourceCode

		SET @error = @@ERROR
		SET @CategoryID = @@IDENTITY
		SET @ErrorSuccessMsg = 'Category Added Succesfully!'
	END
	ELSE IF ((SELECT COUNT(1) FROM TCategory WHERE CategoryID = @CategoryID) > 0)
	BEGIN
			UPDATE TCategory
			SET
				[Name] = @Name, 
				[Description] = @Description, 
				LogoImage = (CASE WHEN (ISNULL(@LogoImgPath,'') != '') THEN @LogoImgPath ELSE LogoImage END),
				Modified = GETDATE(),
				ModifiedBy = @ModifiedBy,
				ModifiedSourceCode = @ModifiedSourceCode
			WHERE
				CategoryID = @CategoryID
				SET @error = @@ERROR
			
			SET @ErrorSuccessMsg = 'Category Updated Succesfully!'
	END

	IF @error != 0
	BEGIN
		ROLLBACK TRANSACTION CategoryDetailSave
		SELECT 0 IsSuccess,
			'Something went wrong. Please try again.!!' AS ErrorSuccessMsg
	END
	ELSE
	BEGIN
		COMMIT TRANSACTION CategoryDetailSave
		SELECT 1 IsSuccess,
				@ErrorSuccessMsg AS ErrorSuccessMsg
	END
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspPositionDetailSave]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[uspPositionDetailSave]
GO
CREATE PROCEDURE [uspPositionDetailSave]
			@PositionID bigint,
			@CategoryID bigint,
			@Name nvarchar(255),
			@Description nvarchar(max),
			@LogoImgPath nvarchar(255),
			@CreatedBy int,
			@ModifiedBy int,
			@ModifiedSourceCode int
AS
BEGIN
	Declare @error int,
			@ErrorSuccessMsg varchar(100)
		
	BEGIN TRANSACTION PositionDetailsSave
		
	IF(@PositionID = 0)
	BEGIN
		-- Staus : 47 Pending, 48 : Approve, 49 : Rejected
		INSERT INTO TPosition([Name], [Description], [LogoImage], [Created], [CreatedBy], [Modified], [ModifiedBy], [CreatedSourceCode], [ModifiedSourceCode], [CodeID])
		SELECT 
			@Name, @Description, @LogoImgPath, GETDATE(), @CreatedBy, GETDATE(), @ModifiedBy, @ModifiedSourceCode, @ModifiedSourceCode, @CategoryID

		SET @error = @@ERROR
		SET @PositionID = @@IDENTITY
		SET @ErrorSuccessMsg = 'Position Added Succesfully!'
	END
	ELSE IF ((SELECT COUNT(1) FROM TPosition WHERE PositionID = @PositionID) > 0)
	BEGIN
			UPDATE TPosition
			SET
				Name = @Name, 
				Description = @Description, 
				CodeID = @CategoryID,
				LogoImage = (CASE WHEN (ISNULL(@LogoImgPath,'') != '') THEN @LogoImgPath ELSE LogoImage END),
				Modified = GETDATE(),
				ModifiedBy = @ModifiedBy,
				ModifiedSourceCode = @ModifiedSourceCode
			WHERE
				PositionID = @PositionID
				SET @error = @@ERROR
			
			SET @ErrorSuccessMsg = 'Position Updated Succesfully!'
	END

	IF @error != 0
	BEGIN
		ROLLBACK TRANSACTION PositionDetailsSave
		SELECT 0 IsSuccess,
			'Something went wrong. Please try again.!!' AS ErrorSuccessMsg
	END
	ELSE
	BEGIN
		COMMIT TRANSACTION PositionDetailsSave
		SELECT 1 IsSuccess,
				@ErrorSuccessMsg AS ErrorSuccessMsg
	END
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspOrnamentDetailSave]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[uspOrnamentDetailSave]
GO
CREATE PROCEDURE [uspOrnamentDetailSave]
			@OrnamentID int,
			@CategoryID int,
			@PositionID int,
			@Name nvarchar(255),
			@Description nvarchar(max),
			@Weight nvarchar(255),
			@Cost decimal(22,2),
			@LogoImgPath ImageList READONLY,
			@CreatedBy int,
			@ModifiedBy int,
			@ModifiedSourceCode int
AS
BEGIN
	Declare @error int,
			@ErrorSuccessMsg varchar(100)
		
	BEGIN TRANSACTION OrnamentsDetailsSave
	
	IF(@OrnamentID = 0)
	BEGIN
		-- Staus : 47 Pending, 48 : Approve, 49 : Rejected
		INSERT INTO [TOrnament]([Name], [Description], [CategoryID], [PositionID], [Weight], [Cost], [Created], [CreatedBy], [Modified], [ModifiedBy], [CreatedSourceCode], [ModifiedSourceCode])
		SELECT 
			@Name, @Description, @CategoryID,  @PositionID, @Weight, @Cost, GETDATE(), @CreatedBy, GETDATE(), @ModifiedBy, @ModifiedSourceCode, @ModifiedSourceCode
			
		SET @error = @@ERROR
		SET @OrnamentID = @@IDENTITY
		-- Added Images For Ornaments
			IF (@Error = 0)
			BEGIN
				IF ((Select Count(1) From @LogoImgPath) > 0)	
				BEGIN			
					INSERT INTO TImages(OrnamentID, ImgPath, Created, CreatedBy, Modified, ModifiedBy)
						SELECT 
							@OrnamentID, ImgPath, GETDATE(), @ModifiedBy, GETDATE(), @ModifiedBy
						FROM 
							@LogoImgPath
					SET @Error = @@ERROR
				END
			END
		SET @ErrorSuccessMsg = 'Ornament Added Succesfully!'
	END
	ELSE IF ((SELECT COUNT(1) FROM [TOrnament] WHERE OrnamentID = @OrnamentID) > 0)
	BEGIN
			UPDATE [TOrnament]
			SET
				Name = @Name, 
				Description = @Description, 
				[CategoryID] =@CategoryID, 
				[PositionID] = @PositionID, 
				[Weight] =@Weight, 
				[Cost]= @Cost,
				Modified = GETDATE(),
				ModifiedBy = @ModifiedBy,
				ModifiedSourceCode = @ModifiedSourceCode
			WHERE
				OrnamentID = @OrnamentID
				SET @error = @@ERROR

			-- UN-ASSIGN OLD EMPLOYEE TO TIE USER ACTION PROJECT ITEM TABLE					
			IF (@Error = 0)
			BEGIN
				IF ((Select Count(1) From @LogoImgPath) > 0)	
				BEGIN
					DELETE FROM TImages WHERE  OrnamentID = @OrnamentID
					SET @Error = @@ERROR		
				END
			END

			-- Added Images For Ornaments
			IF (@Error = 0)
			BEGIN
				IF ((Select Count(1) From @LogoImgPath) > 0)	
				BEGIN			
					INSERT INTO TImages(OrnamentID, ImgPath, Created, CreatedBy, Modified, ModifiedBy)
						SELECT 
							@OrnamentID, ImgPath, GETDATE(), @ModifiedBy, GETDATE(), @ModifiedBy
						FROM 
							@LogoImgPath
					SET @Error = @@ERROR
				END
			END
			
			SET @ErrorSuccessMsg = 'Ornament Updated Succesfully!'
	END

	IF @error != 0
	BEGIN
		ROLLBACK TRANSACTION OrnamentsDetailsSave
		SELECT 0 IsSuccess,
			'Something went wrong. Please try again.!!' AS ErrorSuccessMsg
	END
	ELSE
	BEGIN
		COMMIT TRANSACTION OrnamentsDetailsSave
		SELECT 1 IsSuccess,
				@ErrorSuccessMsg AS ErrorSuccessMsg
	END
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspOrnamentsPositionInfoGet]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[uspOrnamentsPositionInfoGet]
GO
-- uspCompanyInfoGet 1, 1
CREATE PROCEDURE [uspOrnamentsPositionInfoGet]
		@PositionID int null
AS
BEGIN
	IF(@PositionID <> 0)
	BEGIN
		SELECT TP.[PositionID], TP.[Name], TP.[Description], TP.[LogoImage],
		(SELECT TC.Description FROM TCode TC WHERE TC.[CodeID] = TP.[CodeID])AS [CategoryName], 
		TP.[Created], TP.[CreatedBy], TP.[Modified], TP.[ModifiedBy], TP.[CodeID] AS CategoryID
		FROM TPosition TP
		WHERE TP.PositionID = @PositionID
	END
	ELSE
	BEGIN
		SELECT TP.[PositionID], TP.[Name], TP.[Description], TP.[LogoImage],
		(SELECT TC.Description FROM TCode TC WHERE TC.[CodeID] = TP.[CodeID])AS [CategoryName], 
		TP.[Created], TP.[CreatedBy], TP.[Modified], TP.[ModifiedBy], TP.[CodeID] AS CategoryID
		FROM TPosition TP
	END
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspOrnamentsInfoGet]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[uspOrnamentsInfoGet]
GO
-- uspCompanyInfoGet 1, 1
CREATE PROCEDURE [uspOrnamentsInfoGet]
		@OrnamentID int null
AS
BEGIN
	IF(@OrnamentID <> 0)
	BEGIN
		SELECT 
			O.[OrnamentID], O.[Name], O.[Description], O.[CategoryID], 
			TC.[Name] AS [CategoryName],
			O.[PositionID], 
			TP.[Name] AS [PositionName],
			O.[Weight], O.[Cost], 
			O.[Created], O.[CreatedBy], O.[Modified], O.[ModifiedBy], O.[CreatedSourceCode], O.[ModifiedSourceCode] 
		FROM TOrnament O 
			INNER JOIN TCategory TC On O.CategoryID = TC.CategoryID
			INNER JOIN TPosition TP On O.PositionID = TP.PositionID
		WHERE OrnamentID = @OrnamentID
		
		SELECT * FROM TImages WHERE OrnamentID = @OrnamentID
	END
	ELSE
	BEGIN
		SELECT 
			O.[OrnamentID], O.[Name], O.[Description], O.[CategoryID], 
			TC.[Name] AS [CategoryName],
			O.[PositionID], 
			TP.[Name] AS [PositionName],
			O.[Weight], O.[Cost], 
			O.[Created], O.[CreatedBy], O.[Modified], O.[ModifiedBy], O.[CreatedSourceCode], O.[ModifiedSourceCode] 
		FROM TOrnament O 
			INNER JOIN TCategory TC On O.CategoryID = TC.CategoryID
			INNER JOIN TPosition TP On O.PositionID = TP.PositionID
			
		SELECT * FROM TImages TI
		INNER JOIN TOrnament O On O.OrnamentID = TI.OrnamentID
	END
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCategoryInfoGet]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[uspCategoryInfoGet]
GO
-- uspCompanyInfoGet 1, 1
CREATE PROCEDURE [uspCategoryInfoGet]
		@CategoryID int null
AS
BEGIN
	IF(@CategoryID <> 0)
	BEGIN
		SELECT [CategoryID], [Name], [Description], [LogoImage], [Created], [CreatedBy], [Modified], [ModifiedBy]
		FROM TCategory
		WHERE [CategoryID] = @CategoryID
	END
	ELSE
	BEGIN
		SELECT [CategoryID], [Name], [Description], [LogoImage], [Created], [CreatedBy], [Modified], [ModifiedBy]
		FROM TCategory
	END
END
GO



IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCategoryDelete]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[uspCategoryDelete]
GO
-- uspCompanyInfoGet 1, 1
CREATE PROCEDURE [uspCategoryDelete]
		@CategoryID int null
AS
BEGIN
	Declare @error int,
			@ErrorSuccessMsg varchar(100)
	BEGIN TRANSACTION CategoryDetailRemove

	IF(@CategoryID <> 0)
	BEGIN
		IF EXISTS (SELECT 1 FROM TOrnament WHERE CategoryID = @CategoryID)
		BEGIN
			SELECT 0 IsSuccess,
			'Category being use in relative Ornaments. Please try again.!!' AS ErrorSuccessMsg
		END
		ELSE
		BEGIN
			DELETE FROM TCategory WHERE [CategoryID] = @CategoryID
			SET @error = @@ERROR
			SET @ErrorSuccessMsg = 'Category Deleted Successfully!'
		END
	END
	ELSE 
	BEGIN
		SET @error = 1
	END

	IF @error != 0
	BEGIN
		ROLLBACK TRANSACTION CategoryDetailRemove
		SELECT 0 IsSuccess,
			'Something went wrong. Please try again.!!' AS ErrorSuccessMsg
	END
	ELSE
	BEGIN
		COMMIT TRANSACTION CategoryDetailRemove
		SELECT 1 IsSuccess,
				@ErrorSuccessMsg AS ErrorSuccessMsg
	END
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspOrnamentPositionDelete]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[uspOrnamentPositionDelete]
GO
-- uspCompanyInfoGet 1, 1
CREATE PROCEDURE [uspOrnamentPositionDelete]
		@OrnamentPositionID int null
AS
BEGIN
	Declare @error int,
			@ErrorSuccessMsg varchar(100)
	BEGIN TRANSACTION OrnamentPositionDetailRemove

	IF(@OrnamentPositionID <> 0)
	BEGIN
		IF EXISTS (SELECT 1 FROM TOrnament WHERE PositionID = @OrnamentPositionID)
		BEGIN
			SELECT 0 IsSuccess,
			'Ornament Position being use in relative Ornaments. Please try again.!!' AS ErrorSuccessMsg
		END
		ELSE
		BEGIN
			DELETE FROM TPosition WHERE PositionID = @OrnamentPositionID
			SET @error = @@ERROR
			SET @ErrorSuccessMsg = 'Ornament Position Deleted Successfully!'
		END
	END
	ELSE 
	BEGIN
		SET @error = 1
	END

	IF @error != 0
	BEGIN
		ROLLBACK TRANSACTION OrnamentPositionDetailRemove
		SELECT 0 IsSuccess,
			'Something went wrong. Please try again.!!' AS ErrorSuccessMsg
	END
	ELSE
	BEGIN
		COMMIT TRANSACTION OrnamentPositionDetailRemove
		SELECT 1 IsSuccess,
				@ErrorSuccessMsg AS ErrorSuccessMsg
	END
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspOrnamentsDelete]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[uspOrnamentsDelete]
GO
-- uspCompanyInfoGet 1, 1
CREATE PROCEDURE [uspOrnamentsDelete]
		@OrnamentID int null
AS
BEGIN
	Declare @error int,
			@ErrorSuccessMsg varchar(100)
	BEGIN TRANSACTION OrnamentsDetailRemove

	IF(@OrnamentID <> 0)
	BEGIN
		DELETE FROM TOrnament WHERE [OrnamentID] = @OrnamentID
		SET @error = @@ERROR
		SET @ErrorSuccessMsg = 'Ornaments Deleted Successfully!'
	END
	ELSE 
	BEGIN
		SET @error = 1
	END

	IF @error != 0
	BEGIN
		ROLLBACK TRANSACTION OrnamentsDetailRemove
		SELECT 0 IsSuccess,
			'Something went wrong. Please try again.!!' AS ErrorSuccessMsg
	END
	ELSE
	BEGIN
		COMMIT TRANSACTION OrnamentsDetailRemove
		SELECT 1 IsSuccess,
				@ErrorSuccessMsg AS ErrorSuccessMsg
	END
END
GO
----
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspCompanyDelete]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[uspCompanyDelete]
GO
-- uspCompanyInfoGet 1, 1
CREATE PROCEDURE [uspCompanyDelete]
		@CompanyID int null
AS
BEGIN
	Declare @error int,
			@ErrorSuccessMsg varchar(100)

	BEGIN TRANSACTION CompanyDetailRemove

	IF(@CompanyID <> 0)
	BEGIN
		DELETE FROM TBranch WHERE [CompanyID] = @CompanyID
		SET @error = @@ERROR
		IF @error = 0
		BEGIN
			DELETE FROM TCompany WHERE [CompanyID] = @CompanyID
			SET @error = @@ERROR
		END
		SET @ErrorSuccessMsg = 'Company Deleted Successfully!'
	END
	ELSE 
	BEGIN
		SET @error = 1
	END

	IF @error != 0
	BEGIN
		ROLLBACK TRANSACTION CompanyDetailRemove
		SELECT 0 IsSuccess,
			'Something went wrong. Please try again.!!' AS ErrorSuccessMsg
	END
	ELSE
	BEGIN
		COMMIT TRANSACTION CompanyDetailRemove
		SELECT 1 IsSuccess,
				@ErrorSuccessMsg AS ErrorSuccessMsg
	END
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspBranchDelete]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[uspBranchDelete]
GO
-- uspCompanyInfoGet 1, 1
CREATE PROCEDURE [uspBranchDelete]
		@BranchID int null
AS
BEGIN
	Declare @error int,
			@ErrorSuccessMsg varchar(100)

	BEGIN TRANSACTION BranchDetailRemove

	IF(@BranchID <> 0)
	BEGIN
		DELETE FROM TBranch WHERE BranchID = @BranchID
		SET @error = @@ERROR
		SET @ErrorSuccessMsg = 'Branch Deleted Successfully!'
	END
	ELSE 
	BEGIN
		SET @error = 1
	END

	IF @error != 0
	BEGIN
		ROLLBACK TRANSACTION CompanyDetailRemove
		SELECT 0 IsSuccess,
			'Something went wrong. Please try again.!!' AS ErrorSuccessMsg
	END
	ELSE
	BEGIN
		COMMIT TRANSACTION CompanyDetailRemove
		SELECT 1 IsSuccess,
				@ErrorSuccessMsg AS ErrorSuccessMsg
	END
END
GO


IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspGetImages]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[uspGetImages]
GO
-- uspCompanyInfoGet 1, 1
CREATE PROCEDURE [uspGetImages]
		@Type int,
		@OtherID int null
AS
BEGIN
	-- 1: Category, 2: Ornaments 3: Position 
	IF(@Type = 1)
	BEGIN
		SELECT LogoImage AS [ImgPath] FROM TCategory
	END
	ELSE IF(@Type = 2 AND ISNULL(@OtherID, 0) > 0)
	BEGIN
		SELECT ImgPath FROM TImages Where OrnamentID = @OtherID
	END
	ELSE IF(@Type = 3)
	BEGIN
		SELECT LogoImage AS [ImgPath] FROM TPosition
	END
END
GO

-- *******************************************************************
--	CREATED: 01Sep2017 by Sumit Darji
--	DESCRIPTION: USER FORGOT PASSWORD
--	MODIFICATION LOG:
-- *******************************************************************
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspForgotPassword]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[uspForgotPassword]
GO
CREATE PROCEDURE uspForgotPassword
				@Username NVARCHAR(255),
				@Email NVARCHAR(100)
AS
BEGIN
	IF EXISTS (SELECT 1 FROM TAdminUser WHERE Username = @Username OR Email = @Email)
	BEGIN
			SELECT
					AdminUserID AS ReturnValue,
					Email AS Email,
					Password AS [Password],
					1 AS isAdmin,
					1 AS Success,
					'Email Sent to your Registered Email Address !!!' AS ErrorSuccessMsg
			FROM 
					TAdminUser
			WHERE 
					Username = @Username OR 
					Email = @Email
	END
	ELSE IF EXISTS (SELECT 1 FROM TCompany WHERE Username = @Username OR Email = @Email)
	BEGIN
			SELECT
					TC.CompanyID AS ReturnValue,
					TC.Email,
					TC.Password,
					0 AS isAdmin,
					1 AS Success,
					'Email Sent to your Registered Email Address !!!' AS ErrorSuccessMsg
			FROM 
					TCompany TC
			WHERE 
					TC.Username = @Username OR 
					TC.Email = @Email
	END
	ELSE IF EXISTS (SELECT 1 FROM TBranch WHERE Username = @Username OR Email = @Email)
	BEGIN
			SELECT
					TB.BranchID AS ReturnValue,
					TB.Email,
					TB.Password,
					0 AS isAdmin,
					1 AS Success,
					'Email Sent to your Registered Email Address !!!' AS ErrorSuccessMsg
			FROM 
					TBranch TB
			WHERE 
					TB.Username = @Username OR 
					TB.Email = @Email
	END
	ELSE
	BEGIN
		SELECT 
			0 AS Success,
			'Username or Email does not exists !!!' AS ErrorSuccessMsg
	END
END

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uspDashboardDetail]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[uspDashboardDetail]
GO
CREATE PROCEDURE uspDashboardDetail
AS
BEGIN
			SELECT
					ISNULL((SELECT Count(1) FROM TCompany),0) AS CompanyCount,
					ISNULL((SELECT Count(1) FROM TCategory),0) AS CategoryCount,
					ISNULL((SELECT Count(1) FROM TPosition),0) AS OrnamentPositionCount,
					ISNULL((SELECT * FROM TOrnament),0) AS OrnamentCount,
					1 AS Success,
					'' AS ErrorSuccessMsg
END