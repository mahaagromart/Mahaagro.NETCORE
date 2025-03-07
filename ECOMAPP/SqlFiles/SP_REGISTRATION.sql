USE [DB_ECOMMERCE]
GO

DECLARE	@return_value Int

EXEC	@return_value = [dbo].[SP_REGISTRATION]
		@ACTION = N'LOGINUSEREEMAIL',
		@Email = N'pavanbagwe16@gmail.com',
		@Password = N'a9b69f27184a3a2789ca5061476336f130fe76ec04e0baeb58c64891f2182157'

SELECT	@return_value as 'Return Value'

GO
