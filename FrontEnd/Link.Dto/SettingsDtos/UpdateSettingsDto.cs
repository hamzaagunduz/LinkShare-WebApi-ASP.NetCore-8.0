using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Dto.SettingsDtos
{
    public class UpdateSettingsDto
    {
            public int Id { get; set; }
            public string firstName { get; set; }
            public string userName { get; set; }
            public string surName { get; set; }
            public string email { get; set; }
            public string password { get; set; }
            public string imageUrl { get; set; }
            public string about { get; set; }

    }
}
