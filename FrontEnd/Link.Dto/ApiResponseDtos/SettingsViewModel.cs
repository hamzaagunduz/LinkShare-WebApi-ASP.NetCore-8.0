using Link.Dto.SettingsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Dto.ApiResponseDtos
{
    public class SettingsViewModel
    {
        public GetSettingsDto GetSettings { get; set; }
        public UpdateSettingsDto UpdateSettings { get; set; }
    }

}
