using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PRN231_HE160575_Project04.ModelsV2
{
    public partial class House
    {
        //public House()
        //{
        //    BookingHistories = new HashSet<BookingHistory>();
        //}

        public int HouseId { get; set; }
        public string Title { get; set; } = null!;
        public string Image { get; set; } = null!;
        public decimal Price { get; set; }
        public int Area { get; set; }
        public string Location { get; set; } = null!;
        public int DaysAgo { get; set; }
        public bool IsActive { get; set; }
        public int Type { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; } = null!;
        public int Rate { get; set; }
        public DateTime PublicDay { get; set; }
        public int UserId { get; set; }
        public string? DistrictCode { get; set; }
        public string? ProvinceCode { get; set; }
        public string? WardCode { get; set; }
        public virtual District? DistrictCodeNavigation { get; set; }
        public virtual Province? ProvinceCodeNavigation { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual Ward? WardCodeNavigation { get; set; }
        public virtual ICollection<BookingHistory> BookingHistories { get; set; }
    }
}
