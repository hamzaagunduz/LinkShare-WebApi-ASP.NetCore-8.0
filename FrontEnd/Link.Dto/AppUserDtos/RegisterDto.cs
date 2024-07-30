using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Dto.AppUserDtos
{
    public class RegisterDto
    {
        public string? FirstName { get; set; }
        public string ?Password { get; set; }
        public string? SurName { get; set; }
        public string ?UserName { get; set; }
        public string ?Email { get; set; }
    }
}
