
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectAkbas.Models
{
    public class mSapIthalatVerileri
    {
        [Key]
        [Required]
        [StringLength(18)]  // char(18)
        public string Kumas { get; set; }

        [DataType(DataType.Currency)] // Decimal
        public decimal? Sipariste { get; set; } // Nullable decimal

        [DataType(DataType.Currency)] // Decimal
        public decimal? Yolda { get; set; } // Nullable decimal

        [DataType(DataType.Currency)] // Decimal
        public decimal? ToplamMiktar { get; set; } // Nullable decimal

        [DataType(DataType.Currency)] // Decimal
        public decimal? Antrepoda { get; set; } // Nullable decimal

        [StringLength(10)]  // nchar(10)
        public string OlcuBirimi { get; set; }
    }
}