using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project01.Enums;

namespace ContactRegister.Models
{
    public class UserModel
    {
        public ProfileEnum Profile { get; set; }

        public string Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public UserModel(ProfileEnum profile)
        {
            Profile = profile;

            string typeUser = "";

            if (Profile == ProfileEnum.ADM)
            {
                typeUser = "ADM";
            }

            else if (Profile == ProfileEnum.MEM)
            {
                typeUser = "MEM";
            }

            Id = $"USER-{Guid.NewGuid():N}-{typeUser}";
        }
    }
}