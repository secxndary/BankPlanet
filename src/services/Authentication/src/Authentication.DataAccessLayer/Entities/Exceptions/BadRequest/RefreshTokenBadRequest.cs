using Authentication.DataAccessLayer.Entities.Exceptions.MessagesConstants;

namespace Authentication.DataAccessLayer.Entities.Exceptions.BadRequest;

public class RefreshTokenBadRequest() : BadRequestException(ExceptionMessagesConstants.RefreshTokenBadRequest);