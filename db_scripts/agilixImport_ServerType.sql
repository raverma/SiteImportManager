
/****** Object:  StoredProcedure [dbo].[agilixImport_ServerType]    Script Date: 08/07/2011 01:10:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[agilixImport_ServerType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[agilixImport_ServerType]
GO

/****** Object:  StoredProcedure [dbo].[agilixImport_ServerType]    Script Date: 08/07/2011 01:10:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE  PROCEDURE [dbo].[agilixImport_ServerType]   
          
AS          
BEGIN          
 -- SET NOCOUNT ON added to prevent extra result sets from          
 -- interfering with SELECT statements.          
 SET NOCOUNT ON;   
 SELECT DestServerTypeID  as ID, 
 DestServerTypeValue + ' (' +   DestServerTypeDesc + ')' as DestData FROM info.tblDestServerType
 SET NOCOUNT Off;   
END 

 
GO

