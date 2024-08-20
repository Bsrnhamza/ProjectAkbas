
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectAkbas.Models
{
    public class mSapIthalatVerileri
    {
        [Key]
        [Required]
        [StringLength(18)] 
        public string? Kumas { get; set; }

        [DataType(DataType.Currency)] 
        public decimal? Sipariste { get; set; } 

        [DataType(DataType.Currency)] 
        public decimal? Antrepoda { get; set; } 

    }
}