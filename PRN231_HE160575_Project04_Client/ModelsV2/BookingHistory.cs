using PRN231_HE160575_Project04_Client.Models;
using System;
using System.Collections.Generic;

namespace PRN231_HE160575_Project04_Client.ModelsV2
{
    public partial class BookingHistory
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int HouseId { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string? Price { get; set; }
        public virtual House House { get; set; } = null!;
        public virtual UserModel User { get; set; } = null!;
    }
}
