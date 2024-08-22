using System.ComponentModel.DataAnnotations;

namespace ProjectAkbas.Models
{
    public class mProformaMaliyet
    {
        [Key]
        public int IDD { get; set; }
        public string? Qualities { get; set; }
        public string? MalGrubu { get; set; }
        public int? SiraBenzerKumas { get; set; }
        public string? KumasKodu { get; set; }
        public string? KumasKalitesi { get; set; }
        public decimal? HamKumasFiyatiMt { get; set; }
        public string? DokumaOrme { get; set; }
        public decimal? YurtDisiDolarMetre { get; set; }
        public decimal? ProfSFYurtici { get; set; }
        public string? Islem { get; set; }
        public decimal? ProfSFYurtdisiIng { get; set; }
        public decimal? YurticiDolarMetre { get; set; }
        public string? QualitiesGroup { get; set; }

    }
}
