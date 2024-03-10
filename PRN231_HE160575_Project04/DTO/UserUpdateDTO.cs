namespace PRN231_HE160575_Project04.DTO
{
    public class UserUpdateDTO
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Birthdate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool? IsActived { get; set; }
    }
}
