using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PRN231_HE160575_Project04.ModelsV2
{
    public partial class BookingHistory
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int HouseId { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public virtual House House { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
