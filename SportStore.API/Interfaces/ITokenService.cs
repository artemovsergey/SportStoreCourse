namespace SportStore.API.Interfaces;

public interface ITokenService
{
    string CreateToken(string UserLogin);
}