
/****** Object:  StoredProcedure [dbo].[agilixImport_GetSiteID]    Script Date: 08/07/2011 01:09:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[agilixImport_GetSiteID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[agilixImport_GetSiteID]
GO


/****** Object:  StoredProcedure [dbo].[agilixImport_GetSiteID]    Script Date: 08/07/2011 01:09:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[agilixImport_GetSiteID]            
@IMPORTID  INT      
AS            
BEGIN        
DECLARE @SITEID AS INT          
SELECT SITEID, ENVSHORTDESC, STATUSSHORTDESC, PXENV.EnvID as EnvID,   
Comments , DestServerTypeDesc , 
convert(varchar(10), ScheduleImportOn, 121) ScheduleImportOn
FROM  IMPORT.TBLAGILIXIMPORT  AI    
 JOIN INFO.TBLPXENVIRONMENT PXENV ON AI.PXENVID = PXENV.ENVID    
 JOIN INFO.TBLSTATUS ST ON AI.STATUSID = ST.STATUSID    
 Join info.tblDestServerType DServer on AI.DestServerTypeID = DServer.DestServerTypeID    
WHERE IMPORTID= @IMPORTID      
END 

GO

