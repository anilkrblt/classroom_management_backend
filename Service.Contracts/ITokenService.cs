using System.Security.Claims;

namespace Service.Contracts
{
    public interface ITokenService
    {
        string CreateToken(string userName, IEnumerable<Claim> claims);
    }
}
