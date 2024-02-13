using Shared.Exceptions.BadRequest;

namespace Authentication.DataAccessLayer.Entities.Exceptions.BadRequest;

public class RefreshTokenBadRequest() : BadRequestException("Invalid client request: token has invalid values.");