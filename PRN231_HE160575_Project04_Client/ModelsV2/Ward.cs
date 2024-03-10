using System;
using System.Collections.Generic;

namespace PRN231_HE160575_Project04_Client.ModelsV2
{
    public partial class Ward
    {
        public Ward()
        {
            Houses = new HashSet<House>();
        }

        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string NameEn { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string FullNameEn { get; set; } = null!;
        public string CodeName { get; set; } = null!;
        public string DistrictCode { get; set; } = null!;
        public int? AdministrativeUnitId { get; set; }

        public virtual AdministrativeUnit? AdministrativeUnit { get; set; }
        public virtual District DistrictCodeNavigation { get; set; } = null!;
        public virtual ICollection<House> Houses { get; set; }
    }
}
