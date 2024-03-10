using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PRN231_HE160575_Project04_Client.Models
{
    public class AdministrativeRegion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Name_en { get; set; }

        public string Code_name { get; set; }

        public string Code_name_en { get; set; }
    }

    public class AdministrativeUnit
    {
        [Key]
        public int Id { get; set; }

        public string Full_name { get; set; }

        public string Full_name_en { get; set; }

        public string Short_name { get; set; }

        public string Short_name_en { get; set; }

        public string Code_name { get; set; }

        public string Code_name_en { get; set; }
    }

    public class Province
    {
        [Key]
        [StringLength(20)]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        public string Name_en { get; set; }

        [Required]
        public string Full_name { get; set; }

        public string Full_name_en { get; set; }

        public string Code_name { get; set; }

        [ForeignKey("AdministrativeUnit")]
        public int? Administrative_unit_id { get; set; }

        [ForeignKey("AdministrativeRegion")]
        public int? Administrative_region_id { get; set; }

        public AdministrativeUnit AdministrativeUnit { get; set; }

        public AdministrativeRegion AdministrativeRegion { get; set; }

        public ICollection<House> Houses { get; set; }
    }

    public class District
    {
        [Key]
        [StringLength(20)]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        public string Name_en { get; set; }

        public string Full_name { get; set; }

        public string Full_name_en { get; set; }

        public string Code_name { get; set; }

        [ForeignKey("Province")]
        public string Province_code { get; set; }

        [ForeignKey("AdministrativeUnit")]
        public int? Administrative_unit_id { get; set; }

        public Province Province { get; set; }

        public AdministrativeUnit AdministrativeUnit { get; set; }

        public ICollection<House> Houses { get; set; }
    }

    public class Ward
    {
        [Key]
        [StringLength(20)]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        public string Name_en { get; set; }

        public string Full_name { get; set; }

        public string Full_name_en { get; set; }

        public string Code_name { get; set; }

        [ForeignKey("District")]
        public string District_code { get; set; }

        [ForeignKey("AdministrativeUnit")]
        public int? Administrative_unit_id { get; set; }

        public District District { get; set; }

        public AdministrativeUnit AdministrativeUnit { get; set; }

        public ICollection<House> Houses { get; set; }
    }

}
