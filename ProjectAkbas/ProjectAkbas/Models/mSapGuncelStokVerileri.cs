
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectakbas.models
{
    public class mSapGuncelStokVerileri
    {
        [Key]
        public string? Kumas { get; set; }

        public decimal StokMiktarı { get; set; }

        public char Type_ { get; set; }

        public string OlcuBirimi { get; set; }

        public decimal En { get; set; }

        public decimal Gramaj { get; set; }
    }
}