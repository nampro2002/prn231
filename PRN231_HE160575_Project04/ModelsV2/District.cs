﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PRN231_HE160575_Project04.ModelsV2
{
    public partial class District
    {
        public District()
        {
            Houses = new HashSet<House>();
            Wards = new HashSet<Ward>();
        }

        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string NameEn { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string FullNameEn { get; set; } = null!;
        public string CodeName { get; set; } = null!;
        public string ProvinceCode { get; set; } = null!;
        public int? AdministrativeUnitId { get; set; }

        public virtual AdministrativeUnit? AdministrativeUnit { get; set; }
        public virtual Province ProvinceCodeNavigation { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<House> Houses { get; set; }
        [JsonIgnore]
        public virtual ICollection<Ward> Wards { get; set; }
    }
}
