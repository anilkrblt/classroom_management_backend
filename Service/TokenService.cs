using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Contracts;

namespace Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Kullanıcı için bir JWT oluşturur
        public string CreateToken(string userName, IEnumerable<Claim> claims)
        {
            Console.WriteLine("=== [CreateToken] Başladı ===");
            
            var jwtSettings = _configuration.GetSection("JwtSettings");

            // Debug amaçlı, bu değerleri loglayın:
            Console.WriteLine($"[CreateToken] Kullanılan Ayarlar:");
            Console.WriteLine($" -> Key: {jwtSettings["Key"]}");
            Console.WriteLine($" -> Issuer: {jwtSettings["Issuer"]}");
            Console.WriteLine($" -> Audience: {jwtSettings["Audience"]}");
            Console.WriteLine($" -> ExpiresInMinutes: {jwtSettings["ExpiresInMinutes"]}");

            // Kullanıcı adı / claims de loglanabilir
            Console.WriteLine($"[CreateToken] userName: {userName}");
            foreach (var c in claims)
            {
                Console.WriteLine($"   Claim: {c.Type} = {c.Value}");
            }

            // Token oluşturma
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiresMinutes = Convert.ToDouble(jwtSettings["ExpiresInMinutes"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expiresMinutes),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Token oluşturuldu, henüz doğrulama testini yapalım
            Console.WriteLine("[CreateToken] Token oluşturuldu, şimdi doğrulanıyor...");
            var principal = ValidateToken(tokenString);

            if (principal == null)
            {
                Console.WriteLine("[CreateToken] -> Oluşturulan token doğrulanamadı!");
            }
            else
            {
                Console.WriteLine("[CreateToken] -> Oluşturulan token başarıyla doğrulandı.");
            }

            Console.WriteLine("=== [CreateToken] Bitti ===");
            return tokenString;
        }

        // Gelen bir token'in geçerliliğini kontrol eder
        public ClaimsPrincipal? ValidateToken(string token)
        {
            Console.WriteLine("=== [ValidateToken] Başladı ===");
            Console.WriteLine($"[ValidateToken] Gelen token: {token}");
            
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var keyValue = jwtSettings["Key"];
            var issuerValue = jwtSettings["Issuer"];
            var audienceValue = jwtSettings["Audience"];

            Console.WriteLine($"[ValidateToken] Ayarlar:");
            Console.WriteLine($" -> Key: {keyValue}");
            Console.WriteLine($" -> Issuer: {issuerValue}");
            Console.WriteLine($" -> Audience: {audienceValue}");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyValue));
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var validationParams = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuerValue,
                    ValidAudience = audienceValue,
                    IssuerSigningKey = key,

                    // İsteğe bağlı: ClockSkew = TimeSpan.Zero,
                };

                Console.WriteLine("[ValidateToken] -> ValidateToken çağrılıyor...");
                var claimsPrincipal = tokenHandler.ValidateToken(token, validationParams, out SecurityToken validatedToken);
                
                // Extra log: Token detayları
                if (validatedToken is JwtSecurityToken jwt)
                {
                    Console.WriteLine($"[ValidateToken] validatedToken.Issuer: {jwt.Issuer}");
                    Console.WriteLine($"[ValidateToken] validatedToken.Audience: {string.Join(",", jwt.Audiences)}");
                    Console.WriteLine($"[ValidateToken] validatedToken.ValidFrom: {jwt.ValidFrom}");
                    Console.WriteLine($"[ValidateToken] validatedToken.ValidTo: {jwt.ValidTo}");
                }

                Console.WriteLine("[ValidateToken] -> Token geçerli bulundu!");
                return claimsPrincipal;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ValidateToken] -> HATA: {ex.GetType().Name} - {ex.Message}");
                // Gerekirse stack trace de:
                // Console.WriteLine(ex.StackTrace);
                Console.WriteLine("=== [ValidateToken] Bitti ===");
                return null; // Token geçersizse null döndür
            }
        }

        // Kullanıcı için bir refresh token oluşturur
        public string CreateRefreshToken()
        {
            Console.WriteLine("[CreateRefreshToken] Yeni refresh token oluşturuluyor...");
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }
            var refresh = Convert.ToBase64String(randomNumber);
            Console.WriteLine($"[CreateRefreshToken] Oluşturulan refresh token: {refresh}");
            return refresh;
        }

        // Refresh token'in geçerliliğini kontrol eder (Opsiyonel)
        public bool ValidateRefreshToken(string refreshToken)
        {
            Console.WriteLine("[ValidateRefreshToken] -> Gelen refresh token: " + refreshToken);
            var isValid = !string.IsNullOrEmpty(refreshToken);
            Console.WriteLine("[ValidateRefreshToken] -> " + (isValid ? "Geçerli" : "Geçersiz"));
            return isValid;
        }

        // Token'in süresinin dolacağı zamanı döndürür
        public DateTime? GetTokenExpiryDate(string token)
        {
            Console.WriteLine("[GetTokenExpiryDate] -> Token süresi kontrol ediliyor...");
            var tokenHandler = new JwtSecurityTokenHandler();
            if (tokenHandler.CanReadToken(token))
            {
                var jwtToken = tokenHandler.ReadJwtToken(token);
                Console.WriteLine($"[GetTokenExpiryDate] ValidFrom: {jwtToken.ValidFrom}, ValidTo: {jwtToken.ValidTo}");
                return jwtToken.ValidTo; // Token'in geçerli olduğu son tarih
            }
            Console.WriteLine("[GetTokenExpiryDate] -> Token okunamadı.");
            return null;
        }
    }
}
