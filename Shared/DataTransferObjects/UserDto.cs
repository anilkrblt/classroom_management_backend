using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public class UserDto
    {
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsAdmin { get; set; }
        public string FullName { get; set; }    // Kullanıcının tam adı
        public int Id { get; set; }             // Kullanıcı ID'si
        public DateTime LastLogin { get; set; } // Son giriş zamanı (Opsiyonel)
    }

}