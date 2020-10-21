using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CloudServiceChallenge2.Models
{
    public class UserDataModel
    {
        [Key]
        public int UserId { get; set; }
        [Column(TypeName = "varchar(8)")]
        public string UserName { get; set; }
        public double NumberOfWins { get; set; }
        public double NumberOfDefeats { get; set; }
        public double NumberOfDraws { get; set; }

        public ICollection<PossessionTitleDataModel> Relation { get; set; } = new HashSet<PossessionTitleDataModel>();

    }
}
