USE [AngularCRUD]
GO
/****** Object:  Table [dbo].[tblEmployeeDetails]    Script Date: 3/31/2022 2:02:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblEmployeeDetails](
	[EmpID] [int] IDENTITY(1,1) NOT NULL,
	[EmpName] [varchar](100) NULL,
	[DateOfBirth] [nvarchar](50) NULL,
	[EmailID] [varchar](100) NULL,
	[Gender] [nchar](10) NULL,
	[EmpAddress] [varchar](150) NULL,
	[Pincode] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[EmpID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[spDeleteEmployeeRecord]    Script Date: 3/31/2022 2:02:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Dhruvin Lad(KCSITGLOBAL)
-- Create date: 30-March-2022
-- Description:	Will delete the recored based on its id
-- =============================================
CREATE PROCEDURE [dbo].[spDeleteEmployeeRecord] 
	@EmpID int
AS
BEGIN
	DELETE FROM 
		tblEmployeeDetails 
	WHERE 
		EmpID=@EmpID;
END
GO
/****** Object:  StoredProcedure [dbo].[spUpdateEmailID]    Script Date: 3/31/2022 2:02:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Dhruvin Lad(KCSITGLOBAL)
-- Create date: 29-March-2022
-- Description:	Will Update the Email of the user by Employee ID
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateEmailID]
@EmpID int,
@EmailID varchar(100)
AS
BEGIN
	UPDATE tblEmployeeDetails

	SET 
		EmailID = @EmailID
	WHERE 
		EmpID=@EmpID;
		return @@rowcount
END
GO
