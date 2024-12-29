using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record ClubDto
    {
        public int ClubId { get; set; }

        public string? ClubName { get; set; }

        public string ClubShorcut { get; set; }
        public string ClubLogo { get; set; }

        public string ClubManager { get; set; }

        public int? ClubManagerId { get; set; }





    }

}