using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectAkbas.Models

{
    [Table("MARD")]
    public class mMard
    {
        [Key]
        [Column("MATNR")]
        public string mardMATNR { get; set; }

        public decimal LABST { get; set; }
    }
}
