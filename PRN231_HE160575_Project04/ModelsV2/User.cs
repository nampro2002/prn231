using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PRN231_HE160575_Project04.ModelsV2
{
    public partial class User
    {
        public User()
        {
            BookingHistories = new HashSet<BookingHistory>();
            Houses = new HashSet<House>();
        }

        public int UserId { get; set; }
        public string FullName { get; set; } 
        public string Birthdate { get; set; }
        public string PhoneNumber { get; set; } 
        public string Email { get; set; }
        public string Password { get; set; } 
        public string Address { get; set; } 
        public string Avatar { get; set; }
        public int Roll { get; set; }
        public bool? IsActived { get; set; }
        public bool? IsVerified { get; set; }
        public double Balance { get; set; }
        [JsonIgnore]
        public virtual ICollection<BookingHistory> BookingHistories { get; set; }
        [JsonIgnore]
        public virtual ICollection<House> Houses { get; set; }
    }
}
