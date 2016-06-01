Create PROCEDURE [dbo].[RegisterDevice]
	@DeviceType varchar(50),
	@PhoneNumber varchar(50),
	@DeviceIdentifier varchar(50),
	@DeviceName varchar(50)
AS
 Declare @Id int

	INSERT INTO 
	Device 
	(DeviceType, PhoneNumber, DeviceIdentifier, DeviceName)
	VALUES 
	(@DeviceType, @PhoneNumber, @DeviceIdentifier, @DeviceName)
	
	SET @Id = CAST(SCOPE_IDENTITY() AS INT)

	SELECT Id, AppDeviceKey, DeviceType, PhoneNumber, DeviceIdentifier, DeviceName from Device where Device.Id = @Id

RETURN 