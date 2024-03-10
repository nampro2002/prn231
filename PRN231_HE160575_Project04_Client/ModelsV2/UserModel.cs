using System;
using System.Collections.Generic;

namespace PRN231_HE160575_Project04_Client.ModelsV2
{
    public partial class UserModel
    {
        public UserModel()
        {
            Houses = new HashSet<House>();
        }

        public int UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string Birthdate { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Avatar { get; set; } = null!;
        public int Roll { get; set; }
        public bool? IsActived { get; set; }
        public bool? IsVerified { get; set; }
        public double Balance { get; set; }

        public virtual ICollection<House> Houses { get; set; }
    }
}
