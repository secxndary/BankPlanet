namespace Authentication.BusinessLogicLayer.DataTransferObjects;

public record TokenDto
(
    string AccessToken, 
    string RefreshToken
);