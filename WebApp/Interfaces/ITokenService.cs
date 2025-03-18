using WebApp.Models;

namespace WebApp.Interfaces;

public interface ITokenService
{
    string  CreateToken(User user);
}