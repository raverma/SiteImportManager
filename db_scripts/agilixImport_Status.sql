
/****** Object:  StoredProcedure [dbo].[agilixImport_Status]    Script Date: 08/07/2011 01:11:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[agilixImport_Status]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[agilixImport_Status]
GO


/****** Object:  StoredProcedure [dbo].[agilixImport_Status]    Script Date: 08/07/2011 01:11:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE  PROCEDURE [dbo].[agilixImport_Status]   
          
AS          
BEGIN          
 -- SET NOCOUNT ON added to prevent extra result sets from          
 -- interfering with SELECT statements.          
 SET NOCOUNT ON;   
 SELECT StatusID, StatusShortDesc FROM info.tblStatus  
 where statusid in (1, 5, 6)
   
END 
GO

