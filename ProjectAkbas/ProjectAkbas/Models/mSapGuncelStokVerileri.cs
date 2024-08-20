
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projectakbas.models
{
    public class mSapGuncelStokVerileri
    {

        public string? Kumas { get; set; }

        public decimal StokMiktarı { get; set; }
    }
}