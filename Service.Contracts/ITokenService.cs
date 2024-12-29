using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Service.Contracts
{
    public interface ITokenService
    {
        // Kullanıcı için bir JWT oluşturur
        string CreateToken(string userName, IEnumerable<Claim> claims);

        // Gelen bir token'in geçerliliğini kontrol eder
        ClaimsPrincipal? ValidateToken(string token);

        // Kullanıcı için bir refresh token oluşturur
        string CreateRefreshToken();

        // Refresh token'in geçerliliğini kontrol eder (Opsiyonel)
        bool ValidateRefreshToken(string refreshToken);

        // Token'in süresinin dolacağı zamanı döndürür
        DateTime? GetTokenExpiryDate(string token);
    }
}
