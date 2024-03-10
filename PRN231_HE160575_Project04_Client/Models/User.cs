using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN231_HE160575_Project04_Client.Models
{
    public class User
    {
        public int Id
        {
            get => UserId;
            set => UserId = value;
        }

        public int UserId { get; set; }

        public bool IsActived { get; set; } = true;
        public bool IsVerified { get; set; } = true;

        public double balance { get; set; } = 0;

        public int Roll { get; set; }

        public string FullName { get; set; }

        public string Birthdate { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Address { get; set; }
        public string Avatar { get; set; }
    }
}
