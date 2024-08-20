using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectAkbas.Models
{

    [Table("ZMM_GRUP_T", Schema = "aep")]
    public class ZmmGrupT
    {
        [Column("MANDT")]
        public string? zmMandt { get; set; }

        [Column("ZGRUP")]
        public string? zmGrup { get; set; }

        [Column("ZGRUP_ADI")]
        public string? zmGrupAd { get; set; }

        [Column("ZGRUP_ADI_EN")]
        public string? zmGrupAdiEn { get; set; }
    }
}
