using Authentication.DataAccessLayer.Entities.Exceptions.MessagesConstants;

namespace Authentication.DataAccessLayer.Entities.Exceptions.BadRequest;

public class RefreshTokenBadRequestException(string message) : BadRequestException(message);