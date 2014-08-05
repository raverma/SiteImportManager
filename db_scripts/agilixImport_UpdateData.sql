
/****** Object:  StoredProcedure [dbo].[aglixImport_UpdateData]    Script Date: 08/07/2011 01:10:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aglixImport_UpdateData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[aglixImport_UpdateData]
GO


/****** Object:  StoredProcedure [dbo].[aglixImport_UpdateData]    Script Date: 08/07/2011 01:10:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
CREATE PROCEDURE [dbo].[aglixImport_UpdateData]  
(  
 @ImportId int,  
 @DestServerTypeID int,  
 @StatusID smallint,   
 @Comments varchar(100),
 @Date datetime
)  
AS  
begin  
UPDATE Import.tblAgilixImport SET StatusID=@StatusID,   
DestServerTypeID = @DestServerTypeID,   
Comments= @Comments  , ScheduleImportOn= @Date 
WHERE ImportId = @ImportId  
end  

GO

