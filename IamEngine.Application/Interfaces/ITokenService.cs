using IamEngine.Domain.Entities;

namespace IamEngine.Application.Interfaces;

public interface ITokenService
{
    Task<string> GenerateJwtAsync(AppUser user);
}