using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CloudServiceChallenge2.Models
{
    public class TitleMasterModel
    {
        [Key]
        public int TitleId { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string TitleName { get; set; }
        public int NumberOfWinsAvailable { get; set; }
        public ICollection<PossessionTitleDataModel> Relation { get; set; } = new HashSet<PossessionTitleDataModel>();

    }
}
