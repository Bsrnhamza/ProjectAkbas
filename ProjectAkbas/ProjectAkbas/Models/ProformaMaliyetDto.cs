using System.ComponentModel.DataAnnotations;

namespace ProjectAkbas.Models
{
    public class ProformaMaliyetDto
    {
        public string? Qualities { get; set; }
        public int IDD { get; set; }
        public string? MalGrubu { get; set; }
        public int? SiraBenzerKumas { get; set; }
        public string? KumasKodu { get; set; }
        public string? KumasKalitesi { get; set; }
        public decimal? HamKumasFiyatiMt { get; set; }
        public string? DokumaOrme { get; set; }
        public decimal? YurtDisiDolarMetre { get; set; }
        public decimal? ProfSFYurtici { get; set; }
        public string? Islem { get; set; }
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
        public string? ZGRUP_ADI { get; set; }
        public decimal? StokMiktarı { get; set; }
        public decimal? Sipariste { get; set; }
        public decimal? Antrepoda { get; set; }
        public decimal? YurticiDolarMetre { get; set; }
        public decimal? LABST { get; set; }
        public decimal? ProfSFYurtdisiIng { get; set; }
   

    }
}
