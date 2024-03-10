using System;
using System.Collections.Generic;

namespace PRN231_HE160575_Project04.ModelsV2
{
    public partial class AdministrativeUnit
    {
        public AdministrativeUnit()
        {
            Districts = new HashSet<District>();
            Provinces = new HashSet<Province>();
            Wards = new HashSet<Ward>();
        }

        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string FullNameEn { get; set; } = null!;
        public string ShortName { get; set; } = null!;
        public string ShortNameEn { get; set; } = null!;
        public string CodeName { get; set; } = null!;
        public string CodeNameEn { get; set; } = null!;

        public virtual ICollection<District> Districts { get; set; }
        public virtual ICollection<Province> Provinces { get; set; }
        public virtual ICollection<Ward> Wards { get; set; }
    }
}
