using System.ComponentModel.DataAnnotations;

namespace ProjectAkbas.Models
{
    public class mMara
    {
        [Key]
        public string? MATNR { get; set; }
        public string? ZKOMP_ORM1 { get; set; }
        public string? ZKOMP_ORM_ORAN1 { get; set; }
        public string? ZKOMP_ORM2 { get; set; }
        public string? ZKOMP_ORM_ORAN2 { get; set; }
        public string? ZKOMP_ORM3 { get; set; }
        public string? ZKOMP_ORM_ORAN3 { get; set; }
        public string? ZKOMP_ORM4 { get; set; }
        public string? ZKOMP_ORM_ORAN4 { get; set; }
        public string? ZKOMP_ORM5 { get; set; }
        public string? ZKOMP_ORM_ORAN5 { get; set; }
        public string? ZGRUP { get; set; }
        public decimal? ZMML_EN { get; set; }
        public decimal? ZMML_GRM { get; set; }
    }
}
