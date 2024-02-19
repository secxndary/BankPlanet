using Common;

namespace Authentication.DataAccessLayer.Entities.Exceptions.BadRequest;

public class RefreshTokenBadRequest() : BadRequestException(ExceptionMessagesConstants.RefreshTokenBadRequest);