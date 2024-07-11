using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Domain.Entities
{
    public class AppUser:IdentityUser<int>
    {

        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }
        public int PostCount { get; set; }
        public int View { get; set; }
        public string? ImageUrl { get; set; }
        public string? About { get; set; }
        public List<ProfileComment> ProfileComments { get; set; }
        public List<Linke> Linkes { get; set; }
        public List<Follower> Followers { get; set; }
        public List<Following> Followings { get; set; }



    }
}
