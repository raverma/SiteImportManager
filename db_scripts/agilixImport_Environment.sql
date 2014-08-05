
/****** Object:  StoredProcedure [dbo].[agilixImport_Environment]    Script Date: 08/07/2011 01:12:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[agilixImport_Environment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[agilixImport_Environment]
GO


/****** Object:  StoredProcedure [dbo].[agilixImport_Environment]    Script Date: 08/07/2011 01:12:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE  PROCEDURE [dbo].[agilixImport_Environment]     
            
AS            
BEGIN            
 -- SET NOCOUNT ON added to prevent extra result sets from            
 -- interfering with SELECT statements.            
 SET NOCOUNT ON;     
 SELECT  EnvID as ID,   
 EnvShortDesc + ' (' +  EnvLongDesc  + ')' as DestData 
 FROM info.tblPXEnvironment
 SET NOCOUNT Off;     
END 
GO

