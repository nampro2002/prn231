using System;
using System.Collections.Generic;

namespace PRN231_HE160575_Project04.ModelsV2
{
    public partial class AdministrativeRegion
    {
        public AdministrativeRegion()
        {
            Provinces = new HashSet<Province>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string NameEn { get; set; } = null!;
        public string CodeName { get; set; } = null!;
        public string CodeNameEn { get; set; } = null!;

        public virtual ICollection<Province> Provinces { get; set; }
    }
}
