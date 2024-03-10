using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PRN231_HE160575_Project04_Client.Models
{
    public class House
    {
        public int HouseId { get; set; }

        public string Title { get; set; }

        public string Image { get; set; }

        public decimal Price { get; set; }

        public int Area { get; set; }

        public string Location { get; set; }

        public int DaysAgo { get; set; }

        public bool IsActive { get; set; }

        public int Type { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        public int Rate { get; set; }
        public DateTime PublicDay { get; set; }



        public string ProvinceCode { get; set; }
        public Province Province { get; set; }

        public string DistrictCode { get; set; }
        public District District { get; set; }

        public string WardCode { get; set; }
        public Ward Ward { get; set; }



        public int UserId { get; set; }
        public User User { get; set; }
    }
}
