using System;
using System.Collections.Generic;

namespace PRN231_HE160575_Project04.ModelsV2
{
    public partial class Province
    {
        public Province()
        {
            Districts = new HashSet<District>();
            Houses = new HashSet<House>();
        }

        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string NameEn { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string FullNameEn { get; set; } = null!;
        public string CodeName { get; set; } = null!;
        public int? AdministrativeUnitId { get; set; }
        public int? AdministrativeRegionId { get; set; }

        public virtual AdministrativeRegion? AdministrativeRegion { get; set; }
        public virtual AdministrativeUnit? AdministrativeUnit { get; set; }
        public virtual ICollection<District> Districts { get; set; }
        public virtual ICollection<House> Houses { get; set; }
    }
}
