﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Dto.SettingsDtos
{
    public class GetSettingsDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }
        public int PostCount { get; set; }
        public int View { get; set; }
        public object ImageUrl { get; set; }
        public string About { get; set; }
        public DateTime? LastLinkAddedTime { get; set; }
    }
}
